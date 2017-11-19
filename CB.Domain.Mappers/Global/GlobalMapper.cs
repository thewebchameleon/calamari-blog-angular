using CB.CMS.Models.Global;
using CB.CMS.SquidexClient;
using CB.Domain.Models;
using Microsoft.Extensions.Options;
using System.Linq;

namespace CB.Domain.Mappers.Global
{
    public class GlobalMapper : BaseMapper, IGlobalMapper
    {
        public GlobalMapper(IOptions<SquidexSettings> settings) : base(settings)
        {
        }

        public GlobalConfig MapToGlobalConfig(GlobalEntity model)
        {
            return new GlobalConfig()
            {
                HeartIconURL = ResolveAssetURL(model.Data.HeartIcon.First()),
                ApplicationName = model.Data.Name
            };
        }
    }
}
