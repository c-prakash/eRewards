using AdminPortal.Web.Application.Interfaces.Common;
using AdminPortal.Web.Application.Requests.Identity;
using AdminPortal.Web.Application.Responses.Identity;
using AdminPortal.Web.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminPortal.Web.Application.Interfaces.Services.Identity
{
    public interface IRoleService : IService
    {
        Task<Result<List<RoleResponse>>> GetAllAsync();

        Task<int> GetCountAsync();

        Task<Result<RoleResponse>> GetByIdAsync(string id);

        Task<Result<string>> SaveAsync(RoleRequest request);

        Task<Result<string>> DeleteAsync(string id);

        Task<Result<PermissionResponse>> GetAllPermissionsAsync(string roleId);

        Task<Result<string>> UpdatePermissionsAsync(PermissionRequest request);
    }
}