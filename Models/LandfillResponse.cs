using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace home_dashboard_api.Models
{
    public class LandfillResponse
    {
        public string Address { get; set; } = string.Empty;
        public DateTime RedBin { get; set; }
        public DateTime YellowBin { get; set; }
        public int CollectionWeek { get; set; }
        public int CollectionDay { get; set; }
    }
}
