using eRewards.Services.Transactions.Domain.ActionsAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eRewards.Services.Transactions.Infrastructure.EntityConfiguration
{
    class ActionsEntityTypeConfiguration : IEntityTypeConfiguration<Actions>
    {
        public void Configure(EntityTypeBuilder<Actions> actionsConfiguration)
        {
            actionsConfiguration.ToTable("Actions", ActionsDbContext.DEFAULT_SCHEMA);
            
            actionsConfiguration.HasKey(o => o.Id);
            actionsConfiguration.Property(o => o.Id)
                .ValueGeneratedOnAdd();

            actionsConfiguration.Ignore(b => b.DomainEvents);

            actionsConfiguration
                .Property<string>("Name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Name")
                .IsRequired(false);

            actionsConfiguration
                .Property<string>("UniqueToken")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("UniqueToken")
                .IsRequired();

            actionsConfiguration
                .Property<int>("AccountNo")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("AccountNo")
                .IsRequired();

            actionsConfiguration
                .Property<string>("UserID")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("UserID")
                .IsRequired();

            actionsConfiguration
                .Property<string>("Payload")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Payload")
                .IsRequired(false);

            actionsConfiguration
                .Property<string>("Sender")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Sender")
                .IsRequired(false);

            actionsConfiguration
                .Property<DateTime>("CreatedAt")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("CreatedAt")
                .IsRequired();

            actionsConfiguration
                .Property<int>("_actionStatusId")
                // .HasField("_orderStatusId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ActionStatusId")
                .IsRequired();

            actionsConfiguration.HasOne(o => o.ActionStatus)
                .WithMany()
                // .HasForeignKey("OrderStatusId");
                .HasForeignKey("_actionStatusId");
        }
    }
}
