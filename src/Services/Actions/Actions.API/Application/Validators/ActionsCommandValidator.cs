using ezLoyalty.Services.Actions.API.Application.Commands;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ezLoyalty.Services.Actions.API.Application.Validators
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionsCommandValidator : AbstractValidator<NewActionCommand>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ActionsCommandValidator(ILogger<NewActionCommand> logger)
        {
            RuleFor(command => command.ActionId).NotEmpty();
            RuleFor(command => command.UniqueToken).NotEmpty();
            RuleFor(command => command.UserID).NotEmpty();
            RuleFor(command => command.AccountNo).NotEmpty();
            RuleFor(command => command.Sender).NotEmpty();
            /*RuleFor(command => command).NotEmpty().Length(12, 19);
            RuleFor(command => command.CardHolderName).NotEmpty();
            RuleFor(command => command.CardExpiration).NotEmpty().Must(BeValidExpirationDate).WithMessage("Please specify a valid card expiration date");
            RuleFor(command => command.CardSecurityNumber).NotEmpty().Length(3);
            RuleFor(command => command.CardTypeId).NotEmpty();
            RuleFor(command => command.OrderItems).Must(ContainOrderItems).WithMessage("No order items found");*/

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }

        //private bool BeValidExpirationDate(DateTime dateTime)
        //{
        //    return dateTime >= DateTime.UtcNow;
        //}

        //private bool ContainOrderItems(IEnumerable<OrderItemDTO> orderItems)
        //{
        //    return orderItems.Any();
        //}
    }
}
