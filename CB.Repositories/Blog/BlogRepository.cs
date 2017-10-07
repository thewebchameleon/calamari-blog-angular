using System.Collections.Generic;
using System.Threading.Tasks;
using CB.Infrastructure.Cache;
using CB.CMS.Models.Blog;
using CB.CMS.Models.Constants;
using CB.CMS.SquidexClient;

namespace CB.Infrastructure.Repositories
{
    public class BlogRepository : BaseRepository, IBlogRepository
    {
        public BlogRepository(ISquidexClientFactory clientFactory, ICacheProvider cache) : base(clientFactory, cache) { }

        #region Public Methods

        public async Task<List<BlogCategoryEntity>> GetBlogCategories()
        {
            return await GetItems<BlogCategoryEntity, BlogCategoryDTO>(SchemaNameConstants.Blog_Categories);
        }

        public async Task<BlogCategoryEntity> GetBlogCategory(string id)
        {
            return await GetItem<BlogCategoryEntity, BlogCategoryDTO>(id, SchemaNameConstants.Blog_Categories);
        }

        public async Task<BlogPostEntity> GetBlogPost(string id)
        {
            return await GetItem<BlogPostEntity, BlogPostDTO>(id, SchemaNameConstants.Blog_Posts);
        }

        public async Task<List<BlogPostEntity>> GetBlogPosts()
        {
            return await GetItems<BlogPostEntity, BlogPostDTO>(SchemaNameConstants.Blog_Posts);
        }

        public async Task<BlogPostTagEntity> GetBlogPostTag(string id)
        {
            return await GetItem<BlogPostTagEntity, BlogPostTagDTO>(id, SchemaNameConstants.Blog_Post_Tags);
        }

        public async Task<List<BlogPostTagEntity>> GetBlogPostTags()
        {
            return await GetItems<BlogPostTagEntity, BlogPostTagDTO>(SchemaNameConstants.Blog_Post_Tags);
        }

        #endregion
    }
}
