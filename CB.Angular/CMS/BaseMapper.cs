using CB.Angular.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace CB.Angular.CMS
{
    public abstract class BaseMapper
    {
        protected readonly SquidexConfig _config;

        public BaseMapper(IOptions<SquidexConfig> config)
        {
            _config = config.Value;
        }

        public string ResolveAssetURL(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return string.Empty;
            }
            return $"{_config.Url}/api/assets/{id}";
        }
    }
}
