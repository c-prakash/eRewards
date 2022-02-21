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
        public int ActionId { get; set; }

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

        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid RequestId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionId"></param>
        /// <param name="token"></param>
        /// <param name="accountNo"></param>
        /// <param name="userId"></param>
        /// <param name="payload"></param>
        /// <param name="sender"></param>
        public ActionReceivedIntegrationEvents(int actionId, string token, int accountNo, string userId, string payload, string sender, DateTime createdAt)
        {
            ActionId = actionId;
            UniqueToken = token;
            AccountNo = accountNo;
            UserID = userId;
            Payload = payload;
            Sender = sender;
            CreatedAt = createdAt;
        }
    }
}
