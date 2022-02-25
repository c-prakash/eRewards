using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Serilog;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using ezLoyalty.Services.Accounts.API.Application.IntegrationEvents.EventHandling;
using ezLoyalty.Services.Accounts.API.Application.IntegrationEvents.Events;
using ezLoyalty.Services.Accounts.API.Extensions;
using ezLoyalty.Services.Accounts.API.Infrastructure.AutoFacModules;

namespace ezLoyalty.Services.Accounts.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddAppInsights(Configuration);

            services.AddControllers();
            //services.AddDbContext<ActionsDbContext>(options => options.UseInMemoryDatabase(databaseName: "ActionsInstance"));

            services
              .AddCustomDbContext(Configuration)
              .AddIntegrationServices(Configuration)
              .AddEventBus(Configuration)
              .AddSwagger()
              .AddHealthChecks()
              .AddCheck("self", () => HealthCheckResult.Healthy());

            services.AddCors(c =>
              {
                  c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
              });

            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ApplicationModule());

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var pathBase = Configuration["PATH_BASE"];
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSerilogRequestLogging();

            app.UseSwagger()
             .UseSwaggerUI(c =>
             {
                 c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "TemplateAPI V1");
             });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //Todo -- fix this to selected origins only
            app.UseCors(cors => cors
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    //ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });
            });

            ConfigureEventBus(app);
        }

        protected virtual void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<ActionStatusChangedToAwaitingAccountValidationIntegrationEvent, ActionStatusChangedToAwaitingAccountValidationIntegrationEventHandler>();
        }
    }
}

