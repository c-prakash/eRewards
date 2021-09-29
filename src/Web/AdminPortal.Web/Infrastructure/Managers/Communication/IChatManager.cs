using AdminPortal.Web.Application.Models.Chat;
using AdminPortal.Web.Application.Responses.Identity;
using AdminPortal.Web.Shared.Wrapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdminPortal.Web.Application.Interfaces.Chat;

namespace AdminPortal.Web.Client.Infrastructure.Managers.Communication
{
    public interface IChatManager : IManager
    {
        Task<IResult<IEnumerable<ChatUserResponse>>> GetChatUsersAsync();

        Task<IResult> SaveMessageAsync(ChatHistory<IChatUser> chatHistory);

        Task<IResult<IEnumerable<ChatHistoryResponse>>> GetChatHistoryAsync(string cId);
    }
}