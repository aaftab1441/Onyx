using Abp.AutoMapper;
using System.Collections.Generic;

namespace Sixoclock.Onyx.Chargepoints.Dto
{
    [AutoMapTo(typeof(Chargepoint))]
    public class CreateOrUpdateChargepointInput
    {
        public int Id { get; set; }
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
        public int Connectors { get; set; }

        public List<ChargepointFeature> ChargepointFeature { get; set; }
    }
}
