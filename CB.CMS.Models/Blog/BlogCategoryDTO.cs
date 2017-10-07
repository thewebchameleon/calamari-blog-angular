using Newtonsoft.Json;
using System.Collections.Generic;
using CB.CMS.SquidexClient;

namespace CB.CMS.Models.Blog
{
    public class BlogCategoryDTO
    {
        [JsonConverter(typeof(InvariantConverter))]
        public string Name { get; set; }

        [JsonConverter(typeof(InvariantConverter))]
        public List<string> Icons { get; set; }

        public BlogCategoryDTO()
        {
            Icons = new List<string>();
        }
    }
}
