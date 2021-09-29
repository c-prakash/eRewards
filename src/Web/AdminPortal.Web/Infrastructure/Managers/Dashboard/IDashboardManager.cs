using AdminPortal.Web.Shared.Wrapper;
using System.Threading.Tasks;
using AdminPortal.Web.Application.Features.Dashboards.Queries.GetData;

namespace AdminPortal.Web.Client.Infrastructure.Managers.Dashboard
{
    public interface IDashboardManager : IManager
    {
        Task<IResult<DashboardDataResponse>> GetDataAsync();
    }
}