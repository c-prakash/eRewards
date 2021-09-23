using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRewards.Services.Transactions.API.Application.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class SetAccountValidationActionStatusCommand : IRequest<bool>
    {
        /// <summary>
        /// 
        /// </summary>
        public int AccountId { get; private set; }

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
        /// <param name="accountId"></param>
        /// <param name="actionId"></param>
        public SetAccountValidationActionStatusCommand(int accountId, int actionId)
        {
            AccountId = accountId;
            ActionId = actionId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="actionId"></param>
        /// <param name="status"></param>
        public SetAccountValidationActionStatusCommand(int accountId, int actionId, bool status)
            :this(accountId, actionId)
        {
            Status = status;
        }
    }
}
