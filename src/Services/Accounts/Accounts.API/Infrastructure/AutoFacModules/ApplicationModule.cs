using Autofac;
using eRewards.Services.Accounts.Domain.AccountsAggregate;
using eRewards.Services.Accounts.Infrastructure.Repositories;

namespace eRewards.Services.Accounts.API.Infrastructure.AutoFacModules
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationModule
         : Autofac.Module
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
