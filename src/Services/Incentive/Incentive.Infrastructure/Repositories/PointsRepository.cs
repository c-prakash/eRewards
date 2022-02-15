using ezLoyalty.Services.Incentive.Domain.PointsAggregate;
using ezLoyalty.Services.Incentive.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ezLoyalty.Services.Incentive.Infrastructure.Repositories
{
    public class PointsRepository
        : IPointsRepository
    {
        private readonly IncentiveDbContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        //IUnitOfWork IRepository<Points>.UnitOfWork => throw new NotImplementedException();

        public PointsRepository(IncentiveDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Points Add(Points point)
        {
            return _context.Points.Add(point).Entity;
        }

        public async Task<IEnumerable<Points>> GetByAccount(int accountNo)
        {
            var allPoints = _context.Points.Where(o => o.AccountNo == accountNo);


            return await allPoints.ToListAsync();
        }

        public async Task<Points> GetAsync(int accountNo, int actionId)
        {
            var resultPoint = await _context.Points.FirstOrDefaultAsync(o => o.ActionId == actionId);

            if (resultPoint == null)
            {
                resultPoint = _context
                            .Points
                            .Local
                            .FirstOrDefault(o => o.ActionId == actionId && o.AccountNo == accountNo );
            }

            return resultPoint;

        }
                 
        public void Update(Points point)
        {
            _context.Entry(point).State = EntityState.Modified;
        }
    }
}
