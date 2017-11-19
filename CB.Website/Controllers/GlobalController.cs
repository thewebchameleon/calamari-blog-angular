using CB.Domain.Models;
using CB.Domain.Services.Global;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CB.Website.Controllers
{
    [Route("api/[controller]")]
    public class GlobalController : Controller
    {
        private readonly IGlobalService _globalService;

        public GlobalController(IGlobalService globalService)
        {
            _globalService = globalService;
        }

        [HttpGet]
        public async Task<GlobalConfig> Get()
        {
            //log started
            var result = await _globalService.GetGlobalConfig();
            //log completed
            return result;
        }
    }
}
