using IntegrationEventLogEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;


namespace eRewards.Services.Transactions.API.Infrastructure.IntegrationEventMigrations
{
    /// <summary>
    /// 
    /// </summary>
    [DbContext(typeof(IntegrationEventLogContext))]
    partial class IntegrationEventLogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.eShopOnContainers.BuildingBlocks.IntegrationEventLogEF.IntegrationEventLogEntry", b =>
            {
                b.Property<Guid>("EventId")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Content")
                    .IsRequired();

                b.Property<DateTime>("CreationTime");

                b.Property<string>("EventTypeName")
                    .IsRequired();

                b.Property<int>("State");

                b.Property<int>("TimesSent");

                b.Property<string>("TransactionId");

                b.HasKey("EventId");

                b.ToTable("IntegrationEventLog");
            });
#pragma warning restore 612, 618
        }
    }
}
