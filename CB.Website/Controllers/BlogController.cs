using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using CB.Domain.Models;
using CB.Services.Blog;

namespace WebChameleon_Blog.Controllers
{
    [Route("api/[controller]")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        [Route("get-blog-posts")]
        public async Task<List<BlogPost>> GetBlogPosts()
        {
            var result = await _blogService.GetBlogPosts();
            return result;
        }

        [HttpGet]
        [Route("get-blog-posts-by-category-id")]
        public async Task<List<BlogPost>> GetBlogPostsByCategoryID(string id)
        {
            var result = await _blogService.GetBlogPostsByCategoryID(id);
            return result;
        }

        [HttpGet]
        [Route("get-blog-categories")]
        public async Task<List<BlogCategory>> GetBlogCategories()
        {
            var result = await _blogService.GetBlogCategories();
            return result;
        }

        [HttpGet]
        [Route("get-blog-post")]
        public async Task<BlogPost> GetBlogPost(string id)
        {
            var result = await _blogService.GetBlogPost(id);
            return result;
        }
    }
}
