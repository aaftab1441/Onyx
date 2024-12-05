using Abp.Domain.Repositories;
using Sixoclock.Onyx.Powers.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Powers
{
    public class PowerAppService : OnyxAppServiceBase, IPowerAppService
    {
        private readonly IRepository<Power> _powerRepository;
        public PowerAppService(IRepository<Power> powerRepository)
        {
            _powerRepository = powerRepository;
        }
        public GetPowersListOutput GetPowersList()
        {
            IEnumerable<PowerListDto> _powersList = from power in _powerRepository.GetAll()
                                                  select new PowerListDto
                                                  {
                                                      Id = power.Id,
                                                      Name = power.PowerName
                                                  };
            return new GetPowersListOutput { Powers = _powersList.ToList() };
        }
    }
}
