using MediatR;
using System;

namespace ezLoyalty.Services.Accounts.Domain.Events
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