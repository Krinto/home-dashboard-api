namespace home_dashboard_api
{
    public interface IAppSettings
    {
        CoinmarketcapSettings CoinmarketcapSettings { get; set; }
        LandfillSettings LandfillSettings { get; set; }
    }

    public class AppSettings: IAppSettings
    {
        public CoinmarketcapSettings CoinmarketcapSettings { get; set; }
        public LandfillSettings LandfillSettings { get; set; }
    }

    public class CoinmarketcapSettings
    {
        public string ApiKey { get; set; }
        public string BaseUrl { get; set; }
    }

    public class LandfillSettings
    {
        public string BaseUrl { get; set; }
        public string HomeAddress { get; set; }
    }
}
