using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Sixoclock.Onyx.TransactionStatuses.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.TransactionStatuses
{
    public class TransactionStatusAppService : OnyxAppServiceBase, ITransactionStatusAppService
    {
        private readonly IRepository<TransactionStatus> _transactionStatusRepository;
        public TransactionStatusAppService(IRepository<TransactionStatus> transactionStatusRepository)
        {
            _transactionStatusRepository = transactionStatusRepository;
        }
        public async Task CreateOrUpdateTransactionStatus(CreateOrUpdateTransactionStatusInput input)
        {
            var transactionStatus = ObjectMapper.Map<TransactionStatus>(input);

            if (input.Id == 0)
            {
                await _transactionStatusRepository.InsertAsync(transactionStatus);
            }
            else
            {
                await _transactionStatusRepository.UpdateAsync(transactionStatus);
            }
        }
        public async Task<GetTransactionStatusForEditOutput> GetTransactionStatusForEdit(EntityDto<int> input)
        {
            //Editing an existing transactionStatus
            var output = new GetTransactionStatusForEditOutput();
            if (input.Id == 0)
            {
                output.TransactionStatus = new TransactionStatusDto();
            }
            else
            {
                var transactionStatus = await _transactionStatusRepository.GetAsync(input.Id);

                output.TransactionStatus = ObjectMapper.Map<TransactionStatusDto>(transactionStatus);
            }

            return output;
        }
        public GetTransactionStatusesListOutput GetTransactionStatusesList()
        {
            IEnumerable<TransactionStatusListDto> _transactionStatussList = from transactionStatus in _transactionStatusRepository.GetAll()
                                                      select new TransactionStatusListDto
                                                      {
                                                          Id = transactionStatus.Id,
                                                          Name = transactionStatus.Value
                                                      };
            return new GetTransactionStatusesListOutput { TransactionStatuses = _transactionStatussList.ToList() };
        }
        public ListResultDto<TransactionStatusDto> GetTransactionStatus(GetTransactionStatusInput input)
        {
            var transactionStatuss = _transactionStatusRepository.GetAll()
            .WhereIf(!input.Filter.IsNullOrEmpty(),
                p => p.Value.ToLower().Contains(input.Filter.ToLower()) ||
                        p.Comment.ToLower().Contains(input.Filter.ToLower())
            )
            .WhereIf(!input.Value.IsNullOrEmpty(),
            p => p.Value.ToLower().Contains(input.Value.ToLower()))
            .WhereIf(!input.Comment.IsNullOrEmpty(),
            p => p.Comment.ToLower().Contains(input.Comment.ToLower()))
            .OrderBy(p => p.Value)
            .ThenBy(p => p.Comment)
            .ToList();

            return new ListResultDto<TransactionStatusDto>(ObjectMapper.Map<List<TransactionStatusDto>>(transactionStatuss));
        }
        public async Task DeleteTransactionStatus(EntityDto<int> input)
        {
            var transactionStatus = await _transactionStatusRepository.GetAsync(input.Id);
            await _transactionStatusRepository.DeleteAsync(transactionStatus);
        }
    }
}
