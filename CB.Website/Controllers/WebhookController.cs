using CB.CMS.SquidexClient;
using CB.Common.Extensions;
using CB.Infrastructure.Cache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        private readonly ILogger<WebhookController> _logger;

        public WebhookController(IOptions<SquidexSettings> settings, ICacheProvider cacheProvider, ILoggerFactory logger)
        {
            _secret = settings.Value.WebhookSecret;
            _cacheProvider = cacheProvider;
            _logger = logger.CreateLogger<WebhookController>();
        }

        [HttpPost]
        [Route("FlushCache")]
        public async Task<IActionResult> FlushCache()
        {
            Request.Headers.TryGetValue("X-Signature", out StringValues signature);

            using (var reader = new StreamReader(Request.Body))
            {
                var requestBody = await reader.ReadToEndAsync();
                var generatedSignature = $"{requestBody}{_secret}".Sha256Base64();

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
                        _logger.LogError(ex, "An error ocurred trying to flush cached items");
                    }
                }
            }
            _logger.LogError("Could not authenticate request");
            return Unauthorized();
        }
    }
}
