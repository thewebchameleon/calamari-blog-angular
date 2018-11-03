using Newtonsoft.Json;
using Squidex.ClientLibrary;

namespace CB.Angular.Infrastructure.Repositories.SquidexRepo.Models
{
    public class BlogPostTagData
    {
        [JsonConverter(typeof(InvariantConverter))]
        public string Name { get; set; }

        [JsonConverter(typeof(InvariantConverter))]
        public string Description { get; set; }
    }
}
