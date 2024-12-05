using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.ChargepointModelImages.Dto;
using Sixoclock.Onyx.IO;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sixoclock.Onyx.ChargepointModelImages
{
    public class ChargepointModelImageAppService : OnyxAppServiceBase, IChargepointModelImageAppService
    {
        private readonly IAppFolders _appFolders;
        private readonly IRepository<ChargepointModelImage> _chargepointModelImageRepository;

        public ChargepointModelImageAppService(IRepository<ChargepointModelImage> chargepointModelImageRepository, IAppFolders appFolders)
        {
            _chargepointModelImageRepository = chargepointModelImageRepository;
            _appFolders = appFolders;
        }

        public async Task CreateOrUpdateChargepointModelImage(CreateOrUpdateChargepointModelImageInput input)
        {
            var tenantsFolderPath = Path.Combine(_appFolders.TenantsFolder, input.OriginalFileName);

            byte[] byteArray;

            using (var fsTempProfilePicture = new FileStream(tenantsFolderPath, FileMode.Open))
            {
                using (var bmpImage = new Bitmap(fsTempProfilePicture))
                {
                    var width = input.Width == 0 ? bmpImage.Width : input.Width;
                    var height = input.Height == 0 ? bmpImage.Height : input.Height;
                    var bmCrop = bmpImage.Clone(new Rectangle(input.X, input.Y, width, height), bmpImage.PixelFormat);

                    using (var stream = new MemoryStream())
                    {
                        bmCrop.Save(stream, bmpImage.RawFormat);
                        byteArray = stream.ToArray();
                    }
                }
            }

            //Delete old temp profile pictures
            AppFileHelper.DeleteFilesInFolderIfExists(_appFolders.TenantsFolder, input.OriginalFileName);

            File.WriteAllBytes(tenantsFolderPath, byteArray);

            //Save new picture
            input.Ext = Path.GetExtension(tenantsFolderPath);

            var chargepointModelImage = ObjectMapper.Map<ChargepointModelImage>(input);

            if (input.Id == 0)
            {
                await _chargepointModelImageRepository.InsertAsync(chargepointModelImage);
            }
            else
            {
                await _chargepointModelImageRepository.UpdateAsync(chargepointModelImage);
            }
        }

        public async Task<GetChargepointModelImageForEditOutput> GetChargepointModelImageForEdit(EntityDto<int> input)
        {
            //Editing an existing Charge point Model Image
            var output = new GetChargepointModelImageForEditOutput();
            if (input.Id == 0)
            {
                output.ChargepointModelImage = new ChargepointModelImageDto();
            }
            else
            {
                var chargepointModelImage = await _chargepointModelImageRepository.GetAsync(input.Id);

                output.ChargepointModelImage = ObjectMapper.Map<ChargepointModelImageDto>(chargepointModelImage);
            }

            return output;
        }

        public async Task<PagedResultDto<ChargepointModelImageDto>> GetChargepointModelImage(GetChargepointModelImageInput input)
        {
            var query = (from image in _chargepointModelImageRepository.GetAll()
            .Include(c => c.ChargepointModel)
            .WhereIf(!input.Filter.IsNullOrEmpty(),
                p => p.Comment.ToLower().Contains(input.Filter.ToLower()) ||
                        p.ChargepointModel.ModelName.ToLower().Contains(input.Filter.ToLower())
            )
            .WhereIf(!input.Comment.IsNullOrEmpty(),
            p => p.Comment.ToLower().Contains(input.Comment.ToLower()))
            .WhereIf(!input.ModelName.IsNullOrEmpty(),
            p => p.ChargepointModel.ModelName.ToLower().Contains(input.ModelName.ToLower()))
                         select new ChargepointModelImageDto
                         {
                             Id = image.Id,
                             ChargepointModelId = image.ChargepointModelId,
                             Comment = image.Comment,
                             CreationTime = image.CreationTime,
                             Ext = image.Ext,
                             IsActive = image.IsActive,
                             ModelName = image.ChargepointModel.ModelName,
                             OriginalFileName = image.OriginalFileName,
                             TenantId = image.TenantId
                         }).AsQueryable();

            var resultCount = await query.CountAsync();
            var results = await query
                .OrderBy(p => p.Comment)
                .ThenBy(p => p.ChargepointModelId)
                .PageBy(input)
                .ToListAsync();
            return new PagedResultDto<ChargepointModelImageDto>(resultCount, results.ToList());
        }

        public async Task DeleteChargepointModelImage(EntityDto<int> input)
        {
            var chargepointModelImage = await _chargepointModelImageRepository.GetAsync(input.Id);
            await _chargepointModelImageRepository.DeleteAsync(chargepointModelImage);
        }
    }
}
