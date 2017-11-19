using CB.CMS.Models.Profile;
using CB.Domain.Mappers;
using System.Linq;
using Microsoft.Extensions.Options;
using CB.CMS.SquidexClient;

namespace CB.Domain.Services.Profile.Mappers
{
    public class ProfileMapper : BaseMapper, IProfileMapper
    {
        public ProfileMapper(IOptions<SquidexSettings> config) : base(config) { }

        public Models.Profile MapToProfile(ProfileEntity model)
        {
            return new Models.Profile()
            {
                Portrait = ResolveAssetURL(model.Data.Portrait.FirstOrDefault()),
                Title = model.Data.Title,
                Name = model.Data.Name,
                Body = model.Data.Body
            };
        }
    }
}
