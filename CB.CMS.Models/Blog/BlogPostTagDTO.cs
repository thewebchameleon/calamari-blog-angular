using Newtonsoft.Json;
using CB.CMS.SquidexClient;

namespace CB.CMS.Models.Blog
{
    public class BlogPostTagDTO
    {
        [JsonConverter(typeof(InvariantConverter))]
        public string Name { get; set; }

        [JsonConverter(typeof(InvariantConverter))]
        public string Description { get; set; }
    }
}
