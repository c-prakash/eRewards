using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezLoyalty.Services.Actions.API.Application.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class SetProductEligibilityRejectedActionStatusCommand : IRequest<bool>
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
        public SetProductEligibilityRejectedActionStatusCommand(int accountNo, int actionId)
        {
            AccountNo = accountNo;
            ActionId = actionId;
        }
    }
}
