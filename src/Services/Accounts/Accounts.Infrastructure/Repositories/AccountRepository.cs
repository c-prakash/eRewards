using eRewards.Services.Accounts.Domain.AccountsAggregate;
using eRewards.Services.Accounts.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRewards.Services.Accounts.Infrastructure.Repositories
{

    public class AccountRepository
        : IAccountRepository
    {
        private readonly AccountDbContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public AccountRepository(AccountDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Account> Add(Account account)
        {
            var result = _context.Accounts.Add(account).Entity;
            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<Account> GetAsync(int accountNo)
        {
            var action = await _context.Accounts.FirstOrDefaultAsync(o => o.Id == accountNo);

            if (action == null)
            {
                action = _context
                            .Accounts
                            .Local
                            .FirstOrDefault(o => o.Id == accountNo);
            }

            return action;

        }

        public async Task Update(Account accountToUpdate)
        {
            //_context.Entry(account).State = EntityState.Modified;
            //await _context.SaveChangesAsync();

            var innerAccount = await _context.Accounts.SingleOrDefaultAsync(o => o.Id == accountToUpdate.Id);
            if (innerAccount != null)
            {
                innerAccount.CustomerId = accountToUpdate.CustomerId;
                innerAccount.UpdatedAt = accountToUpdate.UpdatedAt;

                //_context.Entry(innerAccount).State = EntityState.Modified;
                // await _context.SaveChangesAsync();
                //_context.Accounts.Update(innerAccount);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<long> Count(string searchString)
        {
            var totalItems = new long();
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                totalItems = await _context.Accounts
                .Where(c => c.CustomerId.StartsWith(searchString))
                .LongCountAsync();
            }
            else
            {
                totalItems = await _context.Accounts.LongCountAsync();
            }
            return totalItems;
        }

        public async Task<IEnumerable<Account>> GetAllAsync(string searchString, string orderBy = null, int pageNumber = 0, int pageSize = 10)
        {
            var accountsQuery = _context.Accounts.AsQueryable();

            var acccounts = await accountsQuery
               .Skip(pageSize * pageNumber)
               .Take(pageSize)
               .ToListAsync();

            return acccounts;
        }

        public async Task<int> DeleteAsync(int accountNo)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(o => o.Id == accountNo);

            if (account != null)
            {
                _context.Accounts.Remove(account);

                // or
                // context.Remove<Student>(std);

                _context.SaveChanges();
                return accountNo;
            }

            return 0;

        }
    }
}
