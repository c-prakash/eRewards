using ezLoyalty.Services.Incentive.API;
using ezLoyalty.Services.Incentive.API.Extensions;
using Microsoft.AspNetCore;
using Serilog;

//var builder = WebApplication.CreateBuilder(args);

//builder.Configuration.AddSerilogConfiguration(Program.AppName);

//builder.Host.UseSerilog((ctx, lc) => lc
//    .ReadFrom.Configuration(ctx.Configuration));
//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
////builder.Services.AddDbContext<IncentiveDbContext>(options => options.UseInMemoryDatabase("items"));
//builder.Services.AddCustomDbContext(builder.Configuration);
//builder.Services
//    .AddIntegrationServices(builder.Configuration)
//    .AddEventBus(builder.Configuration)
//    .AddSwagger()
//    .AddHealthChecks()
//    .AddCheck("self", () => HealthCheckResult.Healthy());

//builder.Host
//    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
//    .ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new MediatorModule()))
//    .ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new ApplicationModule()));


//var app = builder.Build();


//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseSerilogRequestLogging();

//app.UseHttpsRedirection();

//app.UseRouting();

//app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();

//    endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
//    {
//        Predicate = _ => true,
//        //ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
//    });
//    endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
//    {
//        Predicate = r => r.Name.Contains("self")
//    });
//});

////var eventBus = (app as IApplicationBuilder).ApplicationServices.GetRequiredService<IEventBus>();
////eventBus.Subscribe<ActionsPublishedIntegrationEvent, ActionsPublishedIntegrationEventHandler>();
////eventBus.Subscribe<ActionAccountValidationCompleteIntegrationEvent, ActionAccountValidationCompleteIntegrationEventHandler>();
////eventBus.Subscribe<ProductEligibilityConfirmedIntegrationEvent, ProductEligibilityConfirmedIntegrationEventHandler>();
////eventBus.Subscribe<ProductEligibilityRejectedIntegrationEvent, ProductEligibilityRejectedIntegrationEventHandler>();

//app.Run();


//----------------------------

var configuration = GetConfiguration();

Log.Logger = CreateSerilogLogger(configuration);

try
{
    Log.Information("Configuring web host ({ApplicationContext})...", Program.AppName);
    var host = BuildWebHost(configuration, args);

    Log.Information("Applying migrations ({ApplicationContext})...", Program.AppName);
    host.SeedDatabase();
    
    Log.Information("Starting web host ({ApplicationContext})...", Program.AppName);
    host.Run();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", Program.AppName);
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .CaptureStartupErrors(false)
        .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
        .UseStartup<Startup>()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseSerilog()
        .Build();

Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
{
    return new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .Enrich.WithProperty("ApplicationContext", Program.AppName)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    //var config = builder.Build();

    //if (config.GetValue<bool>("UseVault", false))
    //{
    //    TokenCredential credential = new ClientSecretCredential(
    //        config["Vault:TenantId"],
    //        config["Vault:ClientId"],
    //        config["Vault:ClientSecret"]);
    //    builder.AddAzureKeyVault(new Uri($"https://{config["Vault:Name"]}.vault.azure.net/"), credential);
    //}

    return builder.Build();
}


public partial class Program
{
    public static readonly string AppName = "Incentive.API";
}