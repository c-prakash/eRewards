using AdminPortal.Web.Application.Requests.Mail;
using System.Threading.Tasks;

namespace AdminPortal.Web.Application.Interfaces.Services
{
    public interface IMailService
    {
        Task SendAsync(MailRequest request);
    }
}