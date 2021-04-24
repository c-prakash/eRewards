using eRewards.Services.Accounts.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eRewards.Services.Accounts.Domain.AccountsAggregate
{
    public class Account
        : Entity, IAggregateRoot
    {
        
        public List<AccountMapping> Mappings { get; set; }

        public int? Balance { get; set; }

        public int? LifetimePoints { get; set; }

        public string CustomerId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Account() 
        {
            this.CreatedAt = DateTime.UtcNow;
        }

    }

    public class AccountMapping
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }

        public string Value { get; set; }

        public DateTime CreatedAt { get; set; }

        public Account Account { get; set; }

        public AccountMapping()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}
