using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;
using System.IO;

namespace ezLoyalty.Services.Accounts.API.Extensions
{
    public static class IConfigurationExtensions
    {
        public static IConfiguration CreateConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }

        public static Logger AddSerilogConfiguration(this IConfiguration configuration, string appName)
        {
            var instrumentationKey = configuration["APPINSIGHTS_INSTRUMENTATIONKEY"];

            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .Enrich.WithProperty("ApplicationContext", appName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.ApplicationInsights(instrumentationKey, TelemetryConverter.Traces)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}