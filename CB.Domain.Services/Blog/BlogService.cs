using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CB.Domain.Models;
using CB.Infrastructure.Repositories;
using CB.Services.Blog.Mappers;

namespace CB.Services.Blog
{
    /// <summary>
    /// this class is responsible for fetching data from the repository and returning a domain model
    /// </summary>
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IBlogMapper _blogMapper;

        public BlogService(IBlogRepository blogRepository, IBlogMapper mapper)
        {
            _blogRepository = blogRepository;
            _blogMapper = mapper;
        }

        #region Public Methods

        public async Task<List<BlogCategory>> GetBlogCategories()
        {
            var categories = await _blogRepository.GetBlogCategories();
            return _blogMapper.MapToBlogCategories(categories);
        }

        public async Task<BlogCategory> GetBlogCategory(string id)
        {
            var category = await _blogRepository.GetBlogCategory(id);
            return _blogMapper.MapToBlogCategory(category);
        }

        public async Task<BlogPost> GetBlogPost(string id)
        {
            var post = await _blogRepository.GetBlogPost(id);

            var categories = await _blogRepository.GetBlogCategories();
            var category = categories.Where(c => post.Data.Categories.Contains(c.Id)).FirstOrDefault();

            var tags = await _blogRepository.GetBlogPostTags();
            tags = tags.Where(t => post.Data.Tags.Contains(t.Id)).ToList();

            return _blogMapper.MapToBlogPost(post, category, tags);
        }

        public async Task<List<BlogPost>> GetBlogPosts()
        {
            var posts = await _blogRepository.GetBlogPosts();

            var result = new List<BlogPost>();
            foreach (var post in posts)
            {
                result.Add(await GetBlogPost(post.Id));
            }
            return result;
        }

        public async Task<List<BlogPost>> GetBlogPostsByCategoryID(string id)
        {
            var posts = await _blogRepository.GetBlogPosts();

            posts = posts.Where(p => p.Data.Categories.Contains(id)).ToList();

            var result = new List<BlogPost>();
            foreach (var post in posts)
            {
                result.Add(await GetBlogPost(post.Id));
            }
            return result;
        }

        public async Task<List<BlogPostTag>> GetBlogPostTags()
        {
            var tags = await _blogRepository.GetBlogPostTags();

            var result = new List<BlogPostTag>();
            result.AddRange(await GetBlogPostTags(tags.Select(t => t.Id).ToList()));
            return result;
        }

        public async Task<List<BlogPostTag>> GetBlogPostTags(List<string> tagIDs)
        {
            var result = new List<BlogPostTag>();
            foreach (var tagID in tagIDs)
            {
                var tag = await _blogRepository.GetBlogPostTag(tagID);
                result.Add(_blogMapper.MapToBlogPostTag(tag));
            }
            return result;
        }

        #endregion
    }
}
