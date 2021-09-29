using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminPortal.Web.Client.Infrastructure.Routes
{
    public class MemberAdminEndpoints
    {
        public const string RootEndpoint = "https://localhost:44300/";

        public static string GetAllPaged(int pageNumber, int pageSize, string searchString, string[] orderBy)
        {
            var url = $"api/v1/accounts?pageNumber={pageNumber}&pageSize={pageSize}&searchString={searchString}&orderBy=";
            if (orderBy?.Any() == true)
            {
                foreach (var orderByPart in orderBy)
                {
                    url += $"{orderByPart},";
                }
                url = url[..^1]; // loose training ,
            }
            return url;
        }

        public static string GetAll = "api/v1/accounts";
        public static string GetAccount = "api/v1/accounts";
        public static string GetCount = "api/v1/accounts/count";
        public static string Save = "api/v1/accounts";
        public static string Delete = "api/v1/accounts";
    }
}
