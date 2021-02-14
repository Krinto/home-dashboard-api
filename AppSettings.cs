namespace home_dashboard_api
{
    public interface IAppSettings
    {
        CoinmarketcapSettings CoinmarketcapSettings { get; set; }
    }

    public class AppSettings: IAppSettings
    {
        public CoinmarketcapSettings CoinmarketcapSettings { get; set; }
    }

    public class CoinmarketcapSettings
    {
        public string ApiKey { get; set; }
        public string BaseUrl { get; set; }
    }
}
