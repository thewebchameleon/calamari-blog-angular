using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WC.Blog.Infrastructure.Cache;
using WC.CMS.Models.Blog;
using WC.CMS.Models.Constants;
using WC.CMS.SquidexClient;

namespace WC.Blog.Infrastructure.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ISquidexClientFactory _clientFactory;
        private readonly ICacheProvider _cache;

        public BlogRepository(ISquidexClientFactory clientFactory, ICacheProvider cache)
        {
            _clientFactory = clientFactory;
            _cache = cache;
        }

        #region Public Methods

        public async Task<List<BlogCategoryEntity>> GetBlogCategories()
        {
            return await GetOrSetItems<BlogCategoryEntity, BlogCategory>(SchemaNameConstants.Blog_Categories);
        }

        public async Task<BlogCategoryEntity> GetBlogCategory(string id)
        {
            return await GetOrSetItem<BlogCategoryEntity, BlogCategory>(id, SchemaNameConstants.Blog_Categories);
        }

        public async Task<BlogPostEntity> GetBlogPost(string id)
        {
            return await GetOrSetItem<BlogPostEntity, BlogPost>(id, SchemaNameConstants.Blog_Posts);
        }

        public async Task<List<BlogPostEntity>> GetBlogPosts()
        {
            return await GetOrSetItems<BlogPostEntity, BlogPost>(SchemaNameConstants.Blog_Posts);
        }

        public async Task<BlogPostTagEntity> GetBlogPostTag(string id)
        {
            return await GetOrSetItem<BlogPostTagEntity, BlogPostTag>(id, SchemaNameConstants.Blog_Post_Tags);
        }

        public async Task<List<BlogPostTagEntity>> GetBlogPostTags()
        {
            return await GetOrSetItems<BlogPostTagEntity, BlogPostTag>(SchemaNameConstants.Blog_Post_Tags);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// this will attempt to fetch a content item from the cache otherwise it will fall back to the API
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TData"></typeparam>
        /// <param name="id">the string GUID of the content item</param>
        /// <param name="schemaName">the schema that this content item belongs to</param>
        /// <returns></returns>
        private async Task<TEntity> GetOrSetItem<TEntity, TData>(string id, string schemaName) where TData : class, new() where TEntity : SquidexEntityBase<TData>, new()
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
        private async Task<List<TEntity>> GetOrSetItems<TEntity, TData>(string schemaName) where TData : class, new() where TEntity : SquidexEntityBase<TData>, new()
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

        #endregion
    }
}
