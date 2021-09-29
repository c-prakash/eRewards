using AdminPortal.Web.Application.Responses.Identity;
using AdminPortal.Web.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdminPortal.Web.Application.Interfaces.Chat;
using AdminPortal.Web.Application.Models.Chat;

namespace AdminPortal.Web.Application.Interfaces.Services
{
    public interface IChatService
    {
        Task<Result<IEnumerable<ChatUserResponse>>> GetChatUsersAsync(string userId);

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> message);

        Task<Result<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string userId, string contactId);
    }
}