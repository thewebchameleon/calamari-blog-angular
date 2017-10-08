using System.Threading.Tasks;
using CB.CMS.Models.Profile;
using CB.Infrastructure.Repositories;
using CB.CMS.SquidexClient;
using CB.Infrastructure.Cache;
using CB.CMS.Models.Constants;
using System.Collections.Generic;

namespace CB.Repositories.Profile
{
    public class ProfileRepository : BaseRepository, IProfileRepository
    {
        public ProfileRepository(ISquidexClientFactory clientFactory, ICacheProvider cache) : base(clientFactory, cache) { }

        public async Task<List<ProfileEntity>> GetProfiles()
        {
            return await GetItems<ProfileEntity, ProfileDTO>(SchemaNameConstants.Profile);
        }
    }
}
