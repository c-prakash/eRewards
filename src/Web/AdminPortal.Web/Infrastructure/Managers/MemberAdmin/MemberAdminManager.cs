using AdminPortal.Web.Client.Infrastructure.Extensions;
using AdminPortal.Web.Shared.Models;
using AdminPortal.Web.Shared.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AdminPortal.Web.Client.Infrastructure.Managers.MemberAdmin
{
    public class MemberAdminManager : IMemberAdminManager
    {
        private readonly HttpClient _httpClient;

        public MemberAdminManager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<int>> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{Routes.MemberAdminEndpoints.Delete}/{id}");
            return await response.ToResult<int>();
        }

        public async Task<PaginatedItemsViewModel<Member>> GetAllMembers(int pageNumber, int pageSize,  string _searchString, string[] orderings)
        {
            var response = await _httpClient.GetStreamAsync(Routes.MemberAdminEndpoints.GetAllPaged(pageNumber, pageSize, _searchString, orderings));
            return await JsonSerializer.DeserializeAsync<PaginatedItemsViewModel<Member>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Member> GetMemberDetails(int id)
        {
            return await JsonSerializer.DeserializeAsync<Member>
                (await _httpClient.GetStreamAsync($"{Routes.MemberAdminEndpoints.GetAccount}/{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }


        public async Task<Member> AddMember(Member member)
        {
            var memberJson =
                new StringContent(JsonSerializer.Serialize(member), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(Routes.MemberAdminEndpoints.Save, memberJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Member>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }

        public async Task UpdateMember(Member member)
        {
            var memberJson =
                new StringContent(JsonSerializer.Serialize(member), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync(Routes.MemberAdminEndpoints.Save, memberJson);
        }
    }
}
