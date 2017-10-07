using System.Collections.Generic;
using System.Threading.Tasks;
using CB.Domain.Models;

namespace CB.Services.Blog
{
    public interface IBlogService
    {
        Task<List<BlogPost>> GetBlogPosts();

        Task<BlogPost> GetBlogPost(string id);

        Task<List<BlogCategory>> GetBlogCategories();

        Task<List<BlogPostTag>> GetBlogPostTags();

        Task<List<BlogPostTag>> GetBlogPostTags(List<string> tagIDs);

        Task<BlogCategory> GetBlogCategory(string id);

        Task<List<BlogPost>> GetBlogPostsByCategoryID(string id);
    }
}
