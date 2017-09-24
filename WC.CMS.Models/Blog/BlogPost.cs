using Newtonsoft.Json;
using System.Collections.Generic;
using WC.CMS.SquidexClient;

namespace WC.CMS.Models.Blog
{
    public class BlogPost
    {
        [JsonConverter(typeof(InvariantConverter))]
        public string Title { get; set; }

        [JsonConverter(typeof(InvariantConverter))]
        public string Body { get; set; }

        [JsonConverter(typeof(InvariantConverter))]
        public List<string> Categories { get; set; }

        [JsonConverter(typeof(InvariantConverter))]
        public List<string> Tags { get; set; }

        public BlogPost()
        {
            Tags = new List<string>();
            Categories = new List<string>();
        }
    }
}
