using Abp.Domain.Repositories;
using Sixoclock.Onyx.ResetStatuses.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.ResetStatuses
{
    public class ResetStatusAppService : OnyxAppServiceBase, IResetStatusAppService
    {
        private readonly IRepository<ResetStatus> _resetStatusRepository;
        public ResetStatusAppService(IRepository<ResetStatus> resetStatusRepository)
        {
            _resetStatusRepository = resetStatusRepository;
        }
        
        public GetResetStatusesListOutput GetResetStatussList()
        {
            IEnumerable<ResetStatusListDto> _resetStatussList = from resetStatus in _resetStatusRepository.GetAll()
                                                        select new ResetStatusListDto
                                                        {
                                                            Id = resetStatus.Id,
                                                            Name = resetStatus.ResetStatusValue
                                                        };
            return new GetResetStatusesListOutput { ResetStatuses = _resetStatussList.ToList() };
        }
        public int GetResetStatus(string input)
        {
            var resetStatuses = from resetStatus in _resetStatusRepository.GetAll().Where(r=>r.ResetStatusValue.Contains(input))
                               select resetStatus;

            return resetStatuses.FirstOrDefault().Id;
        }
    }
}
