using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.Connectors.Dto;
using Sixoclock.Onyx.ModelConnectors;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using LinqKit;

namespace Sixoclock.Onyx.Connectors
{
    public class ConnectorAppService : OnyxAppServiceBase,IConnectorAppService
    {
        private readonly IRepository<Connector> _connectorRepository;
        private readonly IModelConnectorAppService _modelConnectorAppService;
        private readonly IConnectorRuleSetExpressionBuilder _connectorRuleSetExpressionBuilder;

        public ConnectorAppService(IRepository<Connector> connectorRepository,IModelConnectorAppService modelConnectorAppService, IConnectorRuleSetExpressionBuilder connectorRuleSetExpressionBuilder)
        {
            _connectorRepository = connectorRepository;
            _modelConnectorAppService = modelConnectorAppService;
            _connectorRuleSetExpressionBuilder = connectorRuleSetExpressionBuilder;
        }

        public async Task CreateOrUpdateConnector(CreateOrUpdateConnectorInput input)
        {
            var connector = ObjectMapper.Map<Connector>(input);

            if (input.Id == 0)
            {
                await _connectorRepository.InsertAsync(connector);
            }
            else
            {
                await _connectorRepository.UpdateAsync(connector);
            }
        }
        public GetConnectorForEditOutput GetConnectorForEdit(EntityDto<int> input)
        {
            //Editing an existing Connector
            var output = new GetConnectorForEditOutput();
            if (input.Id == 0)
            {
                output.Connector = new ConnectorDto();
            }
            else
            {
                var connector = (from conn in _connectorRepository.GetAll().Where(e => e.Id == input.Id)
                                 select new ConnectorDto
                                 {
                                     Id = conn.Id,
                                     Comment = conn.Comment,
                                     EVSEId = conn.EVSEId,
                                     ConnectorTypeId = conn.ConnectorTypeId,
                                     CapacityId = conn.CapacityId,
                                     TenantId = conn.TenantId,
                                     CreationTime = conn.CreationTime
                                 }).FirstOrDefault();

                output.Connector = ObjectMapper.Map<ConnectorDto>(connector);
            }

            return output;
        }
        
        public async Task CopyModelConnectorToConnector(int modelEVSEId,int evseId)
        {
            var modelConnectors = _modelConnectorAppService.GetModelConnectorsByModelEVSEList(modelEVSEId);

            CreateOrUpdateConnectorInput connectorInput = new CreateOrUpdateConnectorInput();

            foreach (var modelConnector in modelConnectors.ModelConnectors)
            {
                connectorInput.CapacityId = modelConnector.CapacityId;
                connectorInput.ConnectorTypeId = modelConnector.ConnectorTypeId;
                connectorInput.EVSEId = evseId;
                connectorInput.Comment = modelConnector.Comment;
                
                await CreateOrUpdateConnector(connectorInput);
                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        public async Task<PagedResultDto<ConnectorDto>> GetConnector(GetConnectorInput input)
        {
            var ruleCondition = await _connectorRuleSetExpressionBuilder.BuiExpressionTree();
            var query = (from modelConnector in _connectorRepository.GetAll().AsExpandable().Include(m => m.EVSE)
                    .ThenInclude(x => x.Chargepoint).ThenInclude(x => x.Group).ThenInclude(x => x.Install)
                    .ThenInclude(x => x.Region)
                    .ThenInclude(x => x.Market).ThenInclude(x => x.Customer).ThenInclude(x => x.Segment)
                    .Include(m => m.EVSE.Chargepoint.ChargepointModel.Vendor).Include(m => m.ConnectorType).Where(ruleCondition)
                select new ConnectorDto
                {
                    Id = modelConnector.Id,
                    Comment = modelConnector.Comment,
                    ConnectorTypeId = modelConnector.ConnectorTypeId,
                    ConnectorType = modelConnector.ConnectorType.ConnectorName,
                    ModelName = modelConnector.EVSE.Chargepoint.ChargepointModel.ModelName,
                    EVSEId = modelConnector.EVSE.EVSE_id,
                    CapacityId = modelConnector.CapacityId,
                    Vendor = modelConnector.EVSE.Chargepoint.ChargepointModel.Vendor.Name,
                    ChargepointId = modelConnector.EVSE.ChargepointId,
                    TenantId = modelConnector.TenantId,
                    CreationTime = modelConnector.CreationTime
                }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.ModelName.Contains(input.Filter) || item.Comment.Contains(input.Filter))
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), item => item.ModelName.Contains(input.Name))
                .WhereIf(!input.Comment.IsNullOrWhiteSpace(), item => item.Comment.Contains(input.Comment))
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<ConnectorDto>(resultCount, results.ToList());
        }
        
        public async Task DeleteConnector(EntityDto<int> input)
        {
            var connector = await _connectorRepository.GetAsync(input.Id);
            await _connectorRepository.DeleteAsync(connector);
        }
    }
}
