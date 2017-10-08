using System.Collections.Generic;
using CB.Domain.Models;
using CB.CMS.Models.Blog;

namespace CB.Services.Blog.Mappers
{
    public interface IBlogMapper
    {
        List<BlogCategory> MapToBlogCategories(List<BlogCategoryEntity> model);

        BlogCategory MapToBlogCategory(BlogCategoryEntity model);

        BlogPost MapToBlogPost(BlogPostEntity model, BlogCategoryEntity category, List<BlogPostTagEntity> tags);

        BlogPostTag MapToBlogPostTag(BlogPostTagEntity model);

        List<BlogPostTag> MapToBlogPostTags(List<BlogPostTagEntity> model);
    }
}
