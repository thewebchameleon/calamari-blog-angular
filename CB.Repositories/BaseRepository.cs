using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CB.Infrastructure.Cache;
using CB.CMS.SquidexClient;

namespace CB.Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        private readonly ISquidexClientFactory _clientFactory;
        private readonly ICacheProvider _cache;

        public BaseRepository(ISquidexClientFactory clientFactory, ICacheProvider cache)
        {
            _clientFactory = clientFactory;
            _cache = cache;
        }

        /// <summary>
        /// this will attempt to fetch a content item from the cache otherwise it will fall back to the API
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <param name="id">the string GUID of the content item</param>
        /// <param name="schemaName">the schema that this content item belongs to</param>
        /// <returns></returns>
        protected async Task<TEntity> GetItem<TEntity, TData>(string id, string schemaName) where TData : class, new() where TEntity : SquidexEntityBase<TData>, new()
        {
            //try fetch from schema
            var collection = new List<TEntity>();
            if (_cache.TryGetItem(schemaName, out collection))
            {
                if (!collection.Any(c => c.Id == id))
                {
                    throw new Exception($"Item with ID {id} was not found in schema {schemaName}");
                }
                return collection.First(c => c.Id == id);
            }

            //else try fetch individually cached item
            var entity = new TEntity();
            if (_cache.TryGetItem(schemaName, out entity))
            {
                return entity;
            }

            //else make call to API
            var client = _clientFactory.GetClient<TEntity, TData>(schemaName);
            var response = await client.GetAsync(id);
            return _cache.SetItem(id, response);
        }

        /// <summary>
        /// this will attempt to fetch content items in the specified schema from the cache otherwise it will fall back to the API
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <param name="schemaName">the schema that this content item belongs to</param>
        /// <returns></returns>
        protected async Task<List<TEntity>> GetItems<TEntity, TData>(string schemaName) where TData : class, new() where TEntity : SquidexEntityBase<TData>, new()
        {
            //try fetch from cache
            var result = new List<TEntity>();
            if (_cache.TryGetItem(schemaName, out result))
            {
                return result;
            }

            //else make call to API
            var client = _clientFactory.GetClient<TEntity, TData>(schemaName);
            var response = await client.GetAsync();
            return _cache.SetItem(schemaName, response.Items);
        }
    }
}
