using CB.CMS.Models.Global;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CB.Repositories.Global
{
    public interface IGlobalRepository
    {
        Task<List<GlobalEntity>> GetGlobalConfig();
    }
}
