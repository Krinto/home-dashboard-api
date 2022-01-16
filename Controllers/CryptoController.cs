using home_dashboard_api.Services;
using home_dashboard_api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace home_dashboard_api.Controllers
{
    [Route("api/v1/crypto")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private readonly ILogger<CryptoController> logger;
        private readonly ICoinmarketcapService coinmarketcapService;
        private readonly IMemoryCache memoryCache;

        private readonly MemoryCacheEntryOptions cacheExpiryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.Now.AddMinutes(15),
            Priority = CacheItemPriority.High,
            SlidingExpiration = TimeSpan.FromMinutes(7.5),
            Size = 1024,
        };

        public CryptoController(ILogger<CryptoController> logger, ICoinmarketcapService coinmarketcap, IMemoryCache memoryCache)
        {
            this.logger = logger;
            coinmarketcapService = coinmarketcap;
            this.memoryCache = memoryCache;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Currency>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("getQuotes")]
        public async Task<ActionResult<IEnumerable<Currency>>> GetQuotes([FromQuery] GetQuotesQuery quotesQuery)
        {
            var cacheKey = $"{quotesQuery.Ids}${quotesQuery.Currency}";
            var currencyList = await memoryCache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15);
                entry.SlidingExpiration = TimeSpan.FromMinutes(7.5);
                entry.Priority = CacheItemPriority.High;
                entry.Size = 1024;
                return await coinmarketcapService.GetLatestQuotes(quotesQuery.Ids, quotesQuery.Currency);
            });
            return Ok(currencyList);
        }
    }

    public class GetQuotesQuery
    {
        [FromQuery]
        [DefaultValue("bitcoin,ethereum")]
        [Description("Comma seperated currency slugs with no spaces")]
        public string Ids { get; set; } = string.Empty;
        [FromQuery]
        [DefaultValue("NZD")]
        [Description("Only provide one currency ticker free tier only allows single conversion")]
        public string Currency { get; set; } = string.Empty;
    }
}
