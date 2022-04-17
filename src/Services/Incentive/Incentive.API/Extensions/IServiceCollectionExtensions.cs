using Autofac;
using ezLoyalty.Services.Incentive.API;
using ezLoyalty.Services.Incentive.API.Application.IntegrationEvents;
using ezLoyalty.Services.Incentive.API.Application.IntegrationEvents.EventHandling;
using ezLoyalty.Services.Incentive.Infrastructure;
using IntegrationEventLogEF;
using IntegrationEventLogEF.Services;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBusServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ezLoyalty.Services.Incentive.API.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ezLoyalty - Incentive HTTP API",
                    Version = "v1",
                    Description = "The Incentive Microservice HTTP API."
                });

                // Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //options.IncludeXmlComments(xmlPath);
            });

            return services;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddAppInsights(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplicationInsightsTelemetry(configuration);
            services.AddApplicationInsightsKubernetesEnricher();

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IncentiveDbContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionString"],
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
            },
                ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
            );

            services.AddDbContext<IntegrationEventLogContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionString"],
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
                        //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
            });

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddIntegrationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
                sp => (c) => new IntegrationEventLogService(c));

            services.AddTransient<IIncentiveIntegrationEventService, IncentiveIntegrationEventService>();

            if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
            {
                services.AddSingleton<IServiceBusPersisterConnection>(sp =>
                {
                    var settings = sp.GetRequiredService<IOptions<IncentiveSettings>>().Value;
                    settings.EventBusConnection = configuration["ServiceBusConnectionString"];
                    var logger = sp.GetRequiredService<ILogger<DefaultServiceBusPersisterConnection>>();

                    var serviceBusConnection = new ServiceBusConnectionStringBuilder(settings.EventBusConnection);

                    return new DefaultServiceBusPersisterConnection(serviceBusConnection, logger);
                });
            }

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var subscriptionClientName = configuration["SubscriptionClientName"];

            if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
            {
                services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
                {
                    var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
                    var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                    var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                    return new EventBusServiceBus(serviceBusPersisterConnection, logger,
                        eventBusSubcriptionsManager, subscriptionClientName, iLifetimeScope);
                });

            }

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            services.AddTransient<ActionStatusChangedToAwaitingRewardsIntegrationEventHandler>();

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="targetAssembly"></param>
        /// <param name="handlerInterface"></param>
        public static void AddGenerics(this IServiceCollection services, Assembly targetAssembly, Type handlerInterface)
        {
            var handlers = targetAssembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)
            );

            foreach (var handler in handlers)
            {
                services.AddScoped(handler.GetInterfaces().First(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface), handler);
            }
        }

        /// <summary>
        /// AddHealthChecks
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());

            hcBuilder
                .AddSqlServer(configuration["ConnectionString"],
                name: "IncentiveDB-check",
                tags: new string[] { "incentivedb" });

            if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
            {
                hcBuilder
                    .AddAzureServiceBusTopic(configuration["ServiceBusConnectionString"],
                    topicName: "erewards_event_bus",
                    name: "incentive-servicebus-check",
                    tags: new string[] { "servicebus" });
            }

            return services;
        }
    }
}