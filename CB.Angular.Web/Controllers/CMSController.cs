using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using CB.Angular.CMS.Contracts;
using CB.Angular.Infrastructure.Cache;
using CB.Angular.Infrastructure.Configuration;
using CB.Angular.Infrastructure.Extensions;
using CB.Angular.Interface.CMS;

namespace CalamariBlog.Server.Controllers
{
    [Route("api/[controller]")]
    public class CMSController : Controller
    {
        private readonly ICMSService _service;
        //private readonly ILoggerService _logger;
        private readonly ICacheProvider _cacheProvider;
        private readonly SquidexConfig _settings;

        public CMSController(ICMSService service, ICacheProvider cacheProvider, IOptions<SquidexConfig> settings)
        {
            _service = service;
            //_logger = loggerService;
            _cacheProvider = cacheProvider;
            _settings = settings.Value;
        }

        [HttpPost]
        [Route("flush-cache")]
        public async Task<IActionResult> FlushCache()
        {
            Request.Headers.TryGetValue("X-Signature", out StringValues signature);

            using (var reader = new StreamReader(Request.Body))
            {
                var requestBody = await reader.ReadToEndAsync();
                var generatedSignature = $"{requestBody}{_settings.WebHookSecret}".Sha256Base64();

                if (generatedSignature == signature)
                {
                    try
                    {
                        dynamic jObject = JObject.Parse(requestBody);
                        var payload = jObject.payload;

                        string schemaName = ((string)payload.schemaId).Split(',')[1];
                        string contentID = payload.contentId;

                        //clear from cache
                        _cacheProvider.Clear(schemaName);
                        _cacheProvider.Clear(contentID);

                        return Ok();
                    }
                    catch (Exception ex)
                    {
                        //_logger.LogError(ex, "An error ocurred trying to flush cached items");
                    }
                }
            }
            //_logger.LogWarning("Could not authenticate request");
            return Unauthorized();
        }

        [HttpGet]
        [Route("get-profile")]
        public async Task<Profile> GetProfile()
        {
            //log started
            var result = await _service.GetProfile();
            //log completed
            return result;
        }

        [HttpGet]
        [Route("get-global-config")]
        public async Task<GlobalConfig> GetGlobalConfig()
        {
            //log started
            var result = await _service.GetGlobalConfig();
            //log completed
            return result;
        }

        [HttpGet]
        [Route("get-blog-posts")]
        public async Task<List<BlogPost>> GetBlogPosts()
        {
            var result = await _service.GetBlogPosts();
            return result;
        }

        [HttpGet]
        [Route("get-blog-posts-by-category-id")]
        public async Task<List<BlogPost>> GetBlogPostsByCategoryID(string id)
        {
            var result = await _service.GetBlogPostsByCategoryID(id);
            return result;
        }

        [HttpGet]
        [Route("get-blog-categories")]
        public async Task<List<BlogCategory>> GetBlogCategories()
        {
            var result = await _service.GetBlogCategories();
            return result;
        }

        [HttpGet]
        [Route("get-blog-post")]
        public async Task<BlogPost> GetBlogPost(string id)
        {
            var result = await _service.GetBlogPost(id);
            return result;
        }
    }
}
