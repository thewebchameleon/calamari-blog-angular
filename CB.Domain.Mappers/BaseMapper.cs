using CB.CMS.SquidexClient;

namespace CB.Domain.Mappers
{
    public abstract class BaseMapper
    {
        protected readonly ISquidexConfiguration _config;

        public BaseMapper(ISquidexConfiguration config)
        {
            _config = config;
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
