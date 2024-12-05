using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Sixoclock.Onyx.ConnectorTypes.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.ConnectorTypes
{
    public class ConnectorTypeAppService : OnyxAppServiceBase, IConnectorTypeAppService
    {
        private readonly IRepository<ConnectorType> _connectorTypeRepository;
        public ConnectorTypeAppService(IRepository<ConnectorType> connectorTypeRepository)
        {
            _connectorTypeRepository = connectorTypeRepository;
        }
        public async Task CreateOrUpdateConnectorType(CreateOrUpdateConnectorTypeInput input)
        {
            var connectorType = ObjectMapper.Map<ConnectorType>(input);

            if (input.Id == 0)
            {
                await _connectorTypeRepository.InsertAsync(connectorType);
            }
            else
            {
                await _connectorTypeRepository.UpdateAsync(connectorType);
            }
        }
        public async Task<GetConnectorTypeForEditOutput> GetConnectorTypeForEdit(EntityDto<int> input)
        {
            //Editing an existing connector type
            var output = new GetConnectorTypeForEditOutput();
            if (input.Id == 0)
            {
                output.ConnectorType = new ConnectorTypeDto();
            }
            else
            {
                var connectorType = await _connectorTypeRepository.GetAsync(input.Id);

                output.ConnectorType = ObjectMapper.Map<ConnectorTypeDto>(connectorType);
            }

            return output;
        }
        public GetConnectorTypesListOutput GetConnectorTypesList()
        {
            IEnumerable<ConnectorTypeListDto> _connectorTypesList = from connectorType in _connectorTypeRepository.GetAll()
                                                                    select new ConnectorTypeListDto
                                                                    {
                                                                        Id = connectorType.Id,
                                                                        Name = connectorType.ConnectorName
                                                                    };
            return new GetConnectorTypesListOutput { ConnectorTypes = _connectorTypesList.ToList() };
        }
        public ListResultDto<ConnectorTypeDto> GetConnectorType(GetConnectorTypeInput input)
        {
            var connectorTypes = from connectorType in _connectorTypeRepository.GetAll()
            .WhereIf(!input.Filter.IsNullOrEmpty(),
                p => p.ConnectorName.ToLower().Contains(input.Filter.ToLower()) ||
                        p.Comment.ToLower().Contains(input.Filter.ToLower())
            )
            .WhereIf(!input.ConnectorName.IsNullOrEmpty(),
            p => p.ConnectorName.ToLower().Contains(input.ConnectorName.ToLower()))
            .WhereIf(!input.Comment.IsNullOrEmpty(),
            p => p.Comment.ToLower().Contains(input.Comment.ToLower()))
            .OrderBy(p => p.ConnectorName)
            .ThenBy(p => p.Comment)
                                 select new ConnectorTypeDto
                                 {
                                     Id = connectorType.Id,
                                     ConnectorName = connectorType.ConnectorName,
                                     Comment = connectorType.Comment
                                 };

            return new ListResultDto<ConnectorTypeDto>(ObjectMapper.Map<List<ConnectorTypeDto>>(connectorTypes));
        }
        public async Task DeleteConnectorType(EntityDto<int> input)
        {
            var connectorType = await _connectorTypeRepository.GetAsync(input.Id);
            await _connectorTypeRepository.DeleteAsync(connectorType);
        }
    }
}
