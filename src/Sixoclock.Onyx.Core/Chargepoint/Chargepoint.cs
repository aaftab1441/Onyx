using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.Collections.Generic;
using Sixoclock.Onyx.Grants;

namespace Sixoclock.Onyx
{
    [Ruleable(typeof(Chargepoint))]
    public class Chargepoint : FullAuditedEntity, IMustHaveTenant
    {
        public int GroupId { get; set; }
        public int? RegistrationStatusId { get; set; }
        public int? OCPPVersionId { get; set; }
        public int? OCPPTransportId { get; set; }
        public int? FirmwareStatusId { get; set; }
        public string FirmwareVersion { get; set; }
        public string FirmwareLocation { get; set; }
        [Ruleable(typeof(string))]
        public string Identity { get; set; }
        [Ruleable(typeof(string),"Serial Number")]
        public string SerialNumber { get; set; }
        [Ruleable(typeof(string),"Meter Serial Number")]
        public string MeterSerialNumber { get; set; }
        [Ruleable(typeof(string))]
        public string Place { get; set; }
        public string IccId { get; set; }
        public string Imsi { get; set; }
        public int? LocalAuthListVersion { get; set; }
        public int? AccUptime { get; set; }
        public int? AccChargetime { get; set; }
        public int? AccFaulttime { get; set; }
        public int? AccEnergyDelivery { get; set; }
        [Ruleable(typeof(int))]
        public int? Temperature { get; set; }
        public string TempUnit { get; set; }
        public string Comment { get; set; }
        public string NetworkAddress { get; set; }
        public int? Port { get; set; }
        public int ChargepointModelId { get; set; }
        public int AdminStatusId { get; set; }   
        
        public int TenantId { get; set; }
        
        public virtual RegistrationStatus RegistrationStatus { get; set; }
        public virtual OCPPVersion OCPPVersion { get; set; }
        public virtual OCPPTransport OCPPTransport { get; set; }
        public virtual ICollection<ChargepointFeature> ConnectorFeatures { get; set; }
        public virtual ICollection<ChargepointKeyValue> ConnectorKeyValues { get; set; }
        public virtual ICollection<Heartbeat> Heartbeats { get; set; }
        public virtual ICollection<ResetEvent> ResetEvents { get; set; }
        public virtual FirmwareStatus FirmwareStatus { get; set; }
        public virtual Group Group { get; set; }
        public virtual ChargepointModel ChargepointModel { get; set; }
        [Ruleable(typeof(AdminStatus),"Admin Status")]
        public virtual AdminStatus AdminStatus { get; set; }
        public virtual ICollection<AdminStatusEvent> AdminStatusEvents { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<OCPPMessageEvent> OCPPMessageEvents { get; set; }
        public virtual ICollection<EVSE> EVSEs { get; set; }
    }
}
