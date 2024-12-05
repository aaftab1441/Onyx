using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.ModelConnectors.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.ModelConnectors
{
    public class ModelConnectorAppService : OnyxAppServiceBase, IModelConnectorAppService
    {
        private readonly IRepository<ModelConnector> _modelConnectorRepository;

        public ModelConnectorAppService(IRepository<ModelConnector> modelConnectorRepository)
        {
            _modelConnectorRepository = modelConnectorRepository;
        }
        public async Task CreateOrUpdateModelConnector(CreateOrUpdateModelConnectorInput input)
        {
            var modelConnector = ObjectMapper.Map<ModelConnector>(input);
            var tenantId = GetCurrentTenant().Id;

            if (input.Id == 0)
            {
                int modelConnectorId = await _modelConnectorRepository.InsertAndGetIdAsync(modelConnector);
            }
            else
            {
                var model = new ModelConnector();
                model.CapacityId = input.CapacityId;
                model.ModelEVSEId = input.ModelEVSEId;
                model.CapacityId = input.CapacityId;
                model.Comment = input.Comment;
                model.ConnectorTypeId = input.ConnectorTypeId;
                model.ConnectorId = input.ConnectorId;
                model.Id = input.Id;
                model.TenantId = input.TenantId;

                await _modelConnectorRepository.UpdateAsync(model);
            }
        }
        
        public GetModelConnectorForEditOutput GetModelConnectorForEdit(EntityDto<int> input)
        {
            //Editing an existing charge point model connector
            var output = new GetModelConnectorForEditOutput();
            if (input.Id == 0)
            {
                output.ModelConnector = new ModelConnectorDto();
            }
            else
            {
                var modelConnector = from connector in _modelConnectorRepository.GetAll().Where(c => c.Id == input.Id).Include(m=>m.ModelEVSE).Include(m=>m.ModelEVSE.ChargepointModel)
                                     select new ModelConnectorDto
                                     {
                                         Id = connector.Id,
                                         VendorId = connector.ModelEVSE.ChargepointModel.VendorId,
                                         ChargepointModelId = connector.ModelEVSE.ChargepointModelId,
                                         Comment = connector.Comment,
                                         ConnectorTypeId = connector.ConnectorTypeId,
                                         CapacityId = connector.CapacityId,
                                         ConnectorId = connector.ConnectorId,
                                         ModelEVSEId = connector.ModelEVSEId,
                                         TenantId = connector.TenantId
                                     };

                output.ModelConnector = ObjectMapper.Map<ModelConnectorDto>(modelConnector.FirstOrDefault());
            }

            return output;
        }
        public GetModelConnectorsListOutput GetModelConnectorsList()
        {
            IEnumerable<ModelConnectorListDto> _modelConnectorsList = from modelConnector in _modelConnectorRepository.GetAll()
                                                                      select new ModelConnectorListDto
                                                                      {
                                                                          Id = modelConnector.Id,
                                                                          Name = modelConnector.ConnectorType.ConnectorName.ToString()
                                                                      };
            return new GetModelConnectorsListOutput { ModelConnectors = _modelConnectorsList.ToList() };
        }
        public GetModelConnectorsByChargepointModelListOutput GetModelConnectorsByModelEVSEList(int modeEVSElId)
        {
            IEnumerable<ModelConnectorByChargepointModelListDto> _modelConnectorsList = from modelConnector in _modelConnectorRepository.GetAll().Where(m=>m.ModelEVSEId == modeEVSElId)
                                                                                        select new ModelConnectorByChargepointModelListDto
                                                                                        {
                                                                                            Id = modelConnector.Id,
                                                                                            CapacityId = modelConnector.CapacityId,
                                                                                            ConnectorTypeId = modelConnector.ConnectorTypeId
                                                                                        };
            return new GetModelConnectorsByChargepointModelListOutput { ModelConnectors = _modelConnectorsList.ToList() };
        }
        public async Task<PagedResultDto<ModelConnectorDto>> GetModelConnector(GetModelConnectorInput input)
        {
            var query = (from modelConnector in _modelConnectorRepository.GetAll().Include(m => m.ModelEVSE).Include(m => m.ModelEVSE.ChargepointModel)
                         .Include(m => m.ModelEVSE.ChargepointModel.Vendor).Include(m => m.ConnectorType)
                         select new ModelConnectorDto
                         {
                             Id = modelConnector.Id,
                             Comment = modelConnector.Comment,
                             ConnectorTypeId = modelConnector.ConnectorTypeId,
                             ConnectorType = modelConnector.ConnectorType.ConnectorName,
                             ModelName = modelConnector.ModelEVSE.ChargepointModel.ModelName,
                             ModelEVSEId = modelConnector.ModelEVSE.EVSEId,
                             ConnectorId = modelConnector.ConnectorId,
                             CapacityId = modelConnector.CapacityId,
                             Vendor = modelConnector.ModelEVSE.ChargepointModel.Vendor.Name,
                             ChargepointModelId = modelConnector.ModelEVSE.ChargepointModelId,
                             TenantId = modelConnector.TenantId
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.ModelName.Contains(input.Filter) || item.Comment.Contains(input.Filter))
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), item => item.ModelName == input.Name)
                .WhereIf(!input.Comment.IsNullOrWhiteSpace(), item => item.Comment == input.Comment)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<ModelConnectorDto>(resultCount, results.ToList());
        }
        public string GetCapacitiesByModelId(EntityDto<int> input)
        {
            string capacities = _modelConnectorRepository.GetAll().Include(c => c.Capacity).Include(c => c.Capacity.Unit).Where(m => m.ModelEVSE.ChargepointModelId == input.Id).Select(c => c.Capacity.Value + " " + c.Capacity.Unit.UnitName).JoinAsString(",");
            return capacities;
        }
        public int GetConnectorNo(EntityDto<int> input)
        {
            int count = _modelConnectorRepository.GetAll().Where(c => c.ModelEVSE.ChargepointModelId == input.Id).Count();
            return count + 1;
        }
        public async Task DeleteModelConnector(EntityDto<int> input)
        {
            var modelConnector = await _modelConnectorRepository.GetAsync(input.Id);
            await _modelConnectorRepository.DeleteAsync(modelConnector);
        }
    }
}
