using Autofac;
using eRewards.Services.Transactions.API.Application.Queries;
using eRewards.Services.Transactions.Domain.ActionsAggregate;
using eRewards.Services.Transactions.Infrastructure.Repositories;

namespace eRewards.Services.Transactions.API.Infrastructure.AutoFacModules
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationModule
         : Autofac.Module
    {

        public ApplicationModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ActionsRepository>()
                .As<IActionsRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ActionsQueries>()
              .As<IActionsQueries>()
              .InstancePerLifetimeScope();

            /*
                      builder.RegisterType<RequestManager>()
                         .As<IRequestManager>()
                         .InstancePerLifetimeScope();

                      builder.RegisterAssemblyTypes(typeof(ActionsCommandHandler).GetTypeInfo().Assembly)
                          .AsClosedTypesOf(typeof(IIntegrationEventHandler<>));*/

        }
    }
}
