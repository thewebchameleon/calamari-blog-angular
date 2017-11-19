using CB.Domain.Mappers.Global;
using CB.Repositories.Global;
using System.Threading.Tasks;
using System.Linq;

namespace CB.Domain.Services.Global
{
    public class GlobalService : IGlobalService
    {
        private readonly IGlobalRepository _repository;
        private readonly IGlobalMapper _mapper;

        public GlobalService(IGlobalRepository profileRepository, IGlobalMapper mapper)
        {
            _repository = profileRepository;
            _mapper = mapper;
        }

        public async Task<Models.GlobalConfig> GetGlobalConfig()
        {
            var config = await _repository.GetGlobalConfig();
            return _mapper.MapToGlobalConfig(config.First());
        }
    }
}
