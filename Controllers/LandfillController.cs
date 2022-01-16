using home_dashboard_api.Services;
using home_dashboard_api.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace home_dashboard_api.Controllers
{
    [Route("api/v1/landfill")]
    [ApiController]
    public class LandfillController : ControllerBase
    {
        private readonly ILogger<LandfillController> logger;
        private readonly ILandfillService landfillService;

        public LandfillController(ILogger<LandfillController> logger, ILandfillService landfillService)
        {
            this.logger = logger;
            this.landfillService = landfillService;
        }

        [HttpGet]
        [Route("getDays")]
        [ProducesResponseType(typeof(IList<LandfillDay>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IList<LandfillDay>> GetLandfillDays()
        {
            return await landfillService.GetLandfillDays(null);
        }

        [HttpGet]
        [Route("getDaysByAddress/{address}")]
        [ProducesResponseType(typeof(IList<LandfillDay>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IList<LandfillDay>> GetLandfillDays([FromRoute] string address)
        {
            return await landfillService.GetLandfillDays(address);
        }
    }
}
