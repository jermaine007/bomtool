using Newtonsoft.Json;
using Noone.UI.Models;

namespace BomTool.NetCore.Models
{

    public class CreditInfo : ModelBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
