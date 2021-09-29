using System.Collections.Generic;
using System.Threading.Tasks;
using AdminPortal.Web.Application.Interfaces.Common;
using AdminPortal.Web.Application.Requests.Identity;
using AdminPortal.Web.Application.Responses.Identity;
using AdminPortal.Web.Shared.Wrapper;

namespace AdminPortal.Web.Application.Interfaces.Services.Identity
{
    public interface IRoleClaimService : IService
    {
        Task<Result<List<RoleClaimResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleClaimResponse>> GetByIdAsync(int id);

        Task<Result<List<RoleClaimResponse>>> GetAllByRoleIdAsync(string roleId);

        Task<Result<string>> SaveAsync(RoleClaimRequest request);

        Task<Result<string>> DeleteAsync(int id);
    }
}