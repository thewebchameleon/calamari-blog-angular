using CB.Infrastructure.Repositories;
using CB.CMS.SquidexClient;
using CB.Infrastructure.Cache;
using CB.CMS.Models.Global;
using System.Threading.Tasks;
using System.Collections.Generic;
using CB.CMS.Models.Constants;

namespace CB.Repositories.Global
{
    public class GlobalRepository : BaseRepository, IGlobalRepository
    {
        public GlobalRepository(ISquidexClientFactory clientFactory, ICacheProvider cache) : base(clientFactory, cache)
        {
        }

        #region Public Methods

        public async Task<List<GlobalEntity>> GetGlobalConfig()
        {
            return await GetItems<GlobalEntity, GlobalDTO>(SchemaNameConstants.Global);
        }

        #endregion
    }
}
