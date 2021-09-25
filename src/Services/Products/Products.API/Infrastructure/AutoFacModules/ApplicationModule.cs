using Autofac;
using eRewards.Services.Products.Domain.ProductsAggregate;
using eRewards.Services.Products.Infrastructure.Repositories;

namespace eRewards.Services.Products.API.Infrastructure.AutoFacModules
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
            builder.RegisterType<ProductRepository>()
                .As<IProductRepository>()
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
