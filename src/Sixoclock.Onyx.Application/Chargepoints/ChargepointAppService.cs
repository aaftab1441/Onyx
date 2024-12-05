using System;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.ChargepointFeatures;
using Sixoclock.Onyx.ChargepointKeyValues;
using Sixoclock.Onyx.Chargepoints.Dto;
using Sixoclock.Onyx.Connectors;
using Sixoclock.Onyx.EVSEs;
using Sixoclock.Onyx.KeyValues;
using Sixoclock.Onyx.OCPPFeatures;
using Sixoclock.Onyx.RemoteStartStopEvents;
using Sixoclock.Onyx.RemoteStartStopEvents.Dto;
using Sixoclock.Onyx.ResetEvents;
using Sixoclock.Onyx.ResetEvents.Dto;
using Sixoclock.Onyx.ResetStatuses;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Sixoclock.Onyx.API.JsonClient;
using Sixoclock.Onyx.API.JsonSchema;
using Sixoclock.Onyx.API.JsonSchema.Models;
using System.Collections.ObjectModel;
using Abp.Runtime.Session;
using Sixoclock.Onyx.UnlockEvents;
using Sixoclock.Onyx.UnlockEvents.Dto;
using Sixoclock.Onyx.UnlockStatuses;
using Sixoclock.Onyx.ClearCacheStatuses;
using Sixoclock.Onyx.ClearCacheEvents;
using Sixoclock.Onyx.ClearCacheEvents.Dto;
using Sixoclock.Onyx.API.JsonSchema.Internal;
using Sixoclock.Onyx.ConfigTypes;
using Sixoclock.Onyx.ConfigEvents;
using Sixoclock.Onyx.ConfigEvents.Dto;
using Sixoclock.Onyx.ConfigStatuses;
using Sixoclock.Onyx.AvailabilityEvents;
using Sixoclock.Onyx.AvailabilityEvents.Dto;
using Sixoclock.Onyx.AvailabilityStatuses;
using Abp.Linq.Extensions;
using LinqKit;

