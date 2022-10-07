using BMSDK;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System.Linq;
using System.Threading.Tasks;

namespace Lif_x_BMS
{
    internal class BMSConnector
    {
        public static async void Test()
        {
            var request = new ServiceDesk.MyTickets.Search()
            {
                query = new ServiceDesk.MyTickets.Search.Query()
                {
                    filter = new ServiceDesk.MyTickets.Search.Filter()
                    {
                        queueNames = "Help Desk",
                        statusNames = "New"
                    }
                }
            };
            var ticketResults = await request.PostAsync();
        }
        public static async Task<int> InitializeBMSClient()
        {
            // Get Authkey for BMS
            var authRequest = new RestRequest("/v2/security/authenticate");
            authRequest.AddHeader("content-type", "application/x-www-form-urlencoded");
            authRequest.AddHeader("accept", "application/json");
            var parameters = new
            {
                grantType = "",
                userName = "",
                password = "",
                tenant = ""
            };
            authRequest.AddObject(parameters);

            var authResponse = await BMS.Client.ExecutePostAsync(authRequest);
            var BMSResults = JsonConvert.DeserializeObject<BMSAuthResults>(authResponse.Content);


            // !TODO: What should program do if authentication fails? 
            if (!BMSResults.success)
            {
                Program.Log("Failed to authenticate to BMS.");
                return 1;
            }
            Program.Log("Successfully authenticated to BMS.");
            string accessToken = BMSResults.result.accessToken;
            var authenticator = new JwtAuthenticator(accessToken);
            BMS.Client.Authenticator = authenticator;
            return 0;
        }

        public static async Task<string> GetBMSToken()
        {
            // Get Authkey for BMS
            var authRequest = new RestRequest("/v2/security/authenticate");
            authRequest.AddHeader("content-type", "application/x-www-form-urlencoded");
            authRequest.AddHeader("accept", "application/json");
            var parameters = new
            {
                grantType = "",
                userName = "",
                password = "",
                tenant = ""
            };
            authRequest.AddObject(parameters);

            var authResponse = await BMS.Client.ExecutePostAsync(authRequest);
            var BMSResults = JsonConvert.DeserializeObject<BMSAuthResults>(authResponse.Content);

            // !TODO: What should program do if authentication fails? 
            if (!BMSResults.success)
            {
                Program.Err("Failed to authenticate to BMS.");
                return null;
            }
            Program.Log("Successfully authenticated to BMS.");
            string accessToken = BMSResults.result.accessToken.ToString();
            BMS.Client.Dispose();
            return accessToken;
        }

        public static async Task<int> TicketAlert(string key)
        {
            // Build Ticket Count Request
            var ticketRequest = new RestRequest("/v2/servicedesk/dashboard/ticketscount/bystatus");
            ticketRequest.AddHeader("accept", "application/json");

            // Response 
            var response = await BMS.Client.ExecuteGetAsync(ticketRequest);
            var results = JsonConvert.DeserializeObject<TicketResults>(response.Content);

            // !TODO: What should program do if getting tickets fails? 
            if (!results.success)
            {
                Program.Err("Failed to authenticate to BMS. Token may have expired.");
                return 1;
            }
            Program.Log("Successfully authenticated to BMS.");

            int newTicketCount = results.result.Where(x => x.name == "New").First().ticketsCount;
            int clientRespondedCount = results.result.Where(x => x.name == "Client Responded").First().ticketsCount;
            //int ticketCount = newTicketCount + clientRespondedCount;

            Program.Log($"New tickets open: {newTicketCount}");
            Program.Log($"Client Responded tickets open: {clientRespondedCount}");

            if (newTicketCount > 0)
            {
                LifxConnector.lifxConnector(key);
            }
            else if (clientRespondedCount > 0)
            {
                LifxConnector.lifxConnectorCS(key);
            }
            return 0;
        }
    }
}
