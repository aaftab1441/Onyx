using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.ConnectorStatusCodes.Dto;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.ConnectorStatusCodes
{
    public class ConnectorStatusCodeAppService : OnyxAppServiceBase, IConnectorStatusCodeAppService
    {
        private readonly IRepository<ConnectorStatusCode> _connectorStatusCodeRepository;
        public ConnectorStatusCodeAppService(IRepository<ConnectorStatusCode> connectorStatusCodeRepository)
        {
            _connectorStatusCodeRepository = connectorStatusCodeRepository;
        }
        public async Task CreateOrUpdateConnectorStatusCode(CreateOrUpdateConnectorStatusCodeInput input)
        {
            var connectorStatusCode = ObjectMapper.Map<ConnectorStatusCode>(input);

            if (input.Id == 0)
            {
                await _connectorStatusCodeRepository.InsertAsync(connectorStatusCode);
            }
            else
            {
                await _connectorStatusCodeRepository.UpdateAsync(connectorStatusCode);
            }
        }
        public async Task<GetConnectorStatusCodeForEditOutput> GetConnectorStatusCodeForEdit(EntityDto<int> input)
        {
            //Editing an existing Connector Status Code
            var output = new GetConnectorStatusCodeForEditOutput();
            if (input.Id == 0)
            {
                output.ConnectorStatusCode = new ConnectorStatusCodeDto();
            }
            else
            {
                var connectorStatusCode = await _connectorStatusCodeRepository.GetAsync(input.Id);

                output.ConnectorStatusCode = ObjectMapper.Map<ConnectorStatusCodeDto>(connectorStatusCode);
            }

            return output;
        }
        public async Task<PagedResultDto<ConnectorStatusCodeDto>> GetConnectorStatusCode(GetConnectorStatusCodeInput input)
        {
            var query = (from connectorStatusCode in _connectorStatusCodeRepository.GetAll()
                         select new ConnectorStatusCodeDto
                         {
                             Id = connectorStatusCode.Id,
                             Status = connectorStatusCode.Status,
                             Comment = connectorStatusCode.Comment
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.Status.Contains(input.Filter) || item.Comment.Contains(input.Filter))
                .WhereIf(!input.Status.IsNullOrWhiteSpace(), item => item.Status == input.Status)
                .WhereIf(!input.Comment.IsNullOrWhiteSpace(), item => item.Comment == input.Comment)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<ConnectorStatusCodeDto>(resultCount, results.ToList());
        }
        public async Task DeleteConnectorStatusCode(EntityDto<int> input)
        {
            var connectorStatusCode = await _connectorStatusCodeRepository.GetAsync(input.Id);
            await _connectorStatusCodeRepository.DeleteAsync(connectorStatusCode);
        }
    }
}
