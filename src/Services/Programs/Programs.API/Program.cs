using ezloyalty.Services.Programs.API.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.IO;

namespace ezloyalty.Services.Programs.API
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);
        //public static readonly string AppName = "Programs.API";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var config = IConfigurationExtensions.CreateConfiguration();
            Log.Logger = config.AddSerilogConfiguration(AppName);

            var host = CreateHostBuilder(config, args);

            Log.Information("Starting web host ({ApplicationContext})...", AppName);
            host
                .SeedDatabase()
                .Run();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHost CreateHostBuilder(IConfiguration configuration, string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                 //.ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
                 .ConfigureAppConfiguration((hostContext, builder) =>
                 {
                     // Add other providers for JSON, etc.
                     builder.AddConfiguration(configuration);
                     if (hostContext.HostingEnvironment.IsDevelopment())
                     {
                         builder.AddUserSecrets<Program>();
                     }
                 })
                .CaptureStartupErrors(false)
                .UseStartup<Startup>()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseSerilog()
                .Build();
    }
}
