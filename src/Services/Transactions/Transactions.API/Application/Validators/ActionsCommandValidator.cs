using eRewards.Services.Transactions.API.Application.Commands;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace eRewards.Services.Transactions.API.Application.Validators
{

    public class ActionsCommandValidator : AbstractValidator<ActionsCommand>
    {
        public ActionsCommandValidator(ILogger<ActionsCommand> logger)
        {
            RuleFor(command => command.Name).NotEmpty();
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
