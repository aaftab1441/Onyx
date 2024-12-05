using Abp.Domain.Repositories;
using Sixoclock.Onyx.Units.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Units
{
    public class UnitAppService : OnyxAppServiceBase, IUnitAppService
    {
        private readonly IRepository<Unit> _unitRepository;
        public UnitAppService(IRepository<Unit> unitRepository)
        {
            _unitRepository = unitRepository;
        }
        public GetUnitsListOutput GetUnitsList()
        {
            IEnumerable<UnitListDto> _unitsList = from unit in _unitRepository.GetAll()
                                                      select new UnitListDto
                                                      {
                                                          Id = unit.Id,
                                                          Name = unit.UnitName
                                                      };
            return new GetUnitsListOutput { Units = _unitsList.ToList() };
        }
    }
}
