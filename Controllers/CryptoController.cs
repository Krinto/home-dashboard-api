using home_dashboard_api.Services;
using home_dashboard_api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public CryptoController(ILogger<CryptoController> logger, ICoinmarketcapService coinmarketcap)
        {
            this.logger = logger;
            coinmarketcapService = coinmarketcap;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Currency>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("getQuotes")]
        public async Task<ActionResult<IEnumerable<Currency>>> GetQuotes([FromQuery] GetQuotesQuery quotesQuery)
        {
            return Ok(await coinmarketcapService.GetLatestQuotes(quotesQuery.Slugs, quotesQuery.Currency));
        }
    }

    public class GetQuotesQuery
    {
        [FromQuery]
        [DefaultValue("bitcoin,ethereum")]
        [Description("Comma seperated currency slugs with no spaces")]
        public string Slugs { get; set; }
        [FromQuery]
        [DefaultValue("NZD")]
        [Description("Only provide one currency ticker free tier only allows single conversion")]
        public string Currency { get; set; }
    }
}
