using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.Connectors;
using Sixoclock.Onyx.EVSEs.Dto;
using Sixoclock.Onyx.ModelEVSEs;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using LinqKit;

namespace Sixoclock.Onyx.EVSEs
{
    public class EVSEAppService : OnyxAppServiceBase, IEVSEAppService
    {
        private readonly IRepository<EVSE> _eVSERepository;
        private readonly IModelEVSEAppService _modelEVSEAppService;
        private readonly IConnectorAppService _connectorAppService;
        private readonly IEVSERuleSetExpressionBuilder _evseRuleSetExpressionBuilder;

        public EVSEAppService(IRepository<EVSE> eVSERepository,IModelEVSEAppService modelEVSEAppService,IConnectorAppService connectorAppService, IEVSERuleSetExpressionBuilder evseRuleSetExpressionBuilder)
        {
            _eVSERepository = eVSERepository;
            _modelEVSEAppService = modelEVSEAppService;
            _connectorAppService = connectorAppService;
            _evseRuleSetExpressionBuilder = evseRuleSetExpressionBuilder;
        }

        public async Task CreateOrUpdateEVSE(CreateOrUpdateEVSEInput input)
        {
            var eVSE = ObjectMapper.Map<EVSE>(input);

            if (input.Id == 0)
            {
                await _eVSERepository.InsertAsync(eVSE);
            }
            else
            {
                await _eVSERepository.UpdateAsync(eVSE);
            }
        }

        public GetEVSEForEditOutput GetEVSEForEdit(EntityDto<int> input)
        {
            //Editing an existing EVSE
            var output = new GetEVSEForEditOutput();
            if (input.Id == 0)
            {
                output.EVSE = new EVSEDto();
            }
            else
            {
                //var eVSE = await _eVSERepository.GetAsync(input.Id).include();

                var eVSE = (from evse in _eVSERepository.GetAll().Include(e => e.AvailabilityType)
                        .Include(e => e.Chargepoint.ChargepointModel).Include(e => e.MeterType).Where(e => e.Id == input.Id)
                            select new EVSEDto
                            {
                                Id = evse.Id,
                                EVSE_id = evse.EVSE_id,
                                Comment = evse.Comment,
                                VendorId = evse.Chargepoint.ChargepointModel.VendorId,
                                ChargepointId = evse.ChargepointId,
                                TenantId = evse.TenantId,
                                MeterTypeId = evse.MeterTypeId,
                                CreationTime = evse.CreationTime
                            }).FirstOrDefault();

                output.EVSE = ObjectMapper.Map<EVSEDto>(eVSE);
            }

            return output;
        }

        public async Task<GetEVSEsListOutput> GetEVSEsList()
        {
            var ruleCondition = await _evseRuleSetExpressionBuilder.BuiExpressionTree();
            IEnumerable<EVSEListDto> _eVSEsList = from eVSE in _eVSERepository.GetAll().AsExpandable().Include(x => x.Chargepoint)
                    .ThenInclude(x => x.Group).ThenInclude(x => x.Install).ThenInclude(x => x.Region)
                    .ThenInclude(x => x.Market).ThenInclude(x => x.Customer).ThenInclude(x => x.Segment)

                select new EVSEListDto
                {
                    Id = eVSE.Id,
                    EVSE_id = eVSE.EVSE_id
                };
            return new GetEVSEsListOutput { EVSEs = _eVSEsList.ToList() };
        }

        public async Task<GetEVSEsListOutput> GetEVSEsListByChargepoint(EntityDto<int> input)
        {
            var ruleCondition = await _evseRuleSetExpressionBuilder.BuiExpressionTree();
            IEnumerable<EVSEListDto> _eVSEsList = from eVSE in _eVSERepository.GetAll()
                    .Where(e => e.ChargepointId == input.Id).AsExpandable().Include(x => x.Chargepoint)
                    .ThenInclude(x => x.Group).ThenInclude(x => x.Install).ThenInclude(x => x.Region)
                    .ThenInclude(x => x.Market).ThenInclude(x => x.Customer).ThenInclude(x => x.Segment)
                select new EVSEListDto
                {
                    Id = eVSE.Id,
                    EVSE_id = eVSE.EVSE_id
                };
            return new GetEVSEsListOutput { EVSEs = _eVSEsList.ToList() };
        }

