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

        // ActionMetadata

        public ActionMetadata Create(ActionMetadata action)
        {
            return _context.ActionMetadata.Add(action).Entity;
        }

        public void Update(ActionMetadata action)
        {
            _context.Entry(action).State = EntityState.Modified;
        }

        public async Task<IEnumerable<ActionMetadata>> GetAllActionMetadataAsync()
        {
            var allActions = _context.ActionMetadata;

            return await allActions.ToListAsync();
        }

        public async Task<ActionMetadata> GetActionMetadatAsync(int actionId)
        {
            var actionMetadataInstance = await _context.ActionMetadata.FirstOrDefaultAsync(o => o.Id == actionId);

            return actionMetadataInstance;
        }

        // Actions
        public Action Add(Action actionInstance)
        {
            return _context.Actions.Add(actionInstance).Entity;
        }

        public async Task<IEnumerable<Action>> GetByAccount(int accountNo)
        {
            var allActions = _context.Actions.Where(o => o.AccountNo == accountNo);


            return await allActions.ToListAsync();
        }

        public async Task<IEnumerable<Action>> GetByAccount(int accountNo, int actionId)
        {
            var allActions = _context.Actions.Where(o => o.AccountNo == accountNo && o.ActionId == actionId);


            return await allActions.ToListAsync();
        }

        public async Task<Action> GetAsync(int actionRecordId)
        {
            var action = await _context.Actions.FirstOrDefaultAsync(o => o.Id == actionRecordId);

            if (action == null)
            {
                action = _context
                            .Actions
                            .Local
                            .FirstOrDefault(o => o.Id == actionRecordId);
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
