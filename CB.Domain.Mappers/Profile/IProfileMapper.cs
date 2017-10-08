using CB.CMS.Models.Profile;

namespace CB.Domain.Services.Profile.Mappers
{
    public interface IProfileMapper
    {
        Models.Profile MapToProfile(ProfileEntity model);
    }
}
