using MediatR;

namespace ezLoyalty.Services.Actions.API.Application.Commands
{
    /// <summary>
    /// SetRewardsRejectedActionStatusCommand
    /// </summary>
    public class SetRewardsRejectedActionStatusCommand : IRequest<bool>
    {
        /// <summary>
        /// 
        /// </summary>
        public int AccountNo { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int ActionId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="actionId"></param>
        public SetRewardsRejectedActionStatusCommand(int accountNo, int actionId)
        {
            AccountNo = accountNo;
            ActionId = actionId;
        }
    }
}
