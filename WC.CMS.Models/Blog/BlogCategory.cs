using Newtonsoft.Json;
using System.Collections.Generic;
using WC.CMS.SquidexClient;

namespace WC.CMS.Models.Blog
{
    public class BlogCategory
    {
        [JsonConverter(typeof(InvariantConverter))]
        public string Name { get; set; }

        [JsonConverter(typeof(InvariantConverter))]
        public List<string> Icons { get; set; }

        public BlogCategory()
        {
            Icons = new List<string>();
        }
    }
}
