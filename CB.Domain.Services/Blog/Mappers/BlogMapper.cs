using System.Collections.Generic;
using System.Linq;
using CB.Domain.Models;
using CB.CMS.Models.Blog;
using CB.CMS.SquidexClient;

namespace CB.Services.Blog.Mappers
{
    public class BlogMapper : IBlogMapper
    {
        private ISquidexConfiguration _config;

        public BlogMapper(ISquidexConfiguration config)
        {
            _config = config;
        }

        public List<BlogCategory> MapToBlogCategories(List<BlogCategoryEntity> model)
        {
            var result = new List<BlogCategory>();
            foreach (var category in model)
            {
                result.Add(MapToBlogCategory(category));
            }
            return result;
        }

        public BlogCategory MapToBlogCategory(BlogCategoryEntity model)
        {
            var result = new BlogCategory()
            {
                ID = model.Id,
                Name = model.Data.Name,
                Icon = $"{_config.ServiceURL}/api/assets/{model.Data.Icons.FirstOrDefault()}"
            };
            return result;
        }

        public BlogPost MapToBlogPost(BlogPostEntity model, BlogCategoryEntity category, List<BlogPostTagEntity> tags)
        {
            var result = new BlogPost()
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

        public BlogPostTag MapToBlogPostTag(BlogPostTagEntity model)
        {
            var result = new BlogPostTag()
            {
                ID = model.Id,
                Name = model.Data.Name,
                Description = model.Data.Description
            };
            return result;
        }

        public List<BlogPostTag> MapToBlogPostTags(List<BlogPostTagEntity> model)
        {
            var result = new List<BlogPostTag>();
            foreach (var tag in model)
            {
                result.Add(MapToBlogPostTag(tag));
            }
            return result;
        }
    }
}
