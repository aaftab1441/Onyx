using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.ModelEVSEs.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.ModelEVSEs
{
    public class ModelEVSEAppService : OnyxAppServiceBase, IModelEVSEAppService
    {
        private readonly IRepository<ModelEVSE> _modelEVSERepository;

        public ModelEVSEAppService(IRepository<ModelEVSE> modelEVSERepository)
        {
            _modelEVSERepository = modelEVSERepository;
        }
        public async Task CreateOrUpdateModelEVSE(CreateOrUpdateModelEVSEInput input)
        {
            var modelEVSE = ObjectMapper.Map<ModelEVSE>(input);

            if (input.Id == 0)
            {
                await _modelEVSERepository.InsertAsync(modelEVSE);
            }
            else
            {
                await _modelEVSERepository.UpdateAsync(modelEVSE);
            }
        }
        public GetModelEVSEForEditOutput GetModelEVSEForEdit(EntityDto<int> input)
        {
            //Editing an existing model EVSE
            var output = new GetModelEVSEForEditOutput();
            if (input.Id == 0)
            {
                output.ModelEVSE = new ModelEVSEDto();
            }
            else
            {
                var modelEVSE = (from model in _modelEVSERepository.GetAll().Where(m => m.Id == input.Id).Include(m => m.ModelConnectors).Include(m => m.ChargepointModel).Include(m => m.ChargepointModel.Vendor).Include(m => m.MeterType)
                                select new ModelEVSEDto
                                {
                                    Id = model.Id,
                                    MeterTypeId = model.MeterTypeId,
                                    Vendor = model.ChargepointModel.Vendor.Name,
                                    VendorId = model.ChargepointModel.VendorId,
                                    ModelName = model.ChargepointModel.ModelName,
                                    ChargepointModelId = model.ChargepointModelId,
                                    EVSEId = model.EVSEId,
                                    MeterType = model.MeterType.Type,
                                    Comment = model.Comment,
                                    ConnectorsCount = model.ModelConnectors.Count,
                                    TenantId = model.TenantId,
                                    CreationTime = model.CreationTime
                                }).FirstOrDefault();

                output.ModelEVSE = modelEVSE;
            }

            return output;
        }
        public GetModelEVSEsListOutput GetModelEVSEsList()
        {
            IEnumerable<ModelEVSEListDto> _modelEVSEsList = from modelEVSE in _modelEVSERepository.GetAll()
                                                            select new ModelEVSEListDto
                                                            {
                                                                Id = modelEVSE.Id,
                                                                EVSEId = modelEVSE.EVSEId
                                                            };
            return new GetModelEVSEsListOutput { ModelEVSEs = _modelEVSEsList.ToList() };
        }
        public async Task<PagedResultDto<ModelEVSEDto>> GetModelEVSE(GetModelEVSEInput input)
        {
            var query = (from modelEVSE in _modelEVSERepository.GetAll().Include(m => m.ModelConnectors).Include(m => m.ChargepointModel)
                         .Include(m => m.ChargepointModel.Vendor).Include(m => m.MeterType)
                         select new ModelEVSEDto
                         {
                             Id = modelEVSE.Id,
                             MeterTypeId = modelEVSE.MeterTypeId,
                             Vendor = modelEVSE.ChargepointModel.Vendor.Name,
                             VendorId = modelEVSE.ChargepointModel.VendorId,
                             ModelName = modelEVSE.ChargepointModel.ModelName,
                             ChargepointModelId = modelEVSE.ChargepointModelId,
                             EVSEId = modelEVSE.EVSEId,
                             MeterType = modelEVSE.MeterType.Type,
                             Comment = modelEVSE.Comment,
                             TenantId = modelEVSE.TenantId,
                             ConnectorsCount = modelEVSE.ModelConnectors.Count,
                             CreationTime = modelEVSE.CreationTime
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .WhereIf(!input.Filter.IsNullOrWhiteSpace(), item => item.ModelName.Contains(input.Filter) || item.Comment.Contains(input.Filter))
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), item => item.ModelName == input.Name)
                .WhereIf(!input.Comment.IsNullOrWhiteSpace(), item => item.Comment == input.Comment)
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<ModelEVSEDto>(resultCount, results.ToList());
        }
        public int GetEVSEIdCount(EntityDto<int> input)
        {
            int count = _modelEVSERepository.GetAll().Where(c => c.ChargepointModelId == input.Id).Count();
            return count + 1;
        }
        public async Task DeleteModelEVSE(EntityDto<int> input)
        {
            var modelEVSE = await _modelEVSERepository.GetAsync(input.Id);
            await _modelEVSERepository.DeleteAsync(modelEVSE);
        }

        public GetModelEVSEsListOutput GetModelEVSEsList(int chargepointModelId)
        {
            IEnumerable<ModelEVSEListDto> _modelEVSEsList = from modelEVSE in _modelEVSERepository.GetAll().Include(f => f.ChargepointModel).Where(f => f.ChargepointModelId == chargepointModelId)
                                                                   select new ModelEVSEListDto
                                                                   {
                                                                       Id = modelEVSE.Id,
                                                                       MeterTypeId = modelEVSE.MeterTypeId,
                                                                       VendorId = modelEVSE.ChargepointModel.VendorId,
                                                                       ChargepointModelId = modelEVSE.ChargepointModelId,
                                                                       EVSEId = modelEVSE.EVSEId,
                                                                       Comment = modelEVSE.Comment,
                                                                       IsDeleted = modelEVSE.IsDeleted,
                                                                       TenantId = modelEVSE.TenantId
                                                                   };
            return new GetModelEVSEsListOutput { ModelEVSEs = _modelEVSEsList.ToList() };
        }
    }
}
