using eRewards.Services.Accounts.Domain.AccountsAggregate;
using eRewards.Services.Accounts.Domain.Seedwork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace eRewards.Services.Accounts.Infrastructure
{
    public class AccountDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountMapping> AccountMappings { get; set; }

        private readonly IMediator _mediator;
        private IDbContextTransaction _currentTransaction;

        public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

        public bool HasActiveTransaction => _currentTransaction != null;

        public AccountDbContext(DbContextOptions<AccountDbContext> options, IMediator mediator) 
            : base(options) 
        {

            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            System.Diagnostics.Debug.WriteLine("ActionsDbContext::ctor ->" + this.GetHashCode());
        }
               
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);

            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null) return null;

            _currentTransaction = await Database.BeginTransactionAsync();

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                transaction.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
