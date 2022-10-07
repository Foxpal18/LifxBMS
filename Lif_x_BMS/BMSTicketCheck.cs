using System;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Lif_x_BMS
{
    public class BMSTicketCheck
    {
        public static async void check()
        {
            var client = new RestClient("https://api.bms.kaseya.com/v2/servicedesk/dashboard/ticketsdue");
            var request = new RestRequest();

            request.AddHeader("accept", "application/json");
            request.AddParameter("pageNumber", "1");

            var response = await client.ExecuteGetAsync(request);

            string json = JsonConvert.SerializeObject(response.Content, Formatting.Indented);
            Program.Log(json);
        }
    }
    public class TicketInfo
    {
        public string name { get; set; }
        public string color { get; set; }
        public string fontColor { get; set; }
        public int typeId { get; set; }
        public int order { get; set; }
        public int ticketsCount { get; set; }
    }
    public class TicketResults
    {
        public bool success { get; set; }
        public List<TicketInfo> result { get; set; }
    }

    public class BMSAuthInfo
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public DateTime accessTokenExpireOn { get; set; }
        public DateTime refreshTokenExpireOn { get; set; }
    }

    public class BMSAuthResults
    {
        public bool success { get; set; }
        public BMSAuthInfo result { get; set; }
    }
}
