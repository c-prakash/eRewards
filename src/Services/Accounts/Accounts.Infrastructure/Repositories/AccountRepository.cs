using eRewards.Services.Accounts.Domain.AccountsAggregate;
using eRewards.Services.Accounts.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task Update(Account actions)
        {
            _context.Entry(actions).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
