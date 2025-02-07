﻿using ezLoyalty.Services.Actions.Domain.ActionsAggregate;
using ezLoyalty.Services.Actions.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ezLoyalty.Services.Actions.Infrastructure.EntityConfiguration
{
    class ActionStatusEntityTypeConfiguration
    : IEntityTypeConfiguration<ActionStatus>
    {
        public void Configure(EntityTypeBuilder<ActionStatus> actionStatusConfiguration)
        {
            actionStatusConfiguration.ToTable("ActionStatus", ActionsDbContext.DEFAULT_SCHEMA);

            actionStatusConfiguration.HasKey(o => o.Id);

            actionStatusConfiguration.Property(o => o.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            actionStatusConfiguration.Property(o => o.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
