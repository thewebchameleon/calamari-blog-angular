using CB.CMS.Models.Global;

namespace CB.Domain.Mappers.Global
{
    public interface IGlobalMapper
    {
        Models.GlobalConfig MapToGlobalConfig(GlobalEntity model);
    }
}
