using System.Collections.Generic;
using System.Threading.Tasks;
using CB.CMS.Models.Blog;

namespace CB.Infrastructure.Repositories
{
    public interface IBlogRepository
    {
        Task<List<BlogPostEntity>> GetBlogPosts();

        Task<List<BlogCategoryEntity>> GetBlogCategories();

        Task<BlogPostEntity> GetBlogPost(string id);

        Task<List<BlogPostTagEntity>> GetBlogPostTags();

        Task<BlogCategoryEntity> GetBlogCategory(string id);

        Task<BlogPostTagEntity> GetBlogPostTag(string id);
    }
}
