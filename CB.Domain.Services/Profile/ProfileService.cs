using CB.Domain.Services.Profile.Mappers;
using CB.Repositories.Profile;
using System.Linq;
using System.Threading.Tasks;

namespace CB.Domain.Services.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _repository;
        private readonly IProfileMapper _mapper;

        public ProfileService(IProfileRepository profileRepository, IProfileMapper mapper)
        {
            _repository = profileRepository;
            _mapper = mapper;
        }

        public async Task<Models.Profile> GetProfile()
        {
            var profiles = await _repository.GetProfiles();
            var latestProfile = profiles.OrderByDescending(p => p.LastModified).First();

            return _mapper.MapToProfile(latestProfile);
        }
    }
}
