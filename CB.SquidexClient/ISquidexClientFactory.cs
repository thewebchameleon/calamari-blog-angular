namespace CB.CMS.SquidexClient
{
    public interface ISquidexClientFactory 
    {
        SquidexClient<TEntity, TData> GetClient<TEntity, TData>(string schemaName) where TData : class, new() where TEntity : SquidexEntityBase<TData>;
    }
}
