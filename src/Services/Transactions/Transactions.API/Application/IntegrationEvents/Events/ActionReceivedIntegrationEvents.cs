using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Events;
using System;

namespace ezLoyalty.Services.Actions.API.Application.IntegrationEvents.Events
{
    /// <summary>
    /// 
    /// </summary>
    public record ActionReceivedIntegrationEvents : IntegrationEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UniqueToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int AccountNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Payload { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid RequestId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="token"></param>
        /// <param name="accountNo"></param>
        /// <param name="userId"></param>
        /// <param name="payload"></param>
        /// <param name="sender"></param>
        public ActionReceivedIntegrationEvents(string actionName, string token, int accountNo, string userId, string payload, string sender)
        {
            Name = actionName;
            UniqueToken = token;
            AccountNo = accountNo;
            UserID = userId;
            Payload = payload;
            Sender = sender;
        }
    }
}
