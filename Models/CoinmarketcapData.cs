using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace home_dashboard_api.Models
{
    public class CoinmarketcapItemData
    {
        public Status? Status { get; set; }

        public List<ItemData> DataList { get; set; } = new List<ItemData>();

        private Dictionary<string, ItemData> _dataItemList;
        [JsonProperty(PropertyName = "data")]
        public Dictionary<string, ItemData> DataDictionary
        {
            get { return _dataItemList; }
            set
            {
                _dataItemList = value;
                DataList = value.Values.ToList();
            }
        }
    }

    public class ItemData
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "slug")]
        public string Slug { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "num_market_pairs")]
        public int NumMarketPairs { get; set; }
        [JsonProperty(PropertyName = "date_added")]
        public DateTime? DateAdded { get; set; }
        [JsonProperty(PropertyName = "tags")]
        public List<object> Tags { get; set; } = new List<object>();
        [JsonProperty(PropertyName = "max_supply")]
        public double? MaxSupply { get; set; }
        [JsonProperty(PropertyName = "circulating_supply")]
        public double? CirculatingSupply { get; set; }
        [JsonProperty(PropertyName = "total_supply")]
        public double? TotalSupply { get; set; }
        [JsonProperty(PropertyName = "platform")]
        public CurrenyInfo? Platform { get; set; }
        [JsonProperty(PropertyName = "cmc_rank")]
        public int CmcRank { get; set; }
        [JsonProperty(PropertyName = "last_updated")]
        public DateTime? LastUpdated { get; set; }
        [JsonProperty(PropertyName = "quote")]
        public Dictionary<string, CurrenyPriceInfo> Quote { get; set; } = new Dictionary<string, CurrenyPriceInfo>();
    }

    public class Status
    {
        [JsonProperty(PropertyName = "timestamp")]
        public DateTime? Timestamp { get; set; }
        [JsonProperty(PropertyName = "error_code")]
        public int ErrorCode { get; set; }
        [JsonProperty(PropertyName = "error_message")]
        public string? ErrorMessage { get; set; }
        [JsonProperty(PropertyName = "elapsed")]
        public int Elapsed { get; set; }
        [JsonProperty(PropertyName = "credit_count")]
        public int CreditCount { get; set; }
    }

    public class CurrenyInfo
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "symbol")]
        public string Symbol { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "slug")]
        public string Slug { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "token_address")]
        public string TokenAddress { get; set; } = string.Empty;
    }

    public class CurrenyPriceInfo
    {
        [JsonProperty(PropertyName = "price")]
        public double? Price { get; set; }
        [JsonProperty(PropertyName = "volume_24h")]
        public double? Volume24h { get; set; }
        [JsonProperty(PropertyName = "percent_change_1h")]
        public double? PercentChange1h { get; set; }
        [JsonProperty(PropertyName = "percent_change_24h")]
        public double? PercentChange24h { get; set; }
        [JsonProperty(PropertyName = "percent_change_7d")]
        public double? PercentChange7d { get; set; }
        [JsonProperty(PropertyName = "market_cap")]
        public double? MarketCap { get; set; }
        [JsonProperty(PropertyName = "last_updated")]
        public DateTime? LastUpdated { get; set; }
    }
}
