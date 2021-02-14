using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace home_dashboard_api.ViewModels
{
    public class LandfillDay
    {
        public DateTime Date { get; set; }
        [EnumDataType(typeof(BinType))]
        [JsonConverter(typeof(StringEnumConverter))]
        public BinType BinType { get; set; }
    }

    public enum BinType
    {
        RedBin,
        YellowBin
    }
}
