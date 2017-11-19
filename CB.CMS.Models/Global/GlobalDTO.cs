using CB.CMS.SquidexClient;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CB.CMS.Models.Global
{
    public class GlobalDTO
    {
        [JsonConverter(typeof(InvariantConverter))]
        public List<string> HeartIcon { get; set; }

        [JsonConverter(typeof(InvariantConverter))]
        public string Name { get; set; }

        public GlobalDTO()
        {
            HeartIcon = new List<string>();
        }
    }
}
