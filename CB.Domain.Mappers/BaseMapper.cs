using CB.CMS.SquidexClient;
using Microsoft.Extensions.Options;

namespace CB.Domain.Mappers
{
    public abstract class BaseMapper
    {
        protected readonly SquidexSettings _config;

        public BaseMapper(IOptions<SquidexSettings> settings)
        {
            _config = settings.Value;
        }

        public string ResolveAssetURL(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return string.Empty;
            }
            return $"{_config.ServiceURL}/api/assets/{id}";
        }
    }
}
