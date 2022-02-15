using Autofac;
using ezLoyalty.Services.Actions.API.Application.Queries;
using ezLoyalty.Services.Actions.Domain.ActionsAggregate;
using ezLoyalty.Services.Actions.Infrastructure.Repositories;

namespace ezLoyalty.Services.Actions.API.Infrastructure.AutoFacModules
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
