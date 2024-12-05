using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Sixoclock.Onyx.OCPPFeatures.Dto;
using System.Collections.Generic;

namespace Sixoclock.Onyx.ChargePointModels.Dto
{
    [AutoMapFrom(typeof(ChargepointModel))]
    public class ChargepointModelDto : FullAuditedEntityDto, IHasCreationTime
    {
        public string ModelName { get; set; }
        public string Comment { get; set; }
        public int MountTypeId { get; set; }
        public int VendorId { get; set; }
        public int TenantId { get; set; }        
        public string FirmwareLocation { get; set; }
        public int OCPPVersionId { get; set; }
        public int OCPPTransportId { get; set; }

        public List<ChargeReleaseOptionDto> Releases { get; set; }
        public List<ElectricalOptionDto> Electrics { get; set; }
        public List<ComOptionDto> Coms { get; set; }
        public List<OtherOptionDto> Others { get; set; }
        public GetOCPPFeaturesListOutput OCPPFeatures { get; set; }
        public string VendorName { get; set; }
        public string MountName { get; set; }
        public string OCPPTransportName { get; internal set; }
        public string VersionName { get; internal set; }
        public int ModelEvseCount { get; internal set; }
        public int ModelConnectorsCount { get; internal set; }
    }
}
