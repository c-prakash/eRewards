using Autofac;
using ezLoyalty.Services.Incentive.Domain.PointsAggregate;
using ezLoyalty.Services.Incentive.Infrastructure.Repositories;

namespace ezLoyalty.Services.Incentive.API.Infrastructure.AutoFacModules
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationModule
         : Module
    {

        public ApplicationModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PointsRepository>()
                .As<IPointsRepository>()
                .InstancePerLifetimeScope();

        }
    }
}
