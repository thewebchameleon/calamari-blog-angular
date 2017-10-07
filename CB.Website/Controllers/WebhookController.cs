using CB.CMS.SquidexClient;
using CB.Common.Extensions;
using CB.Infrastructure.Cache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CB.Website.Controllers
{
    [Route("api/[controller]")]
    public class WebhookController : Controller
    {
        private readonly string _secret;
        private readonly ICacheProvider _cacheProvider;

        public WebhookController(ISquidexConfiguration config, ICacheProvider cacheProvider)
        {
            _secret = config.WebhookSecret;
            _cacheProvider = cacheProvider;
        }

        [HttpPost]
        [Route("FlushCache")]
        public async Task<IActionResult> FlushCache()
        {
            Request.Headers.TryGetValue("X-Signature", out StringValues signature);

            using (var reader = new StreamReader(Request.Body))
            {
                var txt = await reader.ReadToEndAsync();
                var generatedSignature = $"{txt}{_secret}".Sha256Base64();

                if (generatedSignature == signature)
                {
                    try
                    {
                        dynamic jObject = JObject.Parse(txt);
                        var schemaName = (jObject.schemaId as string).Split(',')[1];
                        var contentID = jObject.contentId;

                        //clear from cache
                        _cacheProvider.Clear(schemaName);
                        _cacheProvider.Clear(contentID);

                        return Ok();
                    }
                    catch (Exception ex)
                    {
                        //todo: log
                        throw new System.Exception("An error occurred");
                    }
                }
            }
            return Unauthorized();
        }
    }
}
