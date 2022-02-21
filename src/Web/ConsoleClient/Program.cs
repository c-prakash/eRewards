using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace eRewards.Host.ConsoleClient
{
    class Program
    {
        static HttpClient client = new HttpClient();

        static async Task<HttpStatusCode> CreateActions(Actions recentAction)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/v1/actions/", recentAction);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.StatusCode;
        }

        static void Main(string[] args)
        {
            System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;


            RunAsync().GetAwaiter().GetResult();
        }
        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("https://localhost:44304/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new product
                Actions product = new Actions
                (
                    actionId: 2,
                    token: Guid.NewGuid().ToString(),
                    accountNo: 1,
                    userId: "chandra",
                    payload: "{data:sampledata}",
                    sender: "CLI-Client",
                    createdAt: DateTime.Now
                );

                var statusCode = await CreateActions(product);
                Console.WriteLine($"Create (HTTP Status = {(int)statusCode})");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }


    public class Actions
    {
        public int ActionId { get; set; }

        public string UniqueToken { get; set; }

        public int AccountNo { get; set; }

        public string UserID { get; set; }

        public string Payload { get; set; }

        public string Sender { get; set; }

        public DateTime CreatedAt { get; set; }

        public Actions(int actionId, string token, int accountNo, string userId, string payload, string sender)
        {
            this.ActionId = actionId;
            this.UniqueToken = token;
            this.AccountNo = accountNo;
            this.UserID = userId;
            this.Payload = payload;
            this.Sender = sender;
        }
        public Actions(int actionId, string token, int accountNo, string userId, string payload, string sender, DateTime createdAt)
            : this(actionId, token, accountNo, userId, payload, sender)
        {

            this.CreatedAt = createdAt;
        }
    }
}
