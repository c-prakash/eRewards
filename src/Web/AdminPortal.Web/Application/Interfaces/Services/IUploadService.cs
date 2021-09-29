using AdminPortal.Web.Application.Requests;

namespace AdminPortal.Web.Application.Interfaces.Services
{
    public interface IUploadService
    {
        string UploadAsync(UploadRequest request);
    }
}