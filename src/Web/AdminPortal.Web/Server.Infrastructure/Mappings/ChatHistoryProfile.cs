using AutoMapper;
using AdminPortal.Web.Application.Interfaces.Chat;
using AdminPortal.Web.Application.Models.Chat;
using AdminPortal.Web.Infrastructure.Models.Identity;

namespace AdminPortal.Web.Infrastructure.Mappings
{
    public class ChatHistoryProfile : Profile
    {
        public ChatHistoryProfile()
        {
            CreateMap<ChatHistory<IChatUser>, ChatHistory<BlazorHeroUser>>().ReverseMap();
        }
    }
}