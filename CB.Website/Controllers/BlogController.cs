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
        [Route("GetBlogPosts")]
        public async Task<List<BlogPost>> GetBlogPosts()
        {
            //log started
            var result = await _blogService.GetBlogPosts();
            //log completed
            return result;
        }

        [HttpGet]
        [Route("GetBlogPostsByCategoryID")]
        public async Task<List<BlogPost>> GetBlogPostsByCategoryID(string id)
        {
            //log started
            var result = await _blogService.GetBlogPostsByCategoryID(id);
            //log completed
            return result;
        }

        [HttpGet]
        [Route("GetBlogCategories")]
        public async Task<List<BlogCategory>> GetBlogCategories()
        {
            //log started
            var result = await _blogService.GetBlogCategories();
            //log completed
            return result;
        }

        [HttpGet]
        [Route("GetBlogPost")]
        public async Task<BlogPost> GetBlogPost(string id)
        {
            //log started
            var result = await _blogService.GetBlogPost(id);
            //log completed
            return result;
        }
    }
}
