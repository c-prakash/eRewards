using eRewards.Services.Transactions.Domain.ActionsAggregate;
using eRewards.Services.Transactions.Infrastructure;
using IntegrationEventLogEF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace eRewards.Services.Transactions.API.Extensions
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
            var actionDbContext = scope.ServiceProvider.GetRequiredService<ActionsDbContext>();
            var integrationLogContext = scope.ServiceProvider.GetRequiredService<IntegrationEventLogContext>();

            //actionDbContext.Database.EnsureDeleted();
            actionDbContext.Database.EnsureCreated();
            integrationLogContext.Database.EnsureCreated();
            //if (context.Database.EnsureCreated())
            //    SeedData.Initialize(context);

            using (actionDbContext)
            {
                if (!actionDbContext.ActionStatus.Any())
                {
                    actionDbContext.ActionStatus.AddRange(GetPredefinedActionStatus());
                }

                actionDbContext.SaveChanges();

            }


            return host;
        }

        private static IEnumerable<ActionStatus> GetPredefinedActionStatus()
        {
            return new List<ActionStatus>()
            {
                ActionStatus.Submitted,
                ActionStatus.AwaitingAccountValidation,
                ActionStatus.AwaitingEligibilityValidation,
                ActionStatus.Rewarded
            };
        }
    }
}
