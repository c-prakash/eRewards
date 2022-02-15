using Autofac;
using ezLoyalty.Services.Incentive.API.Application.Behavior;
using ezLoyalty.Services.Incentive.API.Application.Commands;
using ezLoyalty.Services.Incentive.API.Application.DomainEventHandlers;
using MediatR;
using System.Reflection;

namespace ezLoyalty.Services.Incentive.API.Infrastructure.AutoFacModules
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(AddPointsCommand).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            // Register the DomainEventHandler classes (they implement INotificationHandler<>) in assembly holding the Domain Events
            builder.RegisterAssemblyTypes(typeof(PointsAwardedDomainEventHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>));

            /*  // Register the Command's Validators (Validators based on FluentValidation library)
             builder
                 .RegisterAssemblyTypes(typeof(CreateOrderCommandValidator).GetTypeInfo().Assembly)
                 .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                 .AsImplementedInterfaces();
            */

            builder.Register<ServiceFactory>(context =>
             {
                 var componentContext = context.Resolve<IComponentContext>();
                 return t =>
                 {
                     try
                     {
                         object o;
                         return componentContext.TryResolve(t, out o) ? o : null;
                     }
                     catch (Exception ex)
                     {
                         return null;
                     }
                 };
             });

            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(TransactionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}