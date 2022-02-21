using ezLoyalty.Services.Actions.Domain.ActionsAggregate;
using ezLoyalty.Services.Actions.Infrastructure;
using IntegrationEventLogEF;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ezLoyalty.Services.Actions.API.Extensions
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
            //integrationLogContext.Database.EnsureDeleted();
            actionDbContext.Database.EnsureCreated();
            integrationLogContext.Database.EnsureCreated();

            using (actionDbContext)
            {
                if (!actionDbContext.ActionStatus.Any())
                {
                    actionDbContext.ActionStatus.AddRange(GetPredefinedActionStatus());
                }

                if (!actionDbContext.ActionMetadata.Any())
                {
                    actionDbContext.ActionMetadata.AddRange(GetDefaultActions());
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
                ActionStatus.AwaitingRewards,
                ActionStatus.Rewarded,
                ActionStatus.AccountRejected,
                ActionStatus.EligibilityRejected,
                ActionStatus.RewardsRejected
            };
        }
        private static IEnumerable<ActionMetadata> GetDefaultActions()
        {
            return new List<ActionMetadata>()
            {
                new ActionMetadata(){ Name="facebook_like", Description="Facebook like on the Channel",  PossiblePoints=10, EffectiveFrom= new DateTime(2022, 1, 1), EffectiveTo = DateTime.MaxValue, CreatedBy ="system", CreatedDate =new DateTime(2022, 1, 1) },
                new ActionMetadata(){ Name="walk_everyday", Description="Walk everyday activity",  PossiblePoints=5, EffectiveFrom= new DateTime(2020, 1, 1), EffectiveTo = DateTime.MaxValue, CreatedBy ="system", CreatedDate =new DateTime(2020, 1, 1) },
                new ActionMetadata(){ Name="challenge_participation", Description="Online challenge participation",  PossiblePoints=100, EffectiveFrom= new DateTime(2022, 1, 1), EffectiveTo = DateTime.MaxValue, CreatedBy ="system", CreatedDate =new DateTime(2022, 1, 1) },
                new ActionMetadata(){ Name="birthday", Description="Customers birthday",  PossiblePoints=20, EffectiveFrom= new DateTime(2020, 1, 1), EffectiveTo = DateTime.MaxValue, CreatedBy ="system", CreatedDate =new DateTime(2020, 1, 1) },
                new ActionMetadata(){ Name="anniversary_bonus", Description="Customers anniversary", PossiblePoints=50, EffectiveFrom= new DateTime(2021, 1, 1), EffectiveTo = DateTime.MaxValue, CreatedBy ="system", CreatedDate =new DateTime(2021, 1, 1) },
                new ActionMetadata(){ Name="custom_activity", Description="Generic custom activity", PossiblePoints=200, EffectiveFrom= new DateTime(2020, 1, 1), EffectiveTo = DateTime.MaxValue, CreatedBy ="system", CreatedDate =new DateTime(2020, 1, 1) },
            };
        }
    }
}
