using MediatR;
using System;

namespace eRewards.Services.Accounts.Domain.Events
{
    public class AccountCreatedDomainEvent : INotification
    {
        public string MerchandId { get; set; }
        public string EventId { get; set; }

        public DateTime CreatedAt { get; set; }

        public AccountCreatedDomainEvent()
        {
           
        }
    }
}