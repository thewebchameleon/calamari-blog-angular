using Newtonsoft.Json;
using System.Collections.Generic;
using CB.CMS.SquidexClient;

namespace CB.CMS.Models.Blog
{
    public class BlogPostDTO
    {
        [JsonConverter(typeof(InvariantConverter))]
        public string Title { get; set; }

        [JsonConverter(typeof(InvariantConverter))]
        public string Body { get; set; }

        [JsonConverter(typeof(InvariantConverter))]
        public List<string> Categories { get; set; }

        [JsonConverter(typeof(InvariantConverter))]
        public List<string> Tags { get; set; }

        public BlogPostDTO()
        {
            Tags = new List<string>();
            Categories = new List<string>();
        }
    }
}
