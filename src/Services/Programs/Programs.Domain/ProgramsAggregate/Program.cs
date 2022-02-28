using ezloyalty.Services.Programs.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ezloyalty.Services.Programs.Domain.ProgramsAggregate
{
    public class Program
        : Entity, IAggregateRoot
    {
        
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public Program() 
        {
            this.CreatedAt = DateTime.UtcNow;
        }

    }
}
