using CB.CMS.SquidexClient;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CB.CMS.Models.Profile
{
    public class ProfileDTO
    {
        [JsonConverter(typeof(InvariantConverter))]
        public List<string> Portrait { get; set; }

        [JsonConverter(typeof(InvariantConverter))]
        public string Name { get; set; }

        [JsonConverter(typeof(InvariantConverter))]
        public string Title { get; set; }

        [JsonConverter(typeof(InvariantConverter))]
        public string Body { get; set; }
    }
}
