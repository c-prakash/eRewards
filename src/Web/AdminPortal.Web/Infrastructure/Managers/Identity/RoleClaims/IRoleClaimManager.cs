using System.Collections.Generic;
using System.Threading.Tasks;
using AdminPortal.Web.Application.Requests.Identity;
using AdminPortal.Web.Application.Responses.Identity;
using AdminPortal.Web.Shared.Wrapper;

namespace AdminPortal.Web.Client.Infrastructure.Managers.Identity.RoleClaims
{
    public interface IRoleClaimManager : IManager
    {
        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsAsync();

        Task<IResult<List<RoleClaimResponse>>> GetRoleClaimsByRoleIdAsync(string roleId);

        Task<IResult<string>> SaveAsync(RoleClaimRequest role);

        Task<IResult<string>> DeleteAsync(string id);
    }
}