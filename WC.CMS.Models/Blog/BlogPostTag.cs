using Newtonsoft.Json;
using WC.CMS.SquidexClient;

namespace WC.CMS.Models.Blog
{
    public class BlogPostTag
    {
        [JsonConverter(typeof(InvariantConverter))]
        public string Name { get; set; }

        [JsonConverter(typeof(InvariantConverter))]
        public string Description { get; set; }
    }
}