namespace Sixoclock.Onyx.Chargepoints
{
    public class ChargepointAppService : OnyxAppServiceBase, IChargepointAppService
    {
        private readonly IRepository<Chargepoint> _chargepointRepository;
        private readonly IConnectorAppService _connectorAppService;
        private readonly IChargepointFeatureAppService _chargepointFeatureAppService;
        private readonly IChargepointKeyValueAppService _chargepointKeyValueAppService;
        private readonly IKeyValueAppService _keyValueAppService;
        private readonly IOCPPFeatureAppService _oCPPFeatureAppService;
        private readonly IEVSEAppService _iEVSEAppService;
        private readonly IResetEventAppService _resetEventAppService;
        private readonly IResetStatusAppService _resetStatusAppService;
        private readonly IDeviceManager _deviceManager;
        private readonly IRemoteStartStopEventAppService _remoteStartStopEventAppService;
        private readonly IRepository<RemoteStartStopEventType> _remoteStartStopEventTypeRepository;
        private readonly IRepository<RemoteStartStopStatus> _remoteStartStopStatusRepository;
        private readonly IUnlockEventAppService _unlockEventAppService;
        private readonly IUnlockStatusAppService _unlockStatusAppService;
        private readonly IClearCacheStatusAppService _clearCacheStatusAppService;
        private readonly IClearCacheEventAppService _clearCacheEventAppService;
        private readonly IConfigTypeAppService _configTypeAppService;
        private readonly IConfigStatusAppService _configStatusAppService;
        private readonly IConfigEventAppService _configEventAppService;
        private readonly IAvailabilityEventAppService _availabilityEventAppService;
        private readonly IAvailabilityStatusAppService _availabilityStatusAppService;
        private readonly IAbpSession _abpSession;
        private readonly IChargepointRuleSetExpressionBuilder _chargepointRuleSetExpressionBuilder;
        public ChargepointAppService(IRepository<Chargepoint> chargepointRepository, IConnectorAppService connectorAppService,
            IChargepointFeatureAppService chargepointFeatureAppService, IChargepointKeyValueAppService chargepointKeyValueAppService,
            IEVSEAppService iEVSEAppService, IKeyValueAppService keyValueAppService, IOCPPFeatureAppService oCPPFeatureAppService,
            IResetEventAppService resetEventAppService, IDeviceManager deviceManager,
            IResetStatusAppService resetStatusAppService, IRemoteStartStopEventAppService remoteStartStopEventAppService,
            IRepository<RemoteStartStopEventType> remoteStartStopEventTypeRepository, IRepository<RemoteStartStopStatus> remoteStartStopStatusRepository,
            IUnlockEventAppService unlockEventAppService, IUnlockStatusAppService unlockStatusAppService,
            IClearCacheStatusAppService clearCacheStatusAppService, IClearCacheEventAppService clearCacheEventAppService,
            IConfigTypeAppService configTypeAppServic, IConfigEventAppService configEventAppService,
            IConfigStatusAppService configStatusAppService, IAvailabilityEventAppService availabilityEventAppService,
            IAvailabilityStatusAppService availabilityStatusAppService, IAbpSession iabpSession, IChargepointRuleSetExpressionBuilder chargepointRuleSetExpressionBuilder)
        {
            _chargepointRepository = chargepointRepository;
            _connectorAppService = connectorAppService;
            _chargepointFeatureAppService = chargepointFeatureAppService;
            _chargepointKeyValueAppService = chargepointKeyValueAppService;
            _iEVSEAppService = iEVSEAppService;
            _keyValueAppService = keyValueAppService;
            _oCPPFeatureAppService = oCPPFeatureAppService;
            _resetEventAppService = resetEventAppService;
            _resetStatusAppService = resetStatusAppService;
            _deviceManager = deviceManager;
            _remoteStartStopEventAppService = remoteStartStopEventAppService;
            _remoteStartStopEventTypeRepository = remoteStartStopEventTypeRepository;
            _remoteStartStopStatusRepository = remoteStartStopStatusRepository;
            _unlockEventAppService = unlockEventAppService;
            _unlockStatusAppService = unlockStatusAppService;
            _clearCacheStatusAppService = clearCacheStatusAppService;
            _clearCacheEventAppService = clearCacheEventAppService;
            _configTypeAppService = configTypeAppServic;
            _configEventAppService = configEventAppService;
            _configStatusAppService = configStatusAppService;
            _availabilityEventAppService = availabilityEventAppService;
            _availabilityStatusAppService = availabilityStatusAppService;
            _abpSession = iabpSession;
            _chargepointRuleSetExpressionBuilder = chargepointRuleSetExpressionBuilder;
        }

