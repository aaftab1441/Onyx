using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Sixoclock.Onyx.OCPPFeatures.Dto;
using Sixoclock.Onyx.EVSEs.Dto;
using System.Collections.Generic;
using Sixoclock.Onyx.ResetEvents.Dto;
using Sixoclock.Onyx.RemoteStartStopEvents.Dto;
using Sixoclock.Onyx.UnlockEvents.Dto;
using Sixoclock.Onyx.ClearCacheEvents.Dto;

namespace Sixoclock.Onyx.Chargepoints.Dto
{
    [AutoMapFrom(typeof(Chargepoint))]
    public class ChargepointDto : FullAuditedEntityDto, IHasCreationTime
    {
        public int GroupId { get; set; }
        public int ChargepointModelId { get; set; }
        public int AdminStatusId { get; set; }
        public int? OCPPVersionId { get; set; }
        public string FirmwareVersion { get; set; }
        public string FirmwareLocation { get; set; }
        public string Identity { get; set; }
        public int ConnectorNumber { get; set; }
        public string SerialNumber { get; set; }
        public string MeterSerialNumber { get; set; }
        public string Place { get; set; }
        public string IccId { get; set; }
        public string Imsi { get; set; }
        public int? LocalAuthListVersion { get; set; }
        public int? AccUptime { get; set; }
        public int? AccChargetime { get; set; }
        public int? AccFaulttime { get; set; }
        public int? AccEnergyDelivery { get; set; }
        public int? Temperature { get; set; }
        public string TempUnit { get; set; }
        public string Comment { get; set; }
        public string NetworkAddress { get; set; }
        public int? Port { get; set; }
        public int? OCPPTransportId { get; set; }
        public int? RegistrationStatusId { get; set; }
        public int? AvailabilityTypeId { get; set; }
        public int? FirmwareStatusId { get; set; }
        public string ConnectorStatus { get; set; }
        public int TenantId { get; set; }
        public string GroupName { get; set; }
        public string VendorName { get; set; }
        public string ModelName { get; set; }
        public string MountTypeName { get; set; }
        public int MountTypeId { get; set; }
        public string Status { get; set; }

        public GetOCPPFeaturesListOutput OCPPFeatures { get; set; }
        public IList<EVSEDto> EVSEs { get; set; }
        public IList<ResetEventDto> ResetEvents { get; set; }
        public IList<RemoteStartStopEventDto> RemoteStartStopEvents { get; set; }
        public string AdminStatus { get; internal set; }
        public string OCPPVersionTransportName { get; internal set; }
        public int Connectors { get; internal set; }
        public string InstallName { get; internal set; }
        public int EVSEsCount { get; internal set; }
        public List<UnlockEventDto> UnlockEvents { get; internal set; }
        public List<ClearCacheEventDto> ClearCacheEvents { get; internal set; } 
    }
}
