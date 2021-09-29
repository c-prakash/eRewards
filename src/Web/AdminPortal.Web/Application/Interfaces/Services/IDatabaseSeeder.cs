using System.Threading.Tasks;

namespace AdminPortal.Web.Application.Interfaces.Services
{
    public interface IDatabaseSeeder
    {
        Task Initialize();
    }
}