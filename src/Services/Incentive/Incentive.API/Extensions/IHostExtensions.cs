using ezLoyalty.Services.Incentive.Infrastructure;
using IntegrationEventLogEF;

namespace ezLoyalty.Services.Incentive.API.Extensions
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
            var incentiveDbContext = scope.ServiceProvider.GetRequiredService<IncentiveDbContext>();
            var integrationLogContext = scope.ServiceProvider.GetRequiredService<IntegrationEventLogContext>();

            //actionDbContext.Database.EnsureDeleted();
            incentiveDbContext.Database.EnsureCreated();
            integrationLogContext.Database.EnsureCreated();
            //if (context.Database.EnsureCreated())
            //    SeedData.Initialize(context);

            return host;
        }
    }
}
