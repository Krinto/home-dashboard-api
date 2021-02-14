using home_dashboard_api.Models;
using home_dashboard_api.ViewModels;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace home_dashboard_api.Services
{
    public interface ILandfillService
    {
        Task<IList<LandfillDay>> GetLandfillDays(string? address);
    }

    public class LandfillService: ILandfillService
    {
        private readonly HttpClient client;
        private readonly IAppSettings settings;

        public LandfillService(IHttpClientFactory clientFactory, IAppSettings settings)
        {
            client = clientFactory.CreateClient("landfill");
            this.settings = settings;
        }

        public async Task<IList<LandfillDay>> GetLandfillDays(string? address)
        {
            var parameters = new Dictionary<string, string?>() {
                { "address_string", address ?? settings.LandfillSettings.HomeAddress }
            };

            var url = new Uri(QueryHelpers.AddQueryString("get_Collection_Dates", parameters), UriKind.Relative);

            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to call fight the landfill:{Environment.NewLine }{response.StatusCode} {response.ReasonPhrase}");
            }

            var result = JsonConvert.DeserializeObject<LandfillResponse[]>(content);

            return new List<LandfillDay>
            {
                new LandfillDay
                {
                    Date = result.First().RedBin,
                    BinType = BinType.RedBin
                },
                new LandfillDay
                {
                    Date = result.First().YellowBin,
                    BinType = BinType.YellowBin
                }
            }.OrderBy(x => x.Date).ToList();
        }
    }
}
