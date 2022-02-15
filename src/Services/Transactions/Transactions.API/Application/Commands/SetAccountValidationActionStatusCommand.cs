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
    public class SetAccountValidationActionStatusCommand : IRequest<bool>
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
        public bool Status { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="actionId"></param>
        public SetAccountValidationActionStatusCommand(int accountNo, int actionId)
        {
            AccountNo = accountNo;
            ActionId = actionId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="actionId"></param>
        /// <param name="status"></param>
        public SetAccountValidationActionStatusCommand(int accountNo, int actionId, bool status)
            : this(accountNo, actionId)
        {
            Status = status;
        }
    }
}
