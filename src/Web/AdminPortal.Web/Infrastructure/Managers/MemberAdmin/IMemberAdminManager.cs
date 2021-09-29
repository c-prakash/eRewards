using AdminPortal.Web.Shared.Models;
using AdminPortal.Web.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPortal.Web.Client.Infrastructure.Managers.MemberAdmin
{
    public interface IMemberAdminManager
    {

        Task<IResult<int>> DeleteAsync(int id);

        Task<PaginatedItemsViewModel<Member>> GetAllMembers(int pageNumber, int pageSize, string _searchString, string[] orderings);

        Task<Member> GetMemberDetails(int id);

        Task<Member> AddMember(Member member);

        Task UpdateMember(Member member);

    }
}
