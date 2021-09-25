using eRewards.Services.Products.Infrastructure;
using IntegrationEventLogEF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace eRewards.Services.Products.API.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class IHostExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IWebHost SeedDatabase(this IWebHost host)
        {
            var scopeFactory = host.Services.GetRequiredService<IServiceScopeFactory>();
            using var scope = scopeFactory.CreateScope();
            var productDbContext = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
            var integrationLogContext = scope.ServiceProvider.GetRequiredService<IntegrationEventLogContext>();

            productDbContext.Database.EnsureCreated();
            integrationLogContext.Database.EnsureCreated();
            //if (context.Database.EnsureCreated())
            //    SeedData.Initialize(context);

            return host;
        }
    }
}
