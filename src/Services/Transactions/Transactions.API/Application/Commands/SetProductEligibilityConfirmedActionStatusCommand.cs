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
    public class SetProductEligibilityConfirmedActionStatusCommand : IRequest<bool>
    {
        /// <summary>
        /// 
        /// </summary>
        public int AccountNo { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public int ActionRecordId { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNo"></param>
        /// <param name="recordId"></param>
        public SetProductEligibilityConfirmedActionStatusCommand(int accountNo, int recordId)
        {
            AccountNo = accountNo;
            ActionRecordId = recordId;
        }
    }
}
