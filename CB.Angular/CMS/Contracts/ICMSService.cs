using CB.Angular.Interface.CMS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CB.Angular.CMS.Contracts
{
    public interface ICMSService
    {
        Task<List<BlogPost>> GetBlogPosts();

        Task<BlogPost> GetBlogPost(string id);

        Task<List<BlogCategory>> GetBlogCategories();

        Task<List<BlogPostTag>> GetBlogPostTags();

        Task<List<BlogPostTag>> GetBlogPostTags(List<string> tagIds);

        Task<BlogCategory> GetBlogCategory(string id);

        Task<List<BlogPost>> GetBlogPostsByCategoryID(string id);

        Task<Profile> GetProfile();

        Task<GlobalConfig> GetGlobalConfig();
    }
}
