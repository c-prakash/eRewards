using AdminPortal.Web.Application.Interfaces.Common;

namespace AdminPortal.Web.Application.Interfaces.Services
{
    public interface ICurrentUserService : IService
    {
        string UserId { get; }
    }
}