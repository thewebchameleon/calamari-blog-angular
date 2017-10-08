using CB.CMS.Models.Profile;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CB.Repositories.Profile
{
    public interface IProfileRepository
    {
        Task<List<ProfileEntity>> GetProfiles();
    }
}