        public async Task<PagedResultDto<EVSEDto>> GetEVSE(GetEVSEInput input)
        {
            var ruleCondition = await _evseRuleSetExpressionBuilder.BuiExpressionTree();
            var query = (from eVSE in _eVSERepository.GetAll().AsExpandable().Include(x => x.Chargepoint)
                    .ThenInclude(x => x.Group).ThenInclude(x => x.Install).ThenInclude(x => x.Region)
                    .ThenInclude(x => x.Market).ThenInclude(x => x.Customer).ThenInclude(x => x.Segment)
                    .Include(e => e.EVSEStatus).Include(e => e.EVSEStatus.EVSEStatusCode)
                    .Include(e => e.AvailabilityType).Include(e => e.Chargepoint.ChargepointModel.Vendor)
                    .Include(e => e.Connectors)
                    .Include(e => e.MeterType).Where(ruleCondition)
                select new EVSEDto
                {
                    Id = eVSE.Id,
                    EVSE_id = eVSE.EVSE_id,
                    EVSEStatus = eVSE.EVSEStatus.EVSEStatusCode.Status,
                    AvailabilityType = eVSE.AvailabilityType.Value,
                    Comment = eVSE.Comment,
                    Vendor = eVSE.Chargepoint.ChargepointModel.Vendor.Name,
                    ModelName = eVSE.Chargepoint.ChargepointModel.ModelName,
                    ChargepointId = eVSE.ChargepointId,
                    ConnectorsCount = eVSE.Connectors.Count,
                    MeterType = eVSE.MeterType.Type,
                    CreationTime = eVSE.CreationTime
                }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.ModelName.Contains(input.Filter) || item.Comment.Contains(input.Comment))
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), item => item.ModelName == input.Name)
                .WhereIf(!input.Comment.IsNullOrWhiteSpace(), item => item.Comment == input.Comment)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<EVSEDto>(resultCount, results.ToList());
        }
        public async Task<ListResultDto<EVSEDto>> GetEVSEByChargepointId(EntityDto<int> input)
        {
            var ruleCondition = await _evseRuleSetExpressionBuilder.BuiExpressionTree();
            var eVSEs = from eVSE in _eVSERepository.GetAll().Include(e => e.EVSEStatus)
                    .Include(e => e.EVSEStatus.EVSEStatusCode).Include(e => e.EVSEStatus.ErrorCode)
                    .Include(e => e.AvailabilityType)
                    .Include(e => e.Chargepoint.ChargepointModel.Vendor).Include(e => e.Connectors)
                    .Include(e => e.MeterType)
                    .Where(e => e.ChargepointId == input.Id).AsExpandable().Include(x => x.Chargepoint)
                    .ThenInclude(x => x.Group).ThenInclude(x => x.Install).ThenInclude(x => x.Region)
                    .ThenInclude(x => x.Market).ThenInclude(x => x.Customer).ThenInclude(x => x.Segment)
                select new EVSEDto
                {
                    Id = eVSE.Id,
                    EVSE_id = eVSE.EVSE_id,
                    EVSEStatus = eVSE.EVSEStatus.EVSEStatusCode.Status,
                    EVSEStatusId = eVSE.EVSEStatusId,
                    EVSEError = eVSE.EVSEStatus.ErrorCode.Value,
                    AvailabilityType = eVSE.AvailabilityType.Value,
                    AvailabilityTypeId = eVSE.AvailabilityTypeId,
                    Comment = eVSE.Comment,
                    Vendor = eVSE.Chargepoint.ChargepointModel.Vendor.Name,
                    ModelName = eVSE.Chargepoint.ChargepointModel.ModelName,
                    ChargepointId = eVSE.ChargepointId,
                    ConnectorsCount = eVSE.Connectors.Count,
                    MeterType = eVSE.MeterType.Type,
                    CreationTime = eVSE.CreationTime
                };

            return new ListResultDto<EVSEDto>(ObjectMapper.Map<List<EVSEDto>>(eVSEs));
        }
        public int GetEVSEIdCount(EntityDto<int> input)
        {
            int count = _eVSERepository.GetAll().Count(c => c.ChargepointId == input.Id);
            return count + 1;
        }
        public async Task CopyModelEVSEsAndConnectorsToEVSEsAndConnectors(int chargepointModelId,int chargepointId)
        {
            var modelEVSEs = _modelEVSEAppService.GetModelEVSEsList(chargepointModelId);
            CreateOrUpdateEVSEInput evse;

            foreach (var modelEVSE in modelEVSEs.ModelEVSEs)
            {
                evse = new CreateOrUpdateEVSEInput();
                evse.ChargepointId = chargepointId;
                evse.MeterTypeId = modelEVSE.MeterTypeId;
                evse.IsDeleted = modelEVSE.IsDeleted;
                evse.Comment = modelEVSE.Comment;
                evse.EVSE_id = modelEVSE.EVSEId;

                int evseId = await _eVSERepository.InsertAndGetIdAsync(ObjectMapper.Map<EVSE>(evse));

                await _connectorAppService.CopyModelConnectorToConnector(modelEVSE.Id, evseId);

                await CurrentUnitOfWork.SaveChangesAsync();
            }

        }

        public async Task DeleteEVSE(EntityDto<int> input)
        {
            var eVSE = await _eVSERepository.GetAsync(input.Id);
            await _eVSERepository.DeleteAsync(eVSE);
        }
    }
}
