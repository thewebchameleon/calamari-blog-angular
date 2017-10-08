using System.Threading.Tasks;

namespace CB.Domain.Services.Profile
{
    public interface IProfileService
    {
        Task<Models.Profile> GetProfile();
    }
}
