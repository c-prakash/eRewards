using eRewards.Services.Transactions.Domain.ActionsAggregate;
using eRewards.Services.Transactions.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eRewards.Services.Transactions.Infrastructure.Repositories
{

    public class ActionsRepository
        : IActionsRepository
    {
        private readonly ActionsDbContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public ActionsRepository(ActionsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Actions Add(Actions actions)
        {
            return _context.Actions.Add(actions).Entity;
        }

        public async Task<IEnumerable<Actions>> GetByAccount(int accountNo)
        {
            var allActions = _context.Actions.Where(o => o.AccountNo == accountNo);
                       

            return await allActions.ToListAsync();
        }

        public async Task<Actions> GetAsync(int actionId)
        {
            var action = await _context.Actions.FirstOrDefaultAsync(o => o.Id == actionId);
            
            if (action == null)
            {
                action = _context
                            .Actions
                            .Local
                            .FirstOrDefault(o => o.Id == actionId);
            }
            
            return action;

        }

        public void Update(Actions actions)
        {
            _context.Entry(actions).State = EntityState.Modified;
        }
    }
}
