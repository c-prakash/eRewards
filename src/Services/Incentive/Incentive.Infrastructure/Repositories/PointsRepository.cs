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
    public class PointsRepository : IPointsRepository
    {
        private readonly IncentiveDbContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public PointsRepository(IncentiveDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Points Add(Points point)
        {
            return _context.Points.Add(point).Entity;
        }
        public void Update(Points point)
        {
            _context.Entry(point).State = EntityState.Modified;
        }

        //public async Task<IEnumerable<Points>> GetAsync(int accountNo)
        //{
        //    var allPoints = _context.Points.Where(o => o.AccountNo == accountNo);

        //    return await allPoints.ToListAsync();
        //}

        //public async Task<IEnumerable<Points>> GetAsync(int accountNo, int actionId)
        //{
        //    var allPoints = _context.Points.Where(o => o.AccountNo == accountNo && o.ActionId == actionId);

        //    return await allPoints.ToListAsync();
        //}

        public async Task<IEnumerable<Points>> GetAsync(int accountNo, int actionId, DateTime dateCondition)
        {
            var allPoints = _context.Points.Where(o => o.AccountNo == accountNo);

            if(actionId != 0)
                allPoints = allPoints.Where(a=> a.ActionId == actionId);

            if(dateCondition != DateTime.MinValue)
                allPoints = allPoints.Where(a => a.EarnedDate == dateCondition);

            return await allPoints.ToListAsync();
        }

        public async Task<Points> GetAsync(int accountNo, int actionId, int actionRecordId)
        {
            var resultPoint = await _context.Points.FirstOrDefaultAsync(o => o.ActionId == actionId && o.AccountNo == accountNo && o.ActionRecordId == actionRecordId);

            if (resultPoint is null)
            {
                resultPoint = _context
                            .Points
                            .Local
                            .FirstOrDefault(o => o.ActionId == actionId && o.AccountNo == accountNo && o.ActionRecordId == actionRecordId);
            }

            return resultPoint;
        }
    }
}
