using System.Collections.Generic;
using System.Linq;
using WC.CMS.Models.Blog;
using WC.CMS.SquidexClient;

namespace WC.Blog.Services.Blog.Mappers
{
    public class BlogMapper : IBlogMapper
    {
        private ISquidexConfiguration _config;

        public BlogMapper(ISquidexConfiguration config)
        {
            _config = config;
        }

        public List<Domain.Models.BlogCategory> MapToBlogCategories(List<BlogCategoryEntity> model)
        {
            var result = new List<Domain.Models.BlogCategory>();
            foreach (var category in model)
            {
                result.Add(MapToBlogCategory(category));
            }
            return result;
        }

        public Domain.Models.BlogCategory MapToBlogCategory(BlogCategoryEntity model)
        {
            var result = new Domain.Models.BlogCategory()
            {
                ID = model.Id,
                Name = model.Data.Name,
                Icon = $"{_config.ServiceURL}/api/assets/{model.Data.Icons.FirstOrDefault()}"
            };
            return result;
        }

        public Domain.Models.BlogPost MapToBlogPost(BlogPostEntity model, BlogCategoryEntity category, List<BlogPostTagEntity> tags)
        {
            var result = new Domain.Models.BlogPost()
            {
                ID = model.Id,
                PublishedDate = model.Created.Date,
                Title = model.Data.Title,
                Body = model.Data.Body,
                Category = MapToBlogCategory(category),
                Tags = MapToBlogPostTags(tags)
            };
            return result;
        }

        public Domain.Models.BlogPostTag MapToBlogPostTag(BlogPostTagEntity model)
        {
            var result = new Domain.Models.BlogPostTag()
            {
                ID = model.Id,
                Name = model.Data.Name,
                Description = model.Data.Description
            };
            return result;
        }

        public List<Domain.Models.BlogPostTag> MapToBlogPostTags(List<BlogPostTagEntity> model)
        {
            var result = new List<Domain.Models.BlogPostTag>();
            foreach (var tag in model)
            {
                result.Add(MapToBlogPostTag(tag));
            }
            return result;
        }
    }
}
