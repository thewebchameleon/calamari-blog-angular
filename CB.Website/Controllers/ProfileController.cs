using CB.Domain.Models;
using CB.Domain.Services.Profile;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CB.Website.Controllers
{
    [Route("api/[controller]")]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<Profile> Get()
        {
            //log started
            var result = await _profileService.GetProfile();
            //log completed
            return result;
        }
    }
}
