using Newtonsoft.Json;
using Noone.UI.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace BomTool.NetCore.Models
{
    public class AboutInfo : ModelBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("version")]
        public string Version { get; set; }
        [JsonProperty("author")]
        public string Author { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("credits")]
        public List<CreditInfo> Credits { get; set; }

        public static AboutInfo Load()
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "about_info.json");
            return JsonConvert.DeserializeObject<AboutInfo>(File.ReadAllText(file));
        }
    }
}
