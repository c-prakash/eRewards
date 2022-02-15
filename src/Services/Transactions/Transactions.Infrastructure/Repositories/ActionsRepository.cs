using ezLoyalty.Services.Actions.Domain.ActionsAggregate;
using ezLoyalty.Services.Actions.Domain.Seedwork;
using ezLoyalty.Services.Actions.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ezLoyalty.Services.Actions.Infrastructure.Repositories
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
            _context = context ?? throw new System.ArgumentNullException(nameof(context));
        }

        public Action Add(Action actions)
        {
            return _context.Actions.Add(actions).Entity;
        }

        public async Task<IEnumerable<Action>> GetByAccount(int accountNo)
        {
            var allActions = _context.Actions.Where(o => o.AccountNo == accountNo);


            return await allActions.ToListAsync();
        }

        public async Task<Action> GetAsync(int actionId)
        {
            var action = await _context.Actions.FirstOrDefaultAsync(o => o.Id == actionId);

            if (action == null)
            {
                action = _context
                            .Actions
                            .Local
                            .FirstOrDefault(o => o.Id == actionId);
            }

            if (action != null)
            {
                await _context.Entry(action)
                    .Reference(i => i.ActionStatus).LoadAsync();
            }

            return action;

        }

        public async Task<Action> GetAsync(string token)
        {
            var action = await _context.Actions.FirstOrDefaultAsync(o => o.UniqueToken == token);

            if (action == null)
            {
                action = _context
                            .Actions
                            .Local
                            .FirstOrDefault(o => o.UniqueToken == token);
            }

            if (action != null)
            {
                await _context.Entry(action)
                    .Reference(i => i.ActionStatus).LoadAsync();
            }

            return action;

        }

        public void Update(Action actions)
        {
            _context.Entry(actions).State = EntityState.Modified;
        }
    }
}
