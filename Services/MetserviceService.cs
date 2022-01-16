using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace home_dashboard_api.Services
{
    public class MetserviceService
    {
        private readonly HttpClient client;
        private readonly IAppSettings settings;

        public MetserviceService(IHttpClientFactory clientFactory, IAppSettings settings)
        {
            client = clientFactory.CreateClient("metservice");
            this.settings = settings;
        }
    }
}
