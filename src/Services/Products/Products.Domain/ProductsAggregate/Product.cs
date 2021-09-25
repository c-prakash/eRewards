using eRewards.Services.Products.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eRewards.Services.Products.Domain.ProductsAggregate
{
    public class Product
        : Entity, IAggregateRoot
    {
        
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Product() 
        {
            this.CreatedAt = DateTime.UtcNow;
        }

    }
}
