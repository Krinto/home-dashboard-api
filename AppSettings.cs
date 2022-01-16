namespace home_dashboard_api
{
    public interface IAppSettings
    {
        Coinmarketcap Coinmarketcap { get; set; }
        Landfill Landfill { get; set; }
        Metservice Metservice { get; set; }
    }

    public class AppSettings: IAppSettings
    {
        public Coinmarketcap Coinmarketcap { get; set; } = new Coinmarketcap();
        public Landfill Landfill { get; set; } = new Landfill();
        public Metservice Metservice { get; set; } = new Metservice();
    }

    public class Coinmarketcap: ApiBase
    {
        public string ApiKey { get; set; } = string.Empty;
    }

    public class Landfill: ApiBase
    {
        public string HomeAddress { get; set; } = string.Empty;
    }

    public class Metservice: ApiBase
    {
    }

    public abstract class ApiBase
    {
        public string BaseUrl { get; set; } = string.Empty;
    }
}