        public async Task CreateOrUpdateChargepoint(CreateOrUpdateChargepointInput input)
        {

            if (input.Id == 0)
            {
                for (int i = 0; i < Convert.ToInt32(input.Place); i++)
                {
                    Chargepoint point = new Chargepoint();
                    point.ChargepointModelId = input.ChargepointModelId;
                    point.Comment = input.Comment;
                    point.AdminStatusId = input.AdminStatusId;
                    point.GroupId = input.GroupId;

                    point.Place = input.Place;
                    point.NetworkAddress = input.NetworkAddress;
                    if (input.OCPPVersionId != 0)
                        point.OCPPVersionId = input.OCPPVersionId;
                    if (input.OCPPTransportId != 0)
                        point.OCPPTransportId = input.OCPPTransportId;
                    point.IccId = input.IccId;
                    point.Imsi = input.Imsi;
                    point.Port = input.Port;

                    var chargepoint = ObjectMapper.Map<Chargepoint>(point);

                    int chargepointId = await _chargepointRepository.InsertAndGetIdAsync(chargepoint);
                    await CurrentUnitOfWork.SaveChangesAsync();

                    await _chargepointFeatureAppService.CopyModelFeaturesToChargepointFeatures(input.ChargepointModelId, chargepointId);

                    await _chargepointKeyValueAppService.CopyModelKeyValuesToChargepointKeyValues(input.ChargepointModelId, chargepointId);

                    await _iEVSEAppService.CopyModelEVSEsAndConnectorsToEVSEsAndConnectors(input.ChargepointModelId, chargepointId);
                }
            }
            else
            {
                var chargepoint = ObjectMapper.Map<Chargepoint>(input);

                await _chargepointRepository.UpdateAsync(chargepoint);

                EntityDto<int> chargepointInput = new EntityDto<int>();
                chargepointInput.Id = input.Id;
                await _chargepointFeatureAppService.DeleteChargepointFeaturesByChargepoint(chargepointInput, input.TenantId);

                await _chargepointFeatureAppService.CreateChargepointFeaturesAndKeys(input.ChargepointFeature, input.Id);
            }
        }
        public GetChargepointForEditOutput GetChargepointForEdit(EntityDto<int> input)
        {
            //Editing an existing Chargepoint
            var output = new GetChargepointForEditOutput();
            if (input.Id == 0)
            {
                output.Chargepoint = new ChargepointDto();
                output.Chargepoint.OCPPFeatures = _oCPPFeatureAppService.GetOCPPFeaturesList();
            }
            else
            {
                var chargepoints = from chargepoint in _chargepointRepository.GetAll().Where(c => c.Id == input.Id).Include(c => c.Group).Include(c => c.ChargepointModel)
                               .Include(c => c.ChargepointModel.Vendor).Include(c => c.ChargepointModel.MountType).Include(c => c.AdminStatus)
                                   select new ChargepointDto
                                   {
                                       Id = chargepoint.Id,
                                       Place = chargepoint.Place,
                                       Comment = chargepoint.Comment,
                                       AdminStatusId = chargepoint.AdminStatusId,
                                       ChargepointModelId = chargepoint.ChargepointModelId,
                                       GroupId = chargepoint.GroupId,
                                       GroupName = chargepoint.Group.GroupName,
                                       VendorName = chargepoint.ChargepointModel.Vendor.Name,
                                       ModelName = chargepoint.ChargepointModel.ModelName,
                                       OCPPVersionId = chargepoint.OCPPVersionId,
                                       OCPPTransportId = chargepoint.OCPPTransportId,
                                       Identity = chargepoint.Identity,
                                       TenantId = chargepoint.TenantId,
                                       CreationTime = chargepoint.CreationTime,
                                       MountTypeName = chargepoint.ChargepointModel.MountType.Name,
                                       NetworkAddress = chargepoint.NetworkAddress,
                                       Port = chargepoint.Port,
                                       IccId = chargepoint.IccId,
                                       Imsi = chargepoint.Imsi,
                                       Status = chargepoint.AdminStatus.Status
                                   };

                output.Chargepoint = new ChargepointDto();
                output.Chargepoint = chargepoints.FirstOrDefault();
                if (output.Chargepoint.OCPPVersionId != null)
                {
                    EntityDto<int> version = new EntityDto<int>();
                    version.Id = (int)output.Chargepoint.OCPPVersionId;

                    output.Chargepoint.OCPPFeatures = _oCPPFeatureAppService.GetOCPPFeaturesByOCPPVersionList(version);

                    var modelFeatures = _chargepointFeatureAppService.GetChargepointFeaturesList(input.Id);
                    foreach (var ocppFeature in output.Chargepoint.OCPPFeatures.OCPPFeatures)
                    {
                        if (modelFeatures.ChargepointFeatures.Any(m => m.OCPPFeatureId == ocppFeature.Id))
                        {
                            ocppFeature.Assigned = true;
                        }
                    }
                }
                else
                {
                    output.Chargepoint.OCPPFeatures = _oCPPFeatureAppService.GetOCPPFeaturesList();
                }
            }

            return output;
        }
        public async Task<GetChargepointsListOutput> GetChargepointsList()
        {
            var ruleCondition = await _chargepointRuleSetExpressionBuilder.BuiExpressionTree();
            IEnumerable<ChargepointListDto> _chargepointsList = from chargepoint in _chargepointRepository.GetAll()
                    .AsExpandable().Include(x => x.Group).ThenInclude(x => x.Install).ThenInclude(x => x.Region)
                    .ThenInclude(x => x.Market).ThenInclude(x => x.Customer).ThenInclude(x => x.Segment)
                    .Include(x => x.AdminStatus).Where(ruleCondition)
                select new ChargepointListDto
                {
                    Id = chargepoint.Id,
                    Name = chargepoint.Place
                };
            return new GetChargepointsListOutput { Chargepoints = _chargepointsList.ToList() };
        }
        public async Task<GetChargepointsListOutput> GetChargepointsListByGroup(EntityDto<int> input)
        {
            var ruleCondition = await _chargepointRuleSetExpressionBuilder.BuiExpressionTree();
            IEnumerable<ChargepointListDto> _chargepointsList = from chargepoint in _chargepointRepository.GetAll()
                    .Where(f => f.GroupId == input.Id).AsExpandable().Include(x => x.Group).ThenInclude(x => x.Install)
                    .ThenInclude(x => x.Region)
                    .ThenInclude(x => x.Market).ThenInclude(x => x.Customer).ThenInclude(x => x.Segment)
                    .Include(x => x.AdminStatus).Where(ruleCondition)
                select new ChargepointListDto
                {
                    Id = chargepoint.Id,
                    Name = chargepoint.Place
                };
            return new GetChargepointsListOutput { Chargepoints = _chargepointsList.ToList() };
        }
        public async Task<PagedResultDto<ChargepointDto>> GetChargepoint(GetChargepointInput input)
        {

            var ruleCondition = await _chargepointRuleSetExpressionBuilder.BuiExpressionTree();
            var query = (from chargepoint in _chargepointRepository.GetAll().AsExpandable().Include(x => x.Group)
                    .ThenInclude(x => x.Install).ThenInclude(x => x.Region)
                    .ThenInclude(x => x.Market).ThenInclude(x => x.Customer).ThenInclude(x => x.Segment)
                    .Include(c => c.ChargepointModel).Include(c => c.ChargepointModel.Vendor)
                    .Include(c => c.ChargepointModel.MountType)
                    .Include(c => c.AdminStatus).Include(c => c.EVSEs).Where(ruleCondition)
                select new ChargepointDto
                {
                    Id = chargepoint.Id,
                    Place = chargepoint.Place,
                    Comment = chargepoint.Comment,
                    AdminStatusId = chargepoint.AdminStatusId,
                    ChargepointModelId = chargepoint.ChargepointModelId,
                    GroupId = chargepoint.GroupId,
                    GroupName = chargepoint.Group.GroupName,
                    VendorName = chargepoint.ChargepointModel.Vendor.Name,
                    ModelName = chargepoint.ChargepointModel.ModelName,
                    Connectors = chargepoint.EVSEs.Count,
                    InstallName = chargepoint.Group.Install.InstallName,
                    TenantId = chargepoint.TenantId,
                    CreationTime = chargepoint.CreationTime,
                    MountTypeName = chargepoint.ChargepointModel.MountType.Name,
                    Identity = chargepoint.Identity,
                    EVSEsCount = chargepoint.EVSEs.Count(e => !e.IsDeleted),
                    Status = chargepoint.AdminStatus.Status
                }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.Place.Contains(input.Filter) || item.ModelName.Contains(input.Filter) || item.Comment.Contains(input.Filter))
                .WhereIf(!input.Place.IsNullOrWhiteSpace(), item => item.Place == input.Place)
                .WhereIf(!input.Comment.IsNullOrWhiteSpace(), item => item.Comment == input.Comment)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<ChargepointDto>(resultCount, results.ToList());
        }
        public async Task<GetChargepointForEditOutput> GetChargepointByIdForManageChargepoint(EntityDto<int> input)
        {
            if (input.Id == 0) return null;
            var chargepoints = from chargepoint in _chargepointRepository.GetAll().Include(c => c.OCPPVersion).Include(c => c.OCPPTransport).Include(c => c.Group).Include(c => c.Group.Install).Include(c => c.ChargepointModel)
                               .Include(c => c.ChargepointModel.Vendor).Include(c => c.ChargepointModel.MountType).Include(c => c.AdminStatus).Include(c => c.EVSEs).Include(c => c.AdminStatus)
                               .Where(c => c.Id == input.Id)
                               select new ChargepointDto
                               {
                                   Id = chargepoint.Id,
                                   Place = chargepoint.Place,
                                   Comment = chargepoint.Comment,
                                   AdminStatusId = chargepoint.AdminStatusId,
                                   ChargepointModelId = chargepoint.ChargepointModelId,
                                   GroupId = chargepoint.GroupId,
                                   GroupName = chargepoint.Group.GroupName,
                                   VendorName = chargepoint.ChargepointModel.Vendor.Name,
                                   ModelName = chargepoint.ChargepointModel.ModelName,
                                   OCPPVersionTransportName = chargepoint.OCPPVersion.VersionName + " " + chargepoint.OCPPTransport.OCPPTransportName,
                                   AdminStatus = chargepoint.AdminStatus.Status,
                                   TenantId = chargepoint.TenantId,
                                   CreationTime = chargepoint.CreationTime,
                                   Identity = chargepoint.Identity,
                                   Status = chargepoint.AdminStatus.Status
                               };
            var output = new GetChargepointForEditOutput();
            output.Chargepoint = chargepoints.FirstOrDefault();
            output.Chargepoint.EVSEs = (await _iEVSEAppService.GetEVSEByChargepointId(input)).Items.ToList();
            output.Chargepoint.ResetEvents = _resetEventAppService.GetResetEventsListByChargepoint(input).Items.ToList();
            output.Chargepoint.RemoteStartStopEvents = _remoteStartStopEventAppService.GetRemoteStartStopEventsListByChargepoint(input).Items.ToList();
            output.Chargepoint.UnlockEvents = _unlockEventAppService.GetUnlockEventsListByChargepoint(input).Items.ToList();
            output.Chargepoint.ClearCacheEvents = _clearCacheEventAppService.GetClearCacheEventsListByChargepoint(input).Items.ToList();
            return output;
        }
        public async Task CreateResetStatusAndEvent(CreateResetStatusAndEventInput input)
        {
            var chargepoint = _chargepointRepository.Get(input.ChargepointId);
            await _chargepointRepository.UpdateAsync(chargepoint);

            CreateOrUpdateResetEventInput resetEvent = new CreateOrUpdateResetEventInput();
            resetEvent.ChargepointId = input.ChargepointId;
            resetEvent.ResetTypeId = input.ResetTypeId;
            resetEvent.Date = DateTime.Now;
            resetEvent.ResetStatusId = _resetStatusAppService.GetResetStatus("Initiated");
            int eventId = await _resetEventAppService.CreateOrUpdateResetEvent(resetEvent);
            var metadata = new Dictionary<string, string>();
            metadata.Add(APIMetadata.EventId, eventId.ToString());
            await _deviceManager.SendMessageToDeviceAsync(chargepoint.Identity, chargepoint.TenantId, new ResetRequest() { Type = input.ResetType == "Hard" ? ResetTypeEnum.Hard : ResetTypeEnum.Soft }, metadata);
        }
        public async Task CreateRemoteStartTransaction(CreateRemoteStartTransactionInput input)
        {
            //RemoteStartTransaction to CP
            CreateOrUpdateRemoteStartStopEventInput remoteEvent = new CreateOrUpdateRemoteStartStopEventInput();
            remoteEvent.EVSEId = input.EVSEId;
            remoteEvent.RemoteStartStopStatusId = _remoteStartStopStatusRepository.GetAll().Where(t => t.Value.Contains("Initiated")).FirstOrDefault().Id;
            remoteEvent.RemoteStartStopEventTypeId = _remoteStartStopEventTypeRepository.GetAll().Where(t => t.EventType.Contains("Start")).FirstOrDefault().Id;
            int eventId = await _remoteStartStopEventAppService.CreateOrUpdateRemoteStartStopEvent(remoteEvent);

            var metadata = new Dictionary<string, string>();
            metadata.Add(APIMetadata.EventId, eventId.ToString());

            var chargepoint = _chargepointRepository.Get(input.ChargepointId);

            await _deviceManager.SendMessageToDeviceAsync(chargepoint.Identity, chargepoint.TenantId, new RemoteStartTransactionRequest { ConnectorId = input.EVSEId, IdTag = input.TagId.ToString() }, metadata);
        }
        public async Task CreateRemoteStopTransaction(CreateRemoteStopTransactionInput input)
        {
            //RemoteStopTransaction to CP
            CreateOrUpdateRemoteStartStopEventInput remoteEvent = new CreateOrUpdateRemoteStartStopEventInput();
            remoteEvent.EVSEId = input.EVSEId;
            remoteEvent.RemoteStartStopStatusId = _remoteStartStopStatusRepository.GetAll().Where(t => t.Value.Contains("Initiated")).FirstOrDefault().Id;
            remoteEvent.RemoteStartStopEventTypeId = _remoteStartStopEventTypeRepository.GetAll().Where(t => t.EventType.Contains("Stop")).FirstOrDefault().Id;
            int eventId = await _remoteStartStopEventAppService.CreateOrUpdateRemoteStartStopEvent(remoteEvent);
            var metadata = new Dictionary<string, string>();
            metadata.Add(APIMetadata.EventId, eventId.ToString());
            var chargepoint = _chargepointRepository.Get(input.ChargepointId);

            await _deviceManager.SendMessageToDeviceAsync(chargepoint.Identity, chargepoint.TenantId, new RemoteStopTransactionRequest() { TransactionId = input.EVSEId }, metadata);
        }
        public async Task RefreshChargepointKeyValuesFromCP(EntityDto<int> input)
        {
            var chargepoint = _chargepointRepository.Get(input.Id);

            CreateOrUpdateConfigEventInput configEvent = new CreateOrUpdateConfigEventInput();
            configEvent.ChargepointId = input.Id;
            configEvent.ConfigStatusId = _configStatusAppService.GetConfigStatus("Initiated");
            configEvent.ConfigTypeId = _configTypeAppService.GetConfigType("All");
            int eventId = await _configEventAppService.CreateOrUpdateConfigEvent(configEvent);
            var metadata = new Dictionary<string, string>();
            metadata.Add(APIMetadata.EventId, eventId.ToString());

            await _deviceManager.SendMessageToDeviceAsync(chargepoint.Identity, chargepoint.TenantId, new GetConfigurationRequest(), metadata);
        }
        public async Task SetConfigurationAllCPKeyValues(EntityDto<int> input)
        {
            var chargepoint = _chargepointRepository.Get(input.Id);

            CreateOrUpdateConfigEventInput configEvent = new CreateOrUpdateConfigEventInput();
            configEvent.ChargepointId = input.Id;
            configEvent.ConfigStatusId = _configStatusAppService.GetConfigStatus("Initiated");
            configEvent.ConfigTypeId = _configTypeAppService.GetConfigType("All");
            int eventId = await _configEventAppService.CreateOrUpdateConfigEvent(configEvent);
            var metadata = new Dictionary<string, string>();
            metadata.Add(APIMetadata.EventId, eventId.ToString());

            await _deviceManager.SendMessageToDeviceAsync(chargepoint.Identity, chargepoint.TenantId, new ChangeConfigurationRequest() { Key = input.Id.ToString(), Value = "All" }, metadata);
        }
        public async Task GetChargepointKeyValueFromCP(ChargepointKeyValueConfigurationInut input)
        {
            var chargepoint = _chargepointRepository.Get(input.ChargepointId);

            ObservableCollection<string> _key = new ObservableCollection<string>
            {
                input.ChargepointKeyValueId.ToString()
            };

            CreateOrUpdateConfigEventInput configEvent = new CreateOrUpdateConfigEventInput();
            configEvent.ChargepointId = input.ChargepointId;
            configEvent.ConfigStatusId = _configStatusAppService.GetConfigStatus("Initiated");
            configEvent.ConfigTypeId = _configTypeAppService.GetConfigType("Single");
            int eventId = await _configEventAppService.CreateOrUpdateConfigEvent(configEvent);
            var metadata = new Dictionary<string, string>();
            metadata.Add(APIMetadata.EventId, eventId.ToString());

            await _deviceManager.SendMessageToDeviceAsync(chargepoint.Identity, chargepoint.TenantId, new GetConfigurationRequest() { Key = _key }, metadata);
        }
        public async Task SetChargepointKeyValueInCP(ChargepointKeyValueConfigurationInut input)
        {
            var chargepoint = _chargepointRepository.Get(input.ChargepointId);

            CreateOrUpdateConfigEventInput configEvent = new CreateOrUpdateConfigEventInput();
            configEvent.ChargepointId = input.ChargepointId;
            configEvent.ConfigStatusId = _configStatusAppService.GetConfigStatus("Initiated");
            configEvent.ConfigTypeId = _configTypeAppService.GetConfigType("All");
            int eventId = await _configEventAppService.CreateOrUpdateConfigEvent(configEvent);
            var metadata = new Dictionary<string, string>();
            metadata.Add(APIMetadata.EventId, eventId.ToString());

            await _deviceManager.SendMessageToDeviceAsync(chargepoint.Identity, chargepoint.TenantId, new ChangeConfigurationRequest(), metadata);
        }
        public async Task UpdateAvailability(UpdateAvailabilityInput input)
        {
            var chargepoint = _chargepointRepository.Get(input.ChargepointId);

            CreateOrUpdateAvailabilityEventInput availabilityEventInput = new CreateOrUpdateAvailabilityEventInput();
            availabilityEventInput.AvailabilityTypeId = input.AvailabilityTypeId;
            availabilityEventInput.Date = DateTime.Now;
            availabilityEventInput.EVSEId = input.EVSEId;
            availabilityEventInput.AvailabilityStatusId = _availabilityStatusAppService.GetAvailabilityStatus("Initiated");
            int eventId = await _availabilityEventAppService.CreateOrUpdateAvailabilityEvent(availabilityEventInput);
            var metadata = new Dictionary<string, string>();
            metadata.Add(APIMetadata.EVSEId, input.EVSEId.ToString());
            metadata.Add(APIMetadata.EventId, eventId.ToString());
            await _deviceManager.SendMessageToDeviceAsync(chargepoint.Identity, chargepoint.TenantId, new ChangeAvailabilityRequest() { ConnectorId = input.EVSEId, Type = input.Availability == "Operative" ? ChangeAvailabilityTypeEnum.Operative : ChangeAvailabilityTypeEnum.Inoperative }, metadata);
        }
        public async Task UnlockEVSE(UnlockEVSEInput input)
        {
            var chargepoint = _chargepointRepository.Get(input.ChargepointId);
            CreateOrUpdateUnlockEventInput unlockEvent = new CreateOrUpdateUnlockEventInput();
            unlockEvent.EVSEId = input.EVSEId;
            unlockEvent.UnlockStatusId = _unlockStatusAppService.GetUnlockStatus("Initiated");

            int eventId = await _unlockEventAppService.CreateOrUpdateUnlockEvent(unlockEvent);

            var metadata = new Dictionary<string, string>();
            metadata.Add(APIMetadata.EventId, eventId.ToString());
            metadata.Add(APIMetadata.EVSEId, input.EVSEId.ToString());

            await _deviceManager.SendMessageToDeviceAsync(chargepoint.Identity, chargepoint.TenantId, new UnlockConnectorRequest() { ConnectorId = input.EVSE_Id }, metadata);
        }
        public async Task ClearCacheChargepoint(ClearCacheChargepointInput input)
        {
            var chargepoint = _chargepointRepository.Get(input.ChargepointId);
            CreateOrUpdateClearCacheEventInput clearCacheEvent = new CreateOrUpdateClearCacheEventInput();
            clearCacheEvent.ChargepointId = input.ChargepointId;
            clearCacheEvent.ClearCacheStatusId = _clearCacheStatusAppService.GetClearCacheStatus("Initiated");
            clearCacheEvent.Date = DateTime.Now;
            int eventId = await _clearCacheEventAppService.CreateOrUpdateClearCacheEvent(clearCacheEvent);

            var metadata = new Dictionary<string, string>();
            metadata.Add(APIMetadata.EventId, eventId.ToString());

            await _deviceManager.SendMessageToDeviceAsync(chargepoint.Identity, chargepoint.TenantId, new ClearCacheRequest(), metadata);
        }
        public async Task DeleteChargepoint(EntityDto<int> input)
        {
            var chargepoint = await _chargepointRepository.GetAsync(input.Id);
            await _chargepointRepository.DeleteAsync(chargepoint);
        }
    }
}
