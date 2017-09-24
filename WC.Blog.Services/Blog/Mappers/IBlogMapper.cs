using System.Collections.Generic;
using WC.CMS.Models.Blog;

namespace WC.Blog.Services.Blog.Mappers
{
    public interface IBlogMapper
    {
        List<Domain.Models.BlogCategory> MapToBlogCategories(List<BlogCategoryEntity> model);

        Domain.Models.BlogCategory MapToBlogCategory(BlogCategoryEntity model);

        Domain.Models.BlogPost MapToBlogPost(BlogPostEntity model, BlogCategoryEntity category, List<BlogPostTagEntity> tags);

        Domain.Models.BlogPostTag MapToBlogPostTag(BlogPostTagEntity model);

        List<Domain.Models.BlogPostTag> MapToBlogPostTags(List<BlogPostTagEntity> model);
    }
}
