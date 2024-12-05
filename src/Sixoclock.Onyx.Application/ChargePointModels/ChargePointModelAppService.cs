using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.ChargePointModels.Dto;
using Sixoclock.Onyx.KeyValues;
using Sixoclock.Onyx.KeyValues.Dto;
using Sixoclock.Onyx.ModelConnectors;
using Sixoclock.Onyx.ModelFeatures;
using Sixoclock.Onyx.ModelFeatures.Dto;
using Sixoclock.Onyx.ModelKeyValues;
using Sixoclock.Onyx.ModelKeyValues.Dto;
using Sixoclock.Onyx.OCPPFeatures;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.ChargePointModels
{
    public class ChargePointModelAppService : OnyxAppServiceBase, IChargePointModelAppService
    {
        private readonly IRepository<ChargepointModel> _chargepointModelRepository;
        private readonly IRepository<ChargeReleaseOption> _chargeReleaseOptionRepository;
        private readonly IRepository<ReleaseOptionModel> _releaseOptionModelRepository;
        private readonly IRepository<ElectricalOption> _electricOptionRepository;
        private readonly IRepository<ElectricalOptionModel> _electricalOptionModelRepository;
        private readonly IRepository<ComOption> _comOptionRepository;
        private readonly IRepository<ComOptionModel> _comOptionModelRepository;
        private readonly IRepository<OtherOption> _otherOptionRepository;
        private readonly IRepository<OtherOptionModel> _otherOptionModelRepository;
        private readonly IModelFeatureAppService _modelFeatureAppService;
        private readonly IModelKeyValueAppService _modelKeyValueAppService;
        private readonly IKeyValueAppService _keyValueAppService;
        private readonly IOCPPFeatureAppService _oCPPFeatureAppService;
        private readonly IRepository<ModelConnector> _modelConnectorRepository;
        private readonly IRepository<ModelEVSE> _modelEvseRepository;
        private readonly IRepository<Capacity> _capacityRepository;
        private readonly IRepository<ConnectorType> _connectorTypeRepository;
        private readonly IModelConnectorAppService _connectorAppService;

        public ChargePointModelAppService(IRepository<ChargepointModel> chargepointModelRepository, IRepository<ChargeReleaseOption> chargeReleaseOptionRepository,
            IRepository<ElectricalOption> electricOptionRepository, IRepository<ComOption> comOptionRepository, IRepository<OtherOption> otherOptionRepository,
            IRepository<ReleaseOptionModel> releaseOptionModelRepository, IRepository<ElectricalOptionModel> electricalOptionModelRepository,
            IRepository<ComOptionModel> comOptionModelRepository, IRepository<OtherOptionModel> otherOptionModelRepository,IModelFeatureAppService modelFeatureAppService,
            IModelKeyValueAppService modelKeyValueAppService,IKeyValueAppService keyValueAppService,IOCPPFeatureAppService oCPPFeatureAppService,IRepository<ModelConnector> modelConnectorRepository,
            IRepository<ModelEVSE> modelEvseRepository,IRepository<Capacity> capacityRepository,IRepository<ConnectorType> connectorTypeRepository,ModelConnectorAppService connectorAppService)
        {
            _chargepointModelRepository = chargepointModelRepository;
            _chargeReleaseOptionRepository = chargeReleaseOptionRepository;
            _releaseOptionModelRepository = releaseOptionModelRepository;
            _electricOptionRepository = electricOptionRepository;
            _electricalOptionModelRepository = electricalOptionModelRepository;
            _comOptionRepository = comOptionRepository;
            _comOptionModelRepository = comOptionModelRepository;
            _otherOptionRepository = otherOptionRepository;
            _otherOptionModelRepository = otherOptionModelRepository;
            _modelFeatureAppService = modelFeatureAppService;
            _modelKeyValueAppService = modelKeyValueAppService;
            _keyValueAppService = keyValueAppService;
            _oCPPFeatureAppService = oCPPFeatureAppService;
            _modelConnectorRepository = modelConnectorRepository;
            _modelEvseRepository = modelEvseRepository;
            _capacityRepository = capacityRepository;
            _connectorTypeRepository = connectorTypeRepository;
            _connectorAppService = connectorAppService;
        }

        public async Task<int> CreateOrUpdateChargepointModel(CreateOrUpdateChargepointModelInput input)
        {
            var chargepointModel = ObjectMapper.Map<ChargepointModel>(input);
            var tenantId = GetCurrentTenant().Id;
            if (input.Id == 0)
            {
                int chargepointModelId = await _chargepointModelRepository.InsertAndGetIdAsync(chargepointModel);
                await CurrentUnitOfWork.SaveChangesAsync();

                foreach (ModelFeature feature in input.ModelFeatures)
                {
                    GetKeyValuesListByOCPPFeatureIdListInput keyValueInput = new GetKeyValuesListByOCPPFeatureIdListInput();
                    keyValueInput.TenantId = tenantId;
                    keyValueInput.OCPPFeatureId = feature.OCPPFeatureId;
                    var keyValues = _keyValueAppService.GetKeyValuesListByOCPPFeatureId(keyValueInput);

                    CreateOrUpdateModelKeyValueInput modelKeyValue;
                    foreach (KeyValueByOCPPFeatureIdListDto keyValue in keyValues.KeyValues)
                    {
                        modelKeyValue = new CreateOrUpdateModelKeyValueInput();
                        modelKeyValue.ModelKeyValue = new ModelKeyValue();
                        modelKeyValue.ModelKeyValue.KeyValueId = keyValue.Id;
                        modelKeyValue.ChargepointModelId = chargepointModelId;
                        modelKeyValue.ModelKeyValue.Comment = keyValue.Comment;
                        modelKeyValue.ModelKeyValue.ModelValue = keyValue.DefaultValue;
                        modelKeyValue.ModelKeyValue.Key = keyValue.Key;
                        modelKeyValue.ModelKeyValue.FeatureName = keyValue.FeatureName;
                        modelKeyValue.ModelKeyValue.RW = keyValue.RW;
                        modelKeyValue.ModelKeyValue.TenantId = tenantId;

                        await _modelKeyValueAppService.CreateOrUpdateModelKeyValue(modelKeyValue);
                    }
                }
                int evseId = _modelEvseRepository.GetAll().Where(e => e.ChargepointModelId == chargepointModelId).Count();
                ModelEVSE modelEvse;
                if (evseId == 0)
                {
                    modelEvse = new ModelEVSE();
                    modelEvse.ChargepointModelId = chargepointModelId;
                    modelEvse.EVSEId = evseId;
                    modelEvse.IsDeleted = true;
                    modelEvse.TenantId = tenantId;
                    await _modelEvseRepository.InsertAsync(modelEvse);
                }
                modelEvse = new ModelEVSE();
                modelEvse.EVSEId = evseId + 1;
                modelEvse.ChargepointModelId = chargepointModelId;
                
                
                modelEvse.TenantId = tenantId;
                int modelEVSEId = await _modelEvseRepository.InsertAndGetIdAsync(modelEvse);

                int capacityId = _capacityRepository.GetAll().Where(c => c.TenantId == tenantId && c.Value.Contains("3.7")).FirstOrDefault().Id;
                int connectorTypeId = _connectorTypeRepository.GetAll().Where(c => c.TenantId == tenantId && c.ConnectorName.Contains("Unknown")).FirstOrDefault().Id;
                int connectorId = _modelConnectorRepository.GetAll().Where(c => c.ModelEVSE.ChargepointModelId == chargepointModelId).Count() + 1;
                ModelConnector modelConnector = new ModelConnector();
                modelConnector.CapacityId = capacityId;
                modelConnector.ConnectorId = connectorId;
                modelConnector.ConnectorTypeId = connectorTypeId;
                modelConnector.ModelEVSEId = modelEVSEId;
                modelConnector.TenantId = tenantId;
                await _modelConnectorRepository.InsertAsync(modelConnector);
                return chargepointModelId;
            }
            else
            {
                await _chargepointModelRepository.UpdateAsync(chargepointModel);
                await DeleteModelFeaturesAndKeyValues(input.Id, tenantId);

                CreateOrUpdateModelFeatureInput feature;
                foreach (ModelFeature ocppFeature in input.ModelFeatures)
                {
                    feature = new CreateOrUpdateModelFeatureInput();
                    feature.ModelFeature = ocppFeature;
                    feature.ModelFeature.ChargepointModelId = input.Id;
                    feature.ChargepointModelId = input.Id;
                    await _modelFeatureAppService.CreateOrUpdateModelFeature(feature);

                    GetKeyValuesListByOCPPFeatureIdListInput keyValueInput = new GetKeyValuesListByOCPPFeatureIdListInput();
                    keyValueInput.TenantId = tenantId;
                    keyValueInput.OCPPFeatureId = ocppFeature.OCPPFeatureId;
                    var keyValues = _keyValueAppService.GetKeyValuesListByOCPPFeatureId(keyValueInput);

                    CreateOrUpdateModelKeyValueInput modelKeyValue;
                    foreach (KeyValueByOCPPFeatureIdListDto keyValue in keyValues.KeyValues)
                    {
                        modelKeyValue = new CreateOrUpdateModelKeyValueInput();
                        modelKeyValue.ModelKeyValue = new ModelKeyValue();
                        modelKeyValue.ModelKeyValue.KeyValueId = keyValue.Id;
                        modelKeyValue.ModelKeyValue.Key = keyValue.Key;
                        modelKeyValue.ModelKeyValue.FeatureName = keyValue.FeatureName;
                        modelKeyValue.ChargepointModelId = input.Id;
                        modelKeyValue.ModelKeyValue.Comment = keyValue.Comment;
                        modelKeyValue.ModelKeyValue.ModelValue = keyValue.DefaultValue;
                        modelKeyValue.ModelKeyValue.RW = keyValue.RW;
                        modelKeyValue.ModelKeyValue.TenantId = tenantId;

                        await _modelKeyValueAppService.CreateOrUpdateModelKeyValue(modelKeyValue);
                    }
                }
                return chargepointModel.Id;
            }
        }

        private async Task DeleteModelFeaturesAndKeyValues(int chargepointModelId, int tenantId)
        {
            var modelFeatures = _modelFeatureAppService.GetModelFeaturesList(chargepointModelId);
            foreach (var modelFeature in modelFeatures.ModelFeatures)
            {
                await _modelFeatureAppService.DeleteModelFeature(modelFeature);

                GetModelKeyValuesListByChargepointModelIdListInput modelKeyValueInput = new GetModelKeyValuesListByChargepointModelIdListInput();
                modelKeyValueInput.TenantId = tenantId;
                modelKeyValueInput.ChargepointModelId = chargepointModelId;
                var keyValues = _modelKeyValueAppService.GetModelKeyValuesListByChargepointModelId(modelKeyValueInput);

                foreach (ModelKeyValueByChargepointModelIdListDto modelKeyValue in keyValues.ModelKeyValues)
                {
                    await _modelKeyValueAppService.DeleteModelKeyValue(modelKeyValue);
                }
            }
        }

        public async Task CreateOrUpdateChargepointModelOptions(CreateOrUpdateChargepointModelOptionsInput input)
        {
            int tenantId = GetCurrentTenant().Id;

            var releasesOptionModels = _releaseOptionModelRepository.GetAll().Where(r => r.TenantId == tenantId);
            foreach (var release in releasesOptionModels)
            {
                await _releaseOptionModelRepository.DeleteAsync(release);
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            var electricOptionModels = _electricalOptionModelRepository.GetAll().Where(r => r.TenantId == tenantId);
            foreach (var electric in electricOptionModels)
            {
                await _electricalOptionModelRepository.DeleteAsync(electric);
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            var comOptionModels = _comOptionModelRepository.GetAll().Where(r => r.TenantId == tenantId);
            foreach (var com in comOptionModels)
            {
                await _comOptionModelRepository.DeleteAsync(com);
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            var otherOptionModels = _otherOptionModelRepository.GetAll().Where(r => r.TenantId == tenantId);
            foreach (var other in otherOptionModels)
            {
                await _otherOptionModelRepository.DeleteAsync(other);
                await CurrentUnitOfWork.SaveChangesAsync();
            }

            CurrentUnitOfWork.DisableFilter(AbpDataFilters.SoftDelete);
            foreach (ReleaseOptionModel releaseOptionModel in input.ReleaseOptionModels)
            {
                releaseOptionModel.TenantId = tenantId;
                var option = _releaseOptionModelRepository.GetAll().Where(r => r.TenantId == tenantId && r.ChargeReleaseOptionId == releaseOptionModel.ChargeReleaseOptionId && r.ChargepointModelId == releaseOptionModel.ChargepointModelId).FirstOrDefault();
                if (option == null)
                {
                    await _releaseOptionModelRepository.InsertAsync(releaseOptionModel);
                }
                else
                {
                    option.IsDeleted = false;
                    option.DeleterUserId = null;
                    option.DeletionTime = null;
                    option.LastModificationTime = null;
                    await _releaseOptionModelRepository.UpdateAsync(option);
                }

                await CurrentUnitOfWork.SaveChangesAsync();
            }
            foreach (ElectricalOptionModel electricOptionModel in input.ElectricalOptionModels)
            {
                electricOptionModel.TenantId = tenantId;
                var option = _electricalOptionModelRepository.GetAll().Where(r => r.TenantId == tenantId && r.ElectricalOptionId == electricOptionModel.ElectricalOptionId && r.ChargepointModelId == electricOptionModel.ChargepointModelId).FirstOrDefault();
                if (option == null)
                {
                    await _electricalOptionModelRepository.InsertAsync(electricOptionModel);
                }
                else
                {
                    option.IsDeleted = false;
                    option.DeleterUserId = null;
                    option.DeletionTime = null;
                    option.LastModificationTime = null;
                    await _electricalOptionModelRepository.UpdateAsync(option);
                }

                await CurrentUnitOfWork.SaveChangesAsync();
            }
            foreach (ComOptionModel comOptionModel in input.ComOptionModels)
            {
                comOptionModel.TenantId = tenantId;
                var option = _comOptionModelRepository.GetAll().Where(r => r.TenantId == tenantId && r.ComOptionId == comOptionModel.ComOptionId && r.ChargepointModelId == comOptionModel.ChargepointModelId).FirstOrDefault();
                if (option == null)
                {
                    await _comOptionModelRepository.InsertAsync(comOptionModel);
                }
                else
                {
                    option.IsDeleted = false;
                    option.DeleterUserId = null;
                    option.DeletionTime = null;
                    option.LastModificationTime = null;
                    await _comOptionModelRepository.UpdateAsync(option);
                }

                await CurrentUnitOfWork.SaveChangesAsync();
            }
            foreach (OtherOptionModel otherOptionModel in input.OtherOptionModels)
            {
                otherOptionModel.TenantId = tenantId;
                var option = _otherOptionModelRepository.GetAll().Where(r => r.TenantId == tenantId && r.OtherOptionId == otherOptionModel.OtherOptionId && r.ChargepointModelId == otherOptionModel.ChargepointModelId).FirstOrDefault();
                if (option == null)
                {
                    await _otherOptionModelRepository.InsertAsync(otherOptionModel);
                }
                else
                {
                    option.IsDeleted = false;
                    option.DeleterUserId = null;
                    option.DeletionTime = null;
                    option.LastModificationTime = null;
                    await _otherOptionModelRepository.UpdateAsync(option);
                }

                await CurrentUnitOfWork.SaveChangesAsync();
            }
        }

        public async Task<GetChargepointModelForEditOutput> GetChargepointModelForEdit(EntityDto<int> input)
        {
            //Editing an existing charge point model
            var output = new GetChargepointModelForEditOutput();
            if (input.Id == 0)
            {
                output.ChargepointModel = new ChargepointModelDto();

                output.ChargepointModel.Releases = (from release in _chargeReleaseOptionRepository.GetAll()
                                                    select new ChargeReleaseOptionDto
                                                    {
                                                        Id = release.Id,
                                                        Option = release.Option,
                                                        Comment = release.Comment,
                                                        TenantId = release.TenantId,
                                                        Assigned = false
                                                    }).ToList();

                output.ChargepointModel.Electrics = (from electric in _electricOptionRepository.GetAll()
                                                     select new ElectricalOptionDto
                                                     {
                                                         Id = electric.Id,
                                                         Option = electric.Option,
                                                         TenantId = electric.TenantId,
                                                         Assigned = false
                                                     }).ToList();

                output.ChargepointModel.Coms = (from com in _comOptionRepository.GetAll()
                                                select new ComOptionDto
                                                {
                                                    Id = com.Id,
                                                    Option = com.Option,
                                                    TenantId = com.TenantId,
                                                    Assigned = false
                                                }).ToList();

                output.ChargepointModel.Others = (from other in _otherOptionRepository.GetAll()
                                                  select new OtherOptionDto
                                                  {
                                                      Id = other.Id,
                                                      Option = other.Option,
                                                      TenantId = other.TenantId,
                                                      Assigned = false
                                                  }).ToList();

                output.ChargepointModel.OCPPFeatures = _oCPPFeatureAppService.GetOCPPFeaturesList();
            }
            else
            {
                var chargepointModel = await _chargepointModelRepository.GetAsync(input.Id);

                output.ChargepointModel = ObjectMapper.Map<ChargepointModelDto>(chargepointModel);

                var releaseOptions = (from option in _chargeReleaseOptionRepository.GetAll()
                                     .Include(c => c.ReleaseOptionModels)
                                      select new ChargeReleaseOptionDto
                                      {
                                          Id = option.Id,
                                          Option = option.Option,
                                          Comment = option.Comment,
                                          TenantId = option.TenantId,
                                          ReleaseOptionModels = option.ReleaseOptionModels.ToList()
                                      }).ToList();

                output.ChargepointModel.Releases = releaseOptions;

                for (int i = 0; i < output.ChargepointModel.Releases.Count; i++)
                {
                    if (output.ChargepointModel.Releases[i].ReleaseOptionModels.Any(c => c.ChargepointModelId == input.Id && c.IsDeleted == false))
                        output.ChargepointModel.Releases[i].Assigned = true;
                }

                var electricOptions = (from option in _electricOptionRepository.GetAll()
                                     .Include(c => c.ElectricalOptionModels)
                                       select new ElectricalOptionDto
                                       {
                                           Id = option.Id,
                                           Option = option.Option,
                                           TenantId = option.TenantId,
                                           Electrics = option.ElectricalOptionModels.ToList()
                                       });

                output.ChargepointModel.Electrics = electricOptions.ToList();
                foreach (var electric in output.ChargepointModel.Electrics)
                {
                    if (electric.Electrics.Any(c => c.ChargepointModelId == input.Id && c.IsDeleted == false))
                        electric.Assigned = true;
                }

                var comOptions = (from com in _comOptionRepository.GetAll()
                                     .Include(c => c.ComOptionModels)
                                  select new ComOptionDto
                                  {
                                      Id = com.Id,
                                      Option = com.Option,
                                      TenantId = com.TenantId,
                                      Coms = com.ComOptionModels.ToList()
                                  });

                output.ChargepointModel.Coms = comOptions.ToList();

                foreach (var com in output.ChargepointModel.Coms)
                {
                    if (com.Coms.Any(c => c.ChargepointModelId == input.Id && c.IsDeleted == false))
                        com.Assigned = true;
                }

                var otherOptions = (from other in _otherOptionRepository.GetAll()
                                     .Include(c => c.OtherOptionModels)
                                    select new OtherOptionDto
                                    {
                                        Id = other.Id,
                                        Option = other.Option,
                                        TenantId = other.TenantId,
                                        Others = other.OtherOptionModels.ToList()
                                    });

                output.ChargepointModel.Others = otherOptions.ToList();

                foreach (var other in output.ChargepointModel.Others)
                {
                    if (other.Others.Any(c => c.ChargepointModelId == input.Id && c.IsDeleted == false))
                        other.Assigned = true;
                }

                EntityDto<int> version = new EntityDto<int>();
                version.Id = output.ChargepointModel.OCPPVersionId;

                output.ChargepointModel.OCPPFeatures = _oCPPFeatureAppService.GetOCPPFeaturesByOCPPVersionList(version);

                var modelFeatures = _modelFeatureAppService.GetModelFeaturesList(input.Id);
                foreach (var ocppFeature in output.ChargepointModel.OCPPFeatures.OCPPFeatures)
                {
                    if (modelFeatures.ModelFeatures.Any(m => m.OCPPFeatureId == ocppFeature.Id))
                    {
                        ocppFeature.Assigned = true;
                    }
                }
            }

            return output;
        }

        public GetChargepointModelsListOutput GetChargepointModelsList()
        {
            IEnumerable<ChargepointModelListDto> _chargepointModelsList = from chargepointModel in _chargepointModelRepository.GetAll()
                                                                          select new ChargepointModelListDto
                                                                          {
                                                                              Id = chargepointModel.Id,
                                                                              Name = chargepointModel.ModelName
                                                                          };
            return new GetChargepointModelsListOutput { ChargepointModels = _chargepointModelsList.ToList() };
        }
        
        public GetChargepointModelsListOutput GetChargepointModelsByVendorList(EntityDto<int> input)
        {
            IEnumerable<ChargepointModelListDto> _chargepointModelsList = from chargepointModel in _chargepointModelRepository.GetAll().Where(c => c.VendorId == input.Id)
                                                                          select new ChargepointModelListDto
                                                                          {
                                                                              Id = chargepointModel.Id,
                                                                              Name = chargepointModel.ModelName
                                                                          };
            return new GetChargepointModelsListOutput { ChargepointModels = _chargepointModelsList.ToList() };
        }
        public GetChargepointModelsListOutput GetChargepointModelsByMountList(EntityDto<int> input)
        {
            IEnumerable<ChargepointModelListDto> _chargepointModelsList = from chargepointModel in _chargepointModelRepository.GetAll().Where(c => c.MountTypeId == input.Id)
                                                                          select new ChargepointModelListDto
                                                                          {
                                                                              Id = chargepointModel.Id,
                                                                              Name = chargepointModel.ModelName
                                                                          };
            return new GetChargepointModelsListOutput { ChargepointModels = _chargepointModelsList.ToList() };
        }

        public async Task<PagedResultDto<ChargepointModelDto>> GetChargepointModel(GetChargepointModelInput input)
        {
            var query = (from chargepointModel in _chargepointModelRepository.GetAll().Include(c => c.Vendor).Include(c => c.MountType)
                         .Include(c => c.OCPPVersion).Include(c => c.OCPPTransport).Include(c => c.ModelEVSEs)
                         select new ChargepointModelDto
                         {
                             Id = chargepointModel.Id,
                             ModelName = chargepointModel.ModelName,
                             Comment = chargepointModel.Comment,
                             MountTypeId = chargepointModel.MountTypeId,
                             VendorId = chargepointModel.VendorId,
                             TenantId = chargepointModel.TenantId,
                             VendorName = chargepointModel.Vendor.Name,
                             MountName = chargepointModel.MountType.Name,
                             VersionName = chargepointModel.OCPPVersion.VersionName,
                             OCPPTransportName = chargepointModel.OCPPTransport.OCPPTransportName,
                             ModelEvseCount = chargepointModel.ModelEVSEs.Count,
                             CreationTime = chargepointModel.CreationTime
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.ModelName.Contains(input.Filter) || item.Comment.Contains(input.Filter))
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.ModelName == input.ModelName)
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.Comment == input.Comment)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            for (int i = 0; i < results.Count(); i++)
            {
                var modelEVseIds = _modelEvseRepository.GetAll().Where(m => m.ChargepointModelId == results[i].Id);
                foreach (var evseId in modelEVseIds)
                {
                    results[i].ModelConnectorsCount += _modelConnectorRepository.GetAll().Where(m => m.ModelEVSEId == evseId.Id).Count();
                }
            }

            return new PagedResultDto<ChargepointModelDto>(resultCount, results.ToList());
        }
        public GetChargepointModelForChargepointOutput GetChargepointModelForChargepoint(EntityDto<int> input)
        {
            var chargePointModel = (from m in _chargepointModelRepository.GetAll().Include(c => c.Vendor).Where(c=>c.Id == input.Id)
                                    select new GetChargepointModelForChargepointOutput
                                    {
                                        Vendor = m.Vendor.Name,
                                        OCPPVersionId = m.OCPPVersionId,
                                        OCPPTransportId = m.OCPPTransportId
                                    }).FirstOrDefault();
            var model = chargePointModel;

            if (model != null)
            {
                model.Capacity = _connectorAppService.GetCapacitiesByModelId(input);
                model.NoOfConnectors = _modelConnectorRepository.GetAll().Where(c => c.ModelEVSE.ChargepointModelId == input.Id).Count();
            }

            return model;
        }
        public async Task DeleteChargepointModel(EntityDto<int> input)
        {
            var chargepointModel = await _chargepointModelRepository.GetAsync(input.Id);
            await _chargepointModelRepository.DeleteAsync(chargepointModel);
        }
    }
}
