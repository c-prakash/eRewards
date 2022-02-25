using Autofac;
using ezLoyalty.Services.Accounts.Domain.AccountsAggregate;
using ezLoyalty.Services.Accounts.Infrastructure.Repositories;

namespace ezLoyalty.Services.Accounts.API.Infrastructure.AutoFacModules
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationModule
         : Module
    {
        /// <summary>
        /// 
        /// </summary>
        public ApplicationModule()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountRepository>()
                .As<IAccountRepository>()
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
