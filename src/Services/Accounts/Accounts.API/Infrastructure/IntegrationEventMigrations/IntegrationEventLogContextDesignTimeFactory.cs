using IntegrationEventLogEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace eRewards.Services.Accounts.API.Infrastructure.IntegrationEventMigrations
{
    /// <summary>
    /// 
    /// </summary>
    public class IntegrationEventLogContextDesignTimeFactory : IDesignTimeDbContextFactory<IntegrationEventLogContext>
    {
        public IntegrationEventLogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IntegrationEventLogContext>();

            optionsBuilder.UseSqlServer(".", options => options.MigrationsAssembly(GetType().Assembly.GetName().Name));

            return new IntegrationEventLogContext(optionsBuilder.Options);
        }
    }
}
