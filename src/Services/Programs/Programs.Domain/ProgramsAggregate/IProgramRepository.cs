using ezloyalty.Services.Programs.Domain.Seedwork;
using System.Threading.Tasks;

namespace ezloyalty.Services.Programs.Domain.ProgramsAggregate
{
    //This is just the RepositoryContracts or Interface defined at the Domain Layer
    //as requisite for the Order Aggregate

    public interface IProgramRepository : IRepository<Program>
    {
        Task<Program> Add(Program program);

        Task Update(Program program);


        Task<Program> GetAsync(int programId);
    }
}
