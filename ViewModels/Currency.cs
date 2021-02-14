using System;
using System.Collections.Generic;

namespace home_dashboard_api.ViewModels
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public int Rank { get; set; }
        public double Price { get; set; }
        public double Volume24h { get; set; }
        public double PercentChange1h { get; set; }
        public double PercentChange24h { get; set; }
        public double PercentChange7d { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public double MarketCap { get; set; }
        public string PriceCurrency { get; set; }
    }
}
