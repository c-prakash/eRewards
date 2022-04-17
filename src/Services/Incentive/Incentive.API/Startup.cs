using Autofac;
using Autofac.Extensions.DependencyInjection;
using ezLoyalty.Services.Incentive.API.Application.IntegrationEvents.EventHandling;
using ezLoyalty.Services.Incentive.API.Application.IntegrationEvents.Events;
using ezLoyalty.Services.Incentive.API.Extensions;
using ezLoyalty.Services.Incentive.API.Infrastructure.AutoFacModules;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

namespace ezLoyalty.Services.Incentive.API
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
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            //builder.Services.AddDbContext<IncentiveDbContext>(options => options.UseInMemoryDatabase("items"));
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            services.AddCustomDbContext(Configuration);
            services
                .AddIntegrationServices(Configuration)
                .AddEventBus(Configuration)
                .AddSwagger()
                .AddHealthChecks(Configuration);

            services.AddSingleton<ITelemetryInitializer>(new RoleNameInitializer(Program.AppName));

            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ApplicationModule());

            return new AutofacServiceProvider(container.Build());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
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
                 c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "IncentivesAPI V1");
             });

            app.UseForwardedHeaders();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

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
            eventBus.Subscribe<ActionStatusChangedToAwaitingRewardsIntegrationEvent, ActionStatusChangedToAwaitingRewardsIntegrationEventHandler>();
        }
    }
}
