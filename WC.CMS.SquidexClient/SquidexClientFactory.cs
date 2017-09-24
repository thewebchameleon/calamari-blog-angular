namespace WC.CMS.SquidexClient
{
    public class SquidexClientFactory : ISquidexClientFactory
    {
        readonly ISquidexConfiguration _config;

        public SquidexClientFactory(ISquidexConfiguration config)
        {
            _config = config;
        }

        public SquidexClient<TEntity, TData> GetClient<TEntity, TData>(string schemaName)
            where TEntity : SquidexEntityBase<TData>
            where TData : class, new()
        {
            return new SquidexClient<TEntity, TData>(_config, schemaName);
        }
    }
}
