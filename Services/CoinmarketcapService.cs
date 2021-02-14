using home_dashboard_api.ViewModels;
using home_dashboard_api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.AspNetCore.WebUtilities;
using System.Linq;

namespace home_dashboard_api.Services
{
    public interface ICoinmarketcapService
    {
        Task<IList<Currency>> GetLatestQuotes(string? slugs, string? convert);
    }

    public class CoinmarketcapService: ICoinmarketcapService
    {
        private const string DEFAULT_CURRENCY = "NZD";

        private readonly HttpClient client;

        public CoinmarketcapService(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("coinmarketcap");
        }

        public async Task<IList<Currency>> GetLatestQuotes(string? slugs, string? convert)
        {
            var conversionCurrency = convert ?? DEFAULT_CURRENCY;

            if(slugs == null)
            {
                throw new Exception("You must provide currency slugs");
            }

            var parameters = new Dictionary<string, string?>() { 
                { "convert", conversionCurrency },
                { "slug", slugs.ToLower()}
            };

            var url = new Uri(QueryHelpers.AddQueryString("quotes/latest", parameters), UriKind.Relative);
            var response = await client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<CoinmarketcapItemData>(content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to call coinmarketcap:{Environment.NewLine }{JsonConvert.SerializeObject(result.Status)}");
            }
                        
            return result.DataList.Select(currency => new Currency
            {
                Id = currency.Id,
                Name = currency.Name,
                Symbol = currency.Symbol,
                Rank = currency.CmcRank,
                Price = currency.Quote[conversionCurrency].Price ?? 0d,
                Volume24h = currency.Quote[conversionCurrency].Volume24h ?? 0,
                PercentChange1h = currency.Quote[conversionCurrency].PercentChange1h ?? 0,
                PercentChange24h = currency.Quote[conversionCurrency].PercentChange24h ?? 0,
                PercentChange7d = currency.Quote[conversionCurrency].PercentChange7d ?? 0,
                LastUpdated = currency.Quote[conversionCurrency].LastUpdated ?? DateTimeOffset.Now,
                MarketCap = currency.Quote[conversionCurrency].MarketCap ?? 0d,
                PriceCurrency = conversionCurrency
            }).ToList();
        }
    }
}
