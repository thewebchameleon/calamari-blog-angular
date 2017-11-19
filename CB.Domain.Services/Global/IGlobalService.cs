using System.Threading.Tasks;

namespace CB.Domain.Services.Global
{
    public interface IGlobalService
    {
        Task<Models.GlobalConfig> GetGlobalConfig();
    }
}
