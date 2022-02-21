using ezLoyalty.Services.Incentive.Domain.Seedwork;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ezLoyalty.Services.Incentive.Domain.PointsAggregate
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Order Aggregate

    public interface IPointsRepository : IRepository<Points>
    {
        Points Add(Points point);

        void Update(Points point);

        Task<IEnumerable<Points>> GetByAccount(int accountNo);

        Task<IEnumerable<Points>> GetAsync(int accountNo, int actionId);

        Task<Points> GetAsync(int accountNo, int actionId, int actionRecordId);
    }
}
