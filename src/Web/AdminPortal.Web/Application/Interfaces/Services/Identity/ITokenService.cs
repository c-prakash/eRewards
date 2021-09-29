using AdminPortal.Web.Application.Interfaces.Common;
using AdminPortal.Web.Application.Requests.Identity;
using AdminPortal.Web.Application.Responses.Identity;
using AdminPortal.Web.Shared.Wrapper;
using System.Threading.Tasks;

namespace AdminPortal.Web.Application.Interfaces.Services.Identity
{
    public interface ITokenService : IService
    {
        Task<Result<TokenResponse>> LoginAsync(TokenRequest model);

        Task<Result<TokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest model);
    }
}