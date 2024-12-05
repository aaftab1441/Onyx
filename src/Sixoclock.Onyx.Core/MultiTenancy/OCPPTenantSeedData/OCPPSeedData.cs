using System;
using System.Collections.Generic;
using System.Text;
using Sixoclock.Onyx.Grants;

namespace Sixoclock.Onyx.MultiTenancy.OCPPTenantSeedData
{
    public static class OCPPSeedData
    {
        public static List<AuthorizationStatus> GetInitialAuthorizationStatuses(int tenantId)
        {
            return new List<AuthorizationStatus>
            {
                new AuthorizationStatus() { Value = "Accepted", Comment = "Identifier is allowed for charging." , TenantId = tenantId },
                new AuthorizationStatus() { Value = "Blocked", Comment = "Identifier has been blocked. Not allowed for charging." , TenantId = tenantId },
                new AuthorizationStatus() { Value = "Expired", Comment = "Identifier has expired. Not allowed for charging." , TenantId = tenantId },
                new AuthorizationStatus() { Value = "Invalid", Comment = "Identifier is unknown. Not allowed for charging." , TenantId = tenantId },
                new AuthorizationStatus() { Value = "NoCredit", Comment = "Identifier is valid, but EV Driver doesn’t have enough credit to start charging. Not allowed for charging." , TenantId = tenantId },
                new AuthorizationStatus() { Value = "NotAllowedTypeEVSE", Comment = "Identifier is valid, but not allowed to charge it this type of EVSE." , TenantId = tenantId },
                new AuthorizationStatus() { Value = "NotAtThisLocation", Comment = "Identifier is valid, but not allowed to charge it this location." , TenantId = tenantId },
                new AuthorizationStatus() { Value = "NotAtThisTime", Comment = "Identifier is valid, but not allowed to charge it this location at this time." , TenantId = tenantId },
                new AuthorizationStatus() { Value = "ConcurrentTX", Comment = "Identifier is already involved in another transaction and multiple transactions are not allowed." , TenantId = tenantId }
            };
        }
        public static List<AvailabilityStatus> GetInitialAvailabilityStatuses(int tenantId)
        {
            return new List<AvailabilityStatus>
            {
                new AvailabilityStatus() { Value = "Accepted", TenantId = tenantId },
                new AvailabilityStatus() { Value = "Rejected", TenantId = tenantId },
                new AvailabilityStatus() { Value = "Scheduled", TenantId = tenantId },
                new AvailabilityStatus() { Value = "Initiated", TenantId = tenantId }
            };
        }
        public static List<AvailabilityType> GetInitialAvailabilityTypes(int tenantId)
        {
            return new List<AvailabilityType>
            {
                new AvailabilityType() { Value = "Inoperative", Comment = "Charge point is not available for charging.", TenantId = tenantId },
                new AvailabilityType() { Value = "Operative", Comment = "Charge point is available for charging.", TenantId = tenantId }
            };
        }
        public static List<BillingStatus> GetInitialBillingStatuss(int tenantId)
        {
            return new List<BillingStatus>
            {
                new BillingStatus() { Value = "To Pay",Comment="Bill to be paid", TenantId = tenantId },
                new BillingStatus() { Value = "Paid",Comment="Bill is paid", TenantId = tenantId },
                new BillingStatus() { Value = "Due",Comment="Bill is due for Payment", TenantId = tenantId }
            };
        }
        public static  List<BillingType> GetInitialBillingTypes(int tenantId)
        {
            return new List<BillingType>
            {
                new BillingType() { Type = "Spine",Comment="Billing made by Spine", TenantId = tenantId },
                new BillingType() { Type = "Tenant",Comment="Billing made by Tenant", TenantId = tenantId }
            };
        }
        public static List<CancelReservationStatus> GetInitialCancelReservationStatuses(int tenantId)
        {
            return new List<CancelReservationStatus>
            {
                new CancelReservationStatus() { Value = "Accepted", Comment = "Reservation for the identifier has been cancelled." , TenantId = tenantId },
                new CancelReservationStatus() { Value = "Rejected", Comment = "Reservation could not be cancelled, because there is no reservation active for the identifier." , TenantId = tenantId }
            };
        }
        public static List<Capacity> GetInitialCapacities(int tenantId)
        {
            List<Capacity> Capacities = new List<Capacity>
            {
                new Capacity() { Value = "3.7", PowerId=1, UnitId=6, Comment="TBD", TenantId = tenantId},
                new Capacity() { Value = "11", PowerId=2, UnitId=6, Comment="TBD", TenantId = tenantId},
                new Capacity() { Value = "22", PowerId=2, UnitId=6, Comment="TBD", TenantId = tenantId},
                new Capacity() { Value = "44", PowerId=2, UnitId=6, Comment="TBD", TenantId = tenantId},
                new Capacity() { Value = "50", PowerId=3, UnitId=6, Comment="TBD", TenantId = tenantId}
            };
            //foreach (var capacity in Capacities)
            //{
            //    if (capacity.PowerId == 1)
            //        capacity.PowerId = _context.Powers.Where(l => l.TenantId == tenantId && l.PowerName == "1-Phase").SingleOrDefault().Id;
            //    else if (capacity.PowerId == 2)
            //        capacity.PowerId = _context.Powers.Where(l => l.TenantId == tenantId && l.PowerName == "3-Phase").SingleOrDefault().Id;
            //    else if (capacity.PowerId == 3)
            //        capacity.PowerId = _context.Powers.Where(l => l.TenantId == tenantId && l.PowerName == "DC").SingleOrDefault().Id;
            //}
            //foreach (var capacity in Capacities)
            //{
            //    if (capacity.UnitId == 6)
            //        capacity.UnitId = _context.Units.Where(l => l.TenantId == tenantId && l.UnitName == "kW").SingleOrDefault().Id;
            //}
            return Capacities;
        }
        public static List<ClearCacheStatus> GetInitialclearCacheStatuses(int tenantId)
        {
            return new List<ClearCacheStatus>
            {
                new ClearCacheStatus() { Value = "Accepted", TenantId = tenantId },
                new ClearCacheStatus() { Value = "Rejected", TenantId = tenantId },
                new ClearCacheStatus() { Value = "Initiated", TenantId = tenantId }
            };
        }
        public static List<ConfigStatus> GetInitialConfigStatuses(int tenantId)
        {
            return new List<ConfigStatus>
            {
                new ConfigStatus() { Value = "Accepted", TenantId = tenantId },
                new ConfigStatus() { Value = "Rejected", TenantId = tenantId },
                new ConfigStatus() { Value = "RebootRequired", TenantId = tenantId },
                new ConfigStatus() { Value = "NotSupported", TenantId = tenantId },
                new ConfigStatus() { Value = "Initiated", TenantId = tenantId }
            };
        }
        public static List<ConfigType> GetInitialConfigTypes(int tenantId)
        {
            return new List<ConfigType>
            {
                new ConfigType() { Type = "Single", Comment = "Fetch one config keyvalues.", TenantId = tenantId },
                new ConfigType() { Type = "All", Comment = "Fetch all config keyvalues.", TenantId = tenantId }
            };
        }
        public static List<ConnectorStatusCode> GetInitialConnectorStatusCodes(int tenantId)
        {
            return new List<ConnectorStatusCode>
            {
                new ConnectorStatusCode() { Status = "Available", Comment="When a Connector becomes available for a new user (Operative)", TenantId = tenantId },
                new ConnectorStatusCode() { Status = "Preparing", Comment="When a Connector becomes no longer available for a new user but no charging session is active. Typically a Connector is occupied when a user presents a tag, inserts a cable or a vehicle occupies the parking bay (Operative)", TenantId = tenantId },
                new ConnectorStatusCode() { Status = "Charging", Comment="When the contactor of a Connector closes, allowing the vehicle to charge (Operative)", TenantId = tenantId },
                new ConnectorStatusCode() { Status = "SuspendedEVSE", Comment="When the contactor of a Connector opens upon request of the EVSE, e.g. due to a smart chargingn restriction or as the result of StartTransaction.conf indicating that charging is not allowed (Operative)", TenantId = tenantId },
                new ConnectorStatusCode() { Status = "SuspendedEV", Comment="When the EVSE is ready to deliver energy but contactor is open, e.g. the EV is not ready.", TenantId = tenantId },
                new ConnectorStatusCode() { Status = "Finishing", Comment="When a charging session has stopped at a Connector, but the Connector is not yet available for a new user, e.g. the cable has not been removed or the vehicle has not left the parking bay (Operative)", TenantId = tenantId },
                new ConnectorStatusCode() { Status = "Reserved", Comment="When a Connector becomes reserved as a result of a Reserve Now command (Operative)", TenantId = tenantId },
                new ConnectorStatusCode() { Status = "Occupied", Comment="When an EVSE becomes occupied, so it is not available for a new EV driver. (Operative)", TenantId = tenantId },
                new ConnectorStatusCode() { Status = "Unavailable", Comment="When an EVSE becomes unavailable as the result of a Change Availability command or an event upon which the Charge Point transitions to unavailable at its discretion. Upon receipt of ChangeAvailability.req message command, the status MAY change immediately or the change MAY be scheduled. When scheduled, StatusNotification.req SHALL be send when the availability change becomes effective (Inoperative)", TenantId = tenantId },
                new ConnectorStatusCode() { Status = "Faulted", Comment="When a Charge Point or evse has reported an error and is not available for energy delivery. (Inoperative).", TenantId = tenantId },
                new ConnectorStatusCode() { Status = "ExternalChargingLimitSet", Comment="Indicates that an external charging limit is set.", TenantId = tenantId },
                new ConnectorStatusCode() { Status = "ExternalChargingLimitReleased", Comment="Indicates that an external charging limit is released / reset.", TenantId = tenantId }
            };
        }
        public static List<ConnectorType> GetInitialConnectorTypes(int tenantId)
        {
            return new List<ConnectorType>
            {
                new ConnectorType() { ConnectorName = "cCCS1", Comment = "Combined Charging System 1 (captive cabled) a.k.a. Combo 1", TenantId = tenantId },
                new ConnectorType() { ConnectorName = "cCCS2", Comment = "Combined Charging System 2 (captive cabled) a.k.a. Combo 2", TenantId = tenantId },
                new ConnectorType() { ConnectorName = "cG105", Comment = "JARI G105-1993 (captive cabled) a.k.a. CHAdeMO", TenantId = tenantId },
                new ConnectorType() { ConnectorName = "cTesla", Comment = "Tesla Connector (captive cabled)", TenantId = tenantId },
                new ConnectorType() { ConnectorName = "cType1", Comment = "IEC62196-2 Type 1 connector (captive cabled) a.k.a. J1772", TenantId = tenantId },
                new ConnectorType() { ConnectorName = "cType2", Comment = "IEC62196-2 Type 2 connector (captive cabled) a.k.a. Mennekes socket", TenantId = tenantId },
                new ConnectorType() { ConnectorName = "s309-1P-16A", Comment = "16A 1 phase IEC60309 socket", TenantId = tenantId },
                new ConnectorType() { ConnectorName = "s309-1P-32A", Comment = "32A 1 phase IEC60309 socket", TenantId = tenantId },
                new ConnectorType() { ConnectorName = "s309-3P-16A", Comment = "16A 3 phase IEC60309 socket", TenantId = tenantId },
                new ConnectorType() { ConnectorName = "s309-3P-32A", Comment = "32A 3 phase IEC60309 socket", TenantId = tenantId },
                new ConnectorType() { ConnectorName = "sBS1361", Comment = "UK domestic socket a.k.a. 13Amp", TenantId = tenantId },
                new ConnectorType() { ConnectorName = "sCEE-7-7", Comment = "CEE 7/7 16A socket. May represent 7/4 & 7/5 a.k.a Schuko", TenantId = tenantId },
                new ConnectorType() { ConnectorName = "sType2", Comment = "IEC62196-2 Type 2 socket a.k.a. Mennekes connector", TenantId = tenantId },
                new ConnectorType() { ConnectorName = "sType3", Comment = "IEC62196-2 Type 2 socket a.k.a. Scame", TenantId = tenantId },
                new ConnectorType() { ConnectorName = "Other1PhMax16A", Comment = "Other single phase (domestic) sockets not mentioned above, rated at no more than 16A. CEE7/17, AS3112, NEMA 5-15, NEMA 5-20, JISC8303, TIS166, SI 32, CPCS-CCC, SEV1011, etc.", TenantId = tenantId },
                new ConnectorType() { ConnectorName = "Other1PhOver16A", Comment = "Other single phase sockets not mentioned above (over 16A)", TenantId = tenantId },
                new ConnectorType() { ConnectorName = "Other3Ph", Comment = "Other 3 phase sockets not mentioned above. NEMA14-30, NEMA14-50.", TenantId = tenantId },
                new ConnectorType() { ConnectorName = "wInductive", Comment = "Wireless inductively coupled connection (generic)", TenantId = tenantId },
                new ConnectorType() { ConnectorName = "wResonant", Comment = "Wireless resonant coupled connection (generic)", TenantId = tenantId },
                new ConnectorType() { ConnectorName = "Undetermined", Comment = "Yet to be determined (e.g. before plugged in)", TenantId = tenantId },
                new ConnectorType() { ConnectorName = "Unknown", Comment = "Unknown; not determinable", TenantId = tenantId },
            };
        }
        public static List<Context> GetInitialContexts(int tenantId)
        {
            return new List<Context>
            {
                new Context() { ContextName = "Interruption.Begin", Comment = "Value taken at start of interruption.", TenantId = tenantId },
                new Context() { ContextName = "Interruption.End", Comment = "Value taken when resuming after interruption.", TenantId = tenantId },
                new Context() { ContextName = "Other", Comment = "Value for any other situations.", TenantId = tenantId },
                new Context() { ContextName = "Sample.Clock", Comment = "Value taken at clock aligned interval.", TenantId = tenantId },
                new Context() { ContextName = "Sample.Periodic", Comment = "Value taken as periodic sample relative to start time of transaction.", TenantId = tenantId },
                new Context() { ContextName = "Transaction.Begin", Comment = "Value taken at end of transaction.", TenantId = tenantId },
                new Context() { ContextName = "Transaction.End", Comment = "Value taken at start of transaction.", TenantId = tenantId },
                new Context() { ContextName = "Trigger", Comment = "Value taken in response to a TriggerMessage.req", TenantId = tenantId }
            };
        }
        public static List<Country> GetInitialCountries(int tenantId)
        {
            return new List<Country>
            {
                new Country() { Value = "Germany", Comment="Germany", TenantId = tenantId },
                new Country() { Value = "France", Comment="France", TenantId = tenantId },
                new Country() { Value = "Switzerland", Comment="Switzerland", TenantId = tenantId },
                new Country() { Value = "Austria", Comment="Austria", TenantId = tenantId },
                new Country() { Value = "Sweden", Comment="Sweden", TenantId = tenantId },
                new Country() { Value = "Denmark", Comment="Denmark", TenantId = tenantId },
                new Country() { Value = "Norway", Comment="Norway", TenantId = tenantId }
            };
        }
        public static List<Currency> GetInitialCurrencys(int tenantId)
        {
            return new List<Currency>
            {
                new Currency() { Value = "EUR",Comment="Euro", TenantId = tenantId },
                new Currency() { Value = "GBP",Comment="British Pound", TenantId = tenantId },
                new Currency() { Value = "SEK",Comment="Swedish Krona", TenantId = tenantId },
                new Currency() { Value = "DKK",Comment="Danish Krone", TenantId = tenantId },
                new Currency() { Value = "NOK",Comment="Norwegian Krone", TenantId = tenantId },
                new Currency() { Value = "USD",Comment="US Dollar", TenantId = tenantId }
            };
        }
        public static List<ErrorCode> GetInitialErrorCodes(int tenantId)
        {
            return new List<ErrorCode>
            {
                new ErrorCode() { Value = "ConnectorLockFailure", Comment="Failure to lock or unlock connector.", TenantId = tenantId },
                new ErrorCode() { Value = "EVCommunicationError", Comment="Communication failure with the vehicle, might be Mode 3 or other communication protocol problem. This is not a real error in the sense that the Charge Point doesn’t need to go to the faulted state. Instead, it should go to the SuspendedEVSE state.", TenantId = tenantId },
                new ErrorCode() { Value = "GroundFailure", Comment="Ground fault circuit interrupter has been activated.", TenantId = tenantId },
                new ErrorCode() { Value = "HighTemperature", Comment="Temperature inside Charge Point is too high.", TenantId = tenantId },
                new ErrorCode() { Value = "InternalError", Comment="Error in internal hard- or software component.", TenantId = tenantId },
                new ErrorCode() { Value = "LocalListConflict", Comment="The authorization information received from the Central System is in conflict with the LocalAuthorizationList.", TenantId = tenantId },
                new ErrorCode() { Value = "NoError", Comment="No error to report.", TenantId = tenantId },
                new ErrorCode() { Value = "OtherError", Comment="Other type of error. More information in vendorErrorCode.", TenantId = tenantId },
                new ErrorCode() { Value = "OverCurrentFailure", Comment="Over current protection device has tripped.", TenantId = tenantId },
                new ErrorCode() { Value = "OverVoltage", Comment="Voltage has risen above an acceptable level.", TenantId = tenantId },
                new ErrorCode() { Value = "PowerMeterFailure", Comment="Failure to read power meter.", TenantId = tenantId },
                new ErrorCode() { Value = "PowerSwitchFailure", Comment="Failure to control power switch.", TenantId = tenantId },
                new ErrorCode() { Value = "ReaderFailure", Comment="Failure with idTag reader.", TenantId = tenantId },
                new ErrorCode() { Value = "ResetFailure", Comment="Unable to perform a reset.", TenantId = tenantId },
                new ErrorCode() { Value = "UnderVoltage", Comment="Voltage has dropped below an acceptable level.", TenantId = tenantId },
                new ErrorCode() { Value = "WeakSignal", Comment="Wireless communication device reports a weak signal.", TenantId = tenantId }
            };
        }
        public static List<FirmwareStatus> GetInitialFirmwareStatuses(int tenantId)
        {
            return new List<FirmwareStatus>
            {
                new FirmwareStatus() { Value = "Cancelled", Comment = "The firmware update has been cancelled as result of a CancelFirmwareUpdate request. (Final state)", TenantId = tenantId },
                new FirmwareStatus() { Value = "Downloaded", Comment = "New firmware has been downloaded by Charge Point.", TenantId = tenantId },
                new FirmwareStatus() { Value = "DownloadingCertificate", Comment = "The Charge Point is downloading the Firmware Signing certificate", TenantId = tenantId },
                new FirmwareStatus() { Value = "DownloadFailed", Comment = "Charge point failed to download firmware.", TenantId = tenantId },
                new FirmwareStatus() { Value = "DownloadFinished", Comment = "Downloading of new firmware has finished.", TenantId = tenantId },
                new FirmwareStatus() { Value = "Downloading", Comment = "Firmware is being downloaded.", TenantId = tenantId },
                new FirmwareStatus() { Value = "DownloadScheduled", Comment = "DownloadScheduled Downloading of new firmware has been scheduled.", TenantId = tenantId },
                new FirmwareStatus() { Value = "DownloadPaused", Comment = "DownloadPaused Downloading has been paused.", TenantId = tenantId },
                new FirmwareStatus() { Value = "DownloadResumed", Comment = "Download has resumed", TenantId = tenantId },
                new FirmwareStatus() { Value = "Idle", Comment = "Charge Point is not performing firmware update related tasks. Status Idle SHALL only be used as in a FirmwareStatusNotification.req that was triggered by TriggerMessage.req", TenantId = tenantId },
                new FirmwareStatus() { Value = "InstallationFailed", Comment = "Installation of new firmware has failed.", TenantId = tenantId },
                new FirmwareStatus() { Value = "Installing", Comment = "Firmware is being installed.", TenantId = tenantId },
                new FirmwareStatus() { Value = "Installed", Comment = "New firmware has successfully been installed in charge point.", TenantId = tenantId },
                new FirmwareStatus() { Value = "InstallRebooting", Comment = "Charge Point is about to reboot to activate new firmware. This status MAY be omitted if a reboot is an integral part of the installation and cannot be reported separately.", TenantId = tenantId },
                new FirmwareStatus() { Value = "InstallScheduled", Comment = "Installation of the downloaded firmware is scheduled to take place on installDate given in UpdateFirmware request. This status MAY be omitted if installation takes place immediately.", TenantId = tenantId },
                new FirmwareStatus() { Value = "InstallVerificationFailed", Comment = "Verification of the new Firmware (e.g. using a checksum or some other means) has failed and installation will not proceed. (Final failure state)", TenantId = tenantId },
                new FirmwareStatus() { Value = "InvalidSignature", Comment = "The firmware signature is not valid.", TenantId = tenantId },
                new FirmwareStatus() { Value = "InvalidCertificate", Comment = "The Firmware Signing certificate is invalid.", TenantId = tenantId },
                new FirmwareStatus() { Value = "RevokedCertificate", Comment = "The Firmware Signing certificate has been revoked.", TenantId = tenantId }
            };
        }
        public static List<Format> GetInitialFormates(int tenantId)
        {
            return new List<Format>
            {
                new Format() { FormatType = "Raw", Comment = "Data is to be interpreted as integer/decimal numeric data.", TenantId = tenantId },
                new Format() { FormatType = "SignedData", Comment = "Data is represented as a signed binary data block, encoded as hex data.", TenantId = tenantId }
            };
        }
        public static List<RuleCondition> GetInitialGrantRuleConditionTypes(int tenantId)
        {
            return new List<RuleCondition>
            {
                new RuleCondition() { Value = "=", Comment = "Property value should contain value.", TenantId = tenantId },
                new RuleCondition() { Value = "!=", Comment = "Property value should not contain value.", TenantId = tenantId },
                new RuleCondition() { Value = ">", Comment = "Property value should be equal to value.", TenantId = tenantId },
                new RuleCondition() { Value = "<", Comment = "Property value should not be equal to value.", TenantId = tenantId },
                new RuleCondition() { Value = "()", Comment = "Property value should not be equal to value.", TenantId = tenantId }
            };
        }
        public static List<KeyValue> GetInitialKeyValues(int tenantId)
        {
            List<KeyValue> KeyValues = new List<KeyValue>
            {
                //KeyValues for OCPP 1.5 below (Lines 1-34)
                new KeyValue(){ Key ="AllowOfflineTxForUnknownId", DefaultValue ="FALSE", RW ="RW", Comment ="If this key exists, the Charge Point supports Unknown Offline Authorization. If this key reports a value of true, Unknown Offline Authorization is enabled.", TenantId = tenantId },
                new KeyValue(){ Key ="AuthorizationCacheEnabled", DefaultValue ="FALSE", RW ="RW", Comment ="If this key exists, the Charge Point supports an Authorization Cache. If this key reports a value of true, the Authorization Cache is enabled.", TenantId = tenantId },
                new KeyValue(){ Key ="AuthorizeRemoteTxRequests", DefaultValue ="FALSE", RW ="RW", Comment ="Whether a remote request to start a transaction in the form of a RemoteStartTransaction.req message should be authorized beforehand like a local action to start a transaction.", TenantId = tenantId },
                new KeyValue(){ Key ="BlinkRepeat", DefaultValue ="2", RW ="RW", Comment ="Number of times to blink Charge Point lighting when signalling", TenantId = tenantId },
                new KeyValue(){ Key ="ClockAlignedDataInterval", DefaultValue ="0", RW ="RW", Comment ="Size (in seconds) of the clock-aligned data interval. This is the size (in seconds) of the set of evenly spaced aggregation intervals per day, starting at 00:00:00 (midnight). For example, a value of 900 (15 minutes) indicates that every day should be broken into 96 15-minute intervals. When clock aligned data is being transmitted, the interval in question is identified by the start time and (optional) duration interval value, represented according to the ISO8601 standard. All \"per-period\" data (e.g. energy readings) should be accumulated (for \"flow\" type measurands such as energy), or averaged (for other values) across the entire interval (or partial interval, at the beginning or end of a charging session), and transmitted (if so enabled) at the end of each interval, bearing the interval start time timestamp. A value of \"0\" (numeric zero), by convention, is to be interpreted to mean that no clock-aligned data should be transmitted.", TenantId = tenantId },
                new KeyValue(){ Key ="ConnectionTimeOut", DefaultValue ="20", RW ="RW", Comment ="Interval (from successful authorization) until incipient charging session is automatically canceled due to failure of EV user to (correctly) insert the charging cable connector(s) into the appropriate connector(s).", TenantId = tenantId },
                new KeyValue(){ Key ="GetConfigurationMaxKeys", DefaultValue ="1", RW ="R", Comment ="Maximum number of requested configuration keys in a GetConfiguration.req PDU.", TenantId = tenantId },
                new KeyValue(){ Key ="HeartbeatInterval", DefaultValue ="900", RW ="RW", Comment ="Interval of inactivity (no OCPP exchanges) with central system after which the Charge Point should send a Heartbeat.req PDU", TenantId = tenantId },
                new KeyValue(){ Key ="LightIntensity", DefaultValue ="50", RW ="RW", Comment ="Percentage of maximum intensity at which to illuminate Charge Point lighting", TenantId = tenantId },
                new KeyValue(){ Key ="LocalAuthorizeOffline", DefaultValue ="FALSE", RW ="RW", Comment ="whether the Charge Point, when offline, will start a transaction for locally authorized identifiers.", TenantId = tenantId },
                new KeyValue(){ Key ="LocalPreAuthorize", DefaultValue ="FALSE", RW ="RW", Comment ="whether the Charge Point, when online, will start a transaction for locallyauthorized identifiers without waiting for or requesting an Authorize.conf from the Central System", TenantId = tenantId },
                new KeyValue(){ Key ="MaxEnergyOnInvalidId", DefaultValue ="10000", RW ="RW", Comment ="Maximum energy in Wh delivered when an identifier is invalidated by the Central System after start of a transaction.", TenantId = tenantId },
                new KeyValue(){ Key ="MeterValuesAlignedData", DefaultValue ="5", RW ="RW", Comment ="Clock-aligned measurand(s) to be included in a MeterValues.req PDU, every ClockAlignedDataInterval seconds", TenantId = tenantId },
                new KeyValue(){ Key ="MeterValuesAlignedDataMaxLength", DefaultValue ="10", RW ="R", Comment ="Maximum number of items in a MeterValuesAlignedData Configuration Key.", TenantId = tenantId },
                new KeyValue(){ Key ="MeterValuesSampledData", DefaultValue ="5", RW ="RW", Comment ="Sampled measurands to be included in a MeterValues.req PDU, every MeterValueSampleInterval seconds. Where applicable, the Measurand is combined with the optional phase; for instance: Voltage.L1 Default: \"Energy.Active.Import.Register\"", TenantId = tenantId },
                new KeyValue(){ Key ="MeterValuesSampledDataMaxLength", DefaultValue ="10", RW ="R", Comment ="Maximum number of items in a MeterValuesSampledData Configuration Key.", TenantId = tenantId },
                new KeyValue(){ Key ="MeterValueSampleInterval", DefaultValue ="0", RW ="RW", Comment ="Interval between sampling of metering (or other) data, intended to be transmitted by \"MeterValues\" PDUs. For charging session data (ConnectorId>0), samples are acquired and transmitted periodically at this interval from the start of the charging transaction. A value of \"0\" (numeric zero), by convention, is to be interpreted to mean that no sampled data should be transmitted.", TenantId = tenantId },
                new KeyValue(){ Key ="MinimumStatusDuration", DefaultValue ="5", RW ="RW", Comment ="The minimum duration that a Charge Point or Connector status is stable before a StatusNotification.req PDU is sent to the Central System.", TenantId = tenantId },
                new KeyValue(){ Key ="NumberOfConnectors", DefaultValue ="1", RW ="R", Comment ="The number of physical charging connectors of this Charge Point.", TenantId = tenantId },
                new KeyValue(){ Key ="ResetRetries", DefaultValue ="2", RW ="RW", Comment ="Number of times to retry an unsuccessful reset of the Charge Point.", TenantId = tenantId },
                new KeyValue(){ Key ="ConnectorPhaseRotation", DefaultValue ="1", RW ="RW", Comment ="The phase rotation per connector in respect to the connector’s energy meter (or if absent, the grid connection). Possible values per connector are: NotApplicable (for Single phase or DC Charge Points) Unknown (not (yet) known) RST (Standard Reference Phasing) RTS (Reversed Reference Phasing) SRT (Reversed 240 degree rotation) STR (Standard 120 degree rotation) TRS (Standard 240 degree rotation) TSR (Reversed 120 degree rotation) R can be identified as phase 1 (L1), S as phase 2 (L2), T as phase 3 (L3). If known, the Charge Point MAY also report the phase rotation between the grid connection and the main energymeter by using index number Zero (0). Values are reported in CSL, formatted: 0.RST, 1.RST, 2.RTS", TenantId = tenantId },
                new KeyValue(){ Key ="ConnectorPhaseRotationMaxLength", DefaultValue ="1", RW ="R", Comment ="Maximum number of items in a ConnectorPhaseRotation Configuration Key.", TenantId = tenantId },
                new KeyValue(){ Key ="StopTransactionOnEVSideDisconnect", DefaultValue ="TRUE", RW ="RW", Comment ="When set to true, the Charge Point SHALL administratively stop the transaction when the cable is unplugged from the EV.", TenantId = tenantId },
                new KeyValue(){ Key ="StopTransactionOnInvalidId", DefaultValue ="FALSE", RW ="RW", Comment ="whether the Charge Point will stop an ongoing transaction when it receives a non- Accepted authorization status in a StartTransaction.conf for this transaction", TenantId = tenantId },
                new KeyValue(){ Key ="StopTxnAlignedData", DefaultValue ="10", RW ="RW", Comment ="Clock-aligned periodic measurand(s) to be included in the TransactionData element of StopTransaction.req MeterValues.req PDU for every ClockAlignedDataInterval of the charging session", TenantId = tenantId },
                new KeyValue(){ Key ="StopTxnAlignedDataMaxLength", DefaultValue ="100", RW ="R", Comment ="Maximum number of items in a StopTxnAlignedData Configuration Key.", TenantId = tenantId },
                new KeyValue(){ Key ="StopTxnSampledData", DefaultValue ="10", RW ="RW", Comment ="Sampled measurands to be included in the TransactionData element of StopTransaction.req PDU, every MeterValueSampleInterval seconds from the start of the charging session", TenantId = tenantId },
                new KeyValue(){ Key ="StopTxnSampledDataMaxLength", DefaultValue ="100", RW ="R", Comment ="Maximum number of items in a StopTxnSampledData Configuration Key.", TenantId = tenantId },
                new KeyValue(){ Key ="SupportedFeatureProfiles", DefaultValue ="1", RW ="R", Comment ="A list of supported Feature Profiles. Possible profile identifiers: Core, FirmwareManagement, LocalAuthListManagement, Reservation, SmartCharging and RemoteTrigger.", TenantId = tenantId },
                new KeyValue(){ Key ="SupportedFeatureProfilesMaxLength", DefaultValue ="100", RW ="R", Comment ="Maximum number of items in a SupportedFeatureProfiles Configuration key", TenantId = tenantId },
                new KeyValue(){ Key ="TransactionMessageAttempts", DefaultValue ="3", RW ="RW", Comment ="How often the Charge Point should try to submit a transaction-related message when the Central System fails to process it.", TenantId = tenantId },
                new KeyValue(){ Key ="TransactionMessageRetryInterval", DefaultValue ="2", RW ="RW", Comment ="How long the Charge Point should wait before resubmitting a transactionrelated message that the Central System failed to process.", TenantId = tenantId },
                new KeyValue(){ Key ="UnlockConnectorOnEVSideDisconnect", DefaultValue ="TRUE", RW ="RW", Comment ="When set to true, the Charge Point SHALL unlock the cable on Charge Point side when the cable is unplugged at the EV.", TenantId = tenantId },
                new KeyValue(){ Key ="WebSocketPingInterval", DefaultValue ="10", RW ="RW", Comment ="Only relevant for websocket implementations. 0 disables client side websocket Ping/Pong. In this case there is either no ping/pong or the server initiates the ping and client responds with Pong. Positive values are interpreted as number of seconds between pings. Negative values are not allowed. ChangeConfiguration is expected to return a REJECTED result.", TenantId = tenantId },
                
                //KeyValues for Core OCPP 1.6 below (Lines 35-68)
                new KeyValue(){ Key ="AllowOfflineTxForUnknownId", DefaultValue ="FALSE", RW ="RW", Comment ="If this key exists, the Charge Point supports Unknown Offline Authorization. If this key reports a value of true, Unknown Offline Authorization is enabled.", TenantId = tenantId },
                new KeyValue(){ Key ="AuthorizationCacheEnabled", DefaultValue ="FALSE", RW ="RW", Comment ="If this key exists, the Charge Point supports an Authorization Cache. If this key reports a value of true, the Authorization Cache is enabled.", TenantId = tenantId },
                new KeyValue(){ Key ="AuthorizeRemoteTxRequests", DefaultValue ="FALSE", RW ="RW", Comment ="Whether a remote request to start a transaction in the form of a RemoteStartTransaction.req message should be authorized beforehand like a local action to start a transaction.", TenantId = tenantId },
                new KeyValue(){ Key ="BlinkRepeat", DefaultValue ="2", RW ="RW", Comment ="Number of times to blink Charge Point lighting when signalling", TenantId = tenantId },
                new KeyValue(){ Key ="ClockAlignedDataInterval", DefaultValue ="0", RW ="RW", Comment ="Size (in seconds) of the clock-aligned data interval. This is the size (in seconds) of the set of evenly spaced aggregation intervals per day, starting at 00:00:00 (midnight). For example, a value of 900 (15 minutes) indicates that every day should be broken into 96 15-minute intervals. When clock aligned data is being transmitted, the interval in question is identified by the start time and (optional) duration interval value, represented according to the ISO8601 standard. All \"per-period\" data (e.g. energy readings) should be accumulated (for \"flow\" type measurands such as energy), or averaged (for other values) across the entire interval (or partial interval, at the beginning or end of a charging session), and transmitted (if so enabled) at the end of each interval, bearing the interval start time timestamp. A value of \"0\" (numeric zero), by convention, is to be interpreted to mean that no clock-aligned data should be transmitted.", TenantId = tenantId },
                new KeyValue(){ Key ="ConnectionTimeOut", DefaultValue ="20", RW ="RW", Comment ="Interval (from successful authorization) until incipient charging session is automatically canceled due to failure of EV user to (correctly) insert the charging cable connector(s) into the appropriate connector(s).", TenantId = tenantId },
                new KeyValue(){ Key ="GetConfigurationMaxKeys", DefaultValue ="1", RW ="R", Comment ="Maximum number of requested configuration keys in a GetConfiguration.req PDU.", TenantId = tenantId },
                new KeyValue(){ Key ="HeartbeatInterval", DefaultValue ="900", RW ="RW", Comment ="Interval of inactivity (no OCPP exchanges) with central system after which the Charge Point should send a Heartbeat.req PDU", TenantId = tenantId },
                new KeyValue(){ Key ="LightIntensity", DefaultValue ="50", RW ="RW", Comment ="Percentage of maximum intensity at which to illuminate Charge Point lighting", TenantId = tenantId },
                new KeyValue(){ Key ="LocalAuthorizeOffline", DefaultValue ="FALSE", RW ="RW", Comment ="whether the Charge Point, when offline, will start a transaction for locally authorized identifiers.", TenantId = tenantId },
                new KeyValue(){ Key ="LocalPreAuthorize", DefaultValue ="FALSE", RW ="RW", Comment ="whether the Charge Point, when online, will start a transaction for locallyauthorized identifiers without waiting for or requesting an Authorize.conf from the Central System", TenantId = tenantId },
                new KeyValue(){ Key ="MaxEnergyOnInvalidId", DefaultValue ="10000", RW ="RW", Comment ="Maximum energy in Wh delivered when an identifier is invalidated by the Central System after start of a transaction.", TenantId = tenantId },
                new KeyValue(){ Key ="MeterValuesAlignedData", DefaultValue ="5", RW ="RW", Comment ="Clock-aligned measurand(s) to be included in a MeterValues.req PDU, every ClockAlignedDataInterval seconds", TenantId = tenantId },
                new KeyValue(){ Key ="MeterValuesAlignedDataMaxLength", DefaultValue ="10", RW ="R", Comment ="Maximum number of items in a MeterValuesAlignedData Configuration Key.", TenantId = tenantId },
                new KeyValue(){ Key ="MeterValuesSampledData", DefaultValue ="5", RW ="RW", Comment ="Sampled measurands to be included in a MeterValues.req PDU, every MeterValueSampleInterval seconds. Where applicable, the Measurand is combined with the optional phase; for instance: Voltage.L1 Default: \"Energy.Active.Import.Register\"", TenantId = tenantId },
                new KeyValue(){ Key ="MeterValuesSampledDataMaxLength", DefaultValue ="10", RW ="R", Comment ="Maximum number of items in a MeterValuesSampledData Configuration Key.", TenantId = tenantId },
                new KeyValue(){ Key ="MeterValueSampleInterval", DefaultValue ="0", RW ="RW", Comment ="Interval between sampling of metering (or other) data, intended to be transmitted by \"MeterValues\" PDUs. For charging session data (ConnectorId>0), samples are acquired and transmitted periodically at this interval from the start of the charging transaction. A value of \"0\" (numeric zero), by convention, is to be interpreted to mean that no sampled data should be transmitted.", TenantId = tenantId },
                new KeyValue(){ Key ="MinimumStatusDuration", DefaultValue ="5", RW ="RW", Comment ="The minimum duration that a Charge Point or Connector status is stable before a StatusNotification.req PDU is sent to the Central System.", TenantId = tenantId },
                new KeyValue(){ Key ="NumberOfConnectors", DefaultValue ="1", RW ="R", Comment ="The number of physical charging connectors of this Charge Point.", TenantId = tenantId },
                new KeyValue(){ Key ="ResetRetries", DefaultValue ="2", RW ="RW", Comment ="Number of times to retry an unsuccessful reset of the Charge Point.", TenantId = tenantId },
                new KeyValue(){ Key ="ConnectorPhaseRotation", DefaultValue ="1", RW ="RW", Comment ="The phase rotation per connector in respect to the connector’s energy meter (or if absent, the grid connection). Possible values per connector are: NotApplicable (for Single phase or DC Charge Points) Unknown (not (yet) known) RST (Standard Reference Phasing) RTS (Reversed Reference Phasing) SRT (Reversed 240 degree rotation) STR (Standard 120 degree rotation) TRS (Standard 240 degree rotation) TSR (Reversed 120 degree rotation) R can be identified as phase 1 (L1), S as phase 2 (L2), T as phase 3 (L3). If known, the Charge Point MAY also report the phase rotation between the grid connection and the main energymeter by using index number Zero (0). Values are reported in CSL, formatted: 0.RST, 1.RST, 2.RTS", TenantId = tenantId },
                new KeyValue(){ Key ="ConnectorPhaseRotationMaxLength", DefaultValue ="1", RW ="R", Comment ="Maximum number of items in a ConnectorPhaseRotation Configuration Key.", TenantId = tenantId },
                new KeyValue(){ Key ="StopTransactionOnEVSideDisconnect", DefaultValue ="TRUE", RW ="RW", Comment ="When set to true, the Charge Point SHALL administratively stop the transaction when the cable is unplugged from the EV.", TenantId = tenantId },
                new KeyValue(){ Key ="StopTransactionOnInvalidId", DefaultValue ="FALSE", RW ="RW", Comment ="whether the Charge Point will stop an ongoing transaction when it receives a non- Accepted authorization status in a StartTransaction.conf for this transaction", TenantId = tenantId },
                new KeyValue(){ Key ="StopTxnAlignedData", DefaultValue ="10", RW ="RW", Comment ="Clock-aligned periodic measurand(s) to be included in the TransactionData element of StopTransaction.req MeterValues.req PDU for every ClockAlignedDataInterval of the charging session", TenantId = tenantId },
                new KeyValue(){ Key ="StopTxnAlignedDataMaxLength", DefaultValue ="100", RW ="R", Comment ="Maximum number of items in a StopTxnAlignedData Configuration Key.", TenantId = tenantId },
                new KeyValue(){ Key ="StopTxnSampledData", DefaultValue ="10", RW ="RW", Comment ="Sampled measurands to be included in the TransactionData element of StopTransaction.req PDU, every MeterValueSampleInterval seconds from the start of the charging session", TenantId = tenantId },
                new KeyValue(){ Key ="StopTxnSampledDataMaxLength", DefaultValue ="100", RW ="R", Comment ="Maximum number of items in a StopTxnSampledData Configuration Key.", TenantId = tenantId },
                new KeyValue(){ Key ="SupportedFeatureProfiles", DefaultValue ="1", RW ="R", Comment ="A list of supported Feature Profiles. Possible profile identifiers: Core, FirmwareManagement, LocalAuthListManagement, Reservation, SmartCharging and RemoteTrigger.", TenantId = tenantId },
                new KeyValue(){ Key ="SupportedFeatureProfilesMaxLength", DefaultValue ="100", RW ="R", Comment ="Maximum number of items in a SupportedFeatureProfiles Configuration key", TenantId = tenantId },
                new KeyValue(){ Key ="TransactionMessageAttempts", DefaultValue ="3", RW ="RW", Comment ="How often the Charge Point should try to submit a transaction-related message when the Central System fails to process it.", TenantId = tenantId },
                new KeyValue(){ Key ="TransactionMessageRetryInterval", DefaultValue ="2", RW ="RW", Comment ="How long the Charge Point should wait before resubmitting a transactionrelated message that the Central System failed to process.", TenantId = tenantId },
                new KeyValue(){ Key ="UnlockConnectorOnEVSideDisconnect", DefaultValue ="TRUE", RW ="RW", Comment ="When set to true, the Charge Point SHALL unlock the cable on Charge Point side when the cable is unplugged at the EV.", TenantId = tenantId },
                new KeyValue(){ Key ="WebSocketPingInterval", DefaultValue ="10", RW ="RW", Comment ="Only relevant for websocket implementations. 0 disables client side websocket Ping/Pong. In this case there is either no ping/pong or the server initiates the ping and client responds with Pong. Positive values are interpreted as number of seconds between pings. Negative values are not allowed. ChangeConfiguration is expected to return a REJECTED result.", TenantId = tenantId },

                //KeyValues for LocalAuthListManagement OCPP 1.6 below (Lines 69-71)
                new KeyValue(){ Key ="LocalAuthListEnabled", DefaultValue ="FALSE", RW ="RW", Comment ="whether the Local Authorization List is enabled", TenantId = tenantId },
                new KeyValue(){ Key ="LocalAuthListMaxLength", DefaultValue ="100", RW ="R", Comment ="Maximum number of identifications that can be stored in the Local Authorization List", TenantId = tenantId },
                new KeyValue(){ Key ="SendLocalListMaxLength", DefaultValue ="100", RW ="R", Comment ="Maximum number of identifications that can be send in a single SendLocalList.req", TenantId = tenantId },

                //KeyValues for Reservation OCPP 1.6 below (Lines 72-72)
                new KeyValue(){ Key ="ReserveConnectorZeroSupported", DefaultValue ="FALSE", RW ="R", Comment ="If this configuration key is present and set to true: Charge Point support reservations on connector 0.", TenantId = tenantId },

                //KeyValues for Smart Charging OCPP 1.6 below (Lines 73-77)
                new KeyValue(){ Key ="ChargeProfileMaxStackLevel", DefaultValue ="1", RW ="R", Comment ="Max StackLevel of a ChargingProfile. The number defined also indicates the max allowed number of installed charging schedules per Charging Profile Purposes.", TenantId = tenantId },
                new KeyValue(){ Key ="ChargingScheduleAllowedChargingRateUnit", DefaultValue ="1", RW ="R", Comment ="A list of supported quantities for use in a ChargingSchedule. Allowed values: 'Current' and 'Power'", TenantId = tenantId },
                new KeyValue(){ Key ="ChargingScheduleMaxPeriods", DefaultValue ="1", RW ="R", Comment ="Maximum number of periods that may be defined per ChargingSchedule.", TenantId = tenantId },
                new KeyValue(){ Key ="ConnectorSwitch3to1PhaseSupported", DefaultValue ="1", RW ="R", Comment ="If defined and true, this Charge Point support switching from 3 to 1 phase during a charging session.", TenantId = tenantId },
                new KeyValue(){ Key ="MaxChargingProfilesInstalled", DefaultValue ="1", RW ="R", Comment ="Maximum number of Charging profiles installed at a time", TenantId = tenantId },
                
                //KeyValues for General OCPP 2.0 below (Lines 78-79)
                new KeyValue(){ Key ="ActiveNetworkProfile", DefaultValue ="1", RW ="RW", Comment ="Indicates configuration profile the station uses at that moment to connect to the network.", TenantId = tenantId },
                new KeyValue(){ Key ="AllowOfflineTxForUnknownId", DefaultValue ="1", RW ="R", Comment ="If this key exists, the Charge Point supports Unknown Offline Authorization. If this key reports a value of true, Unknown Offline Authorization is enabled.", TenantId = tenantId }
                
                //Add rest of OCPP 2.0 KeyValues here
            };

            ////Add KeyValues for OCPP 1.5. There are no features in OCPP 1.5, so we use a Dummy feature named Core15
            //for (int i = 0; i < 34; i++)
            //    KeyValues[i].OCPPFeatureId = _context.OCPPFeatures.Where(l => l.TenantId == tenantId && l.FeatureName == "Core15").SingleOrDefault().Id;

            ////Add KeyValues for OCPP 1.6 Core
            //for (int i = 34; i < 68; i++)
            //    KeyValues[i].OCPPFeatureId = _context.OCPPFeatures.Where(l => l.TenantId == tenantId && l.FeatureName == "Core").SingleOrDefault().Id;

            ////Add KeyValues for OCPP 1.6, Local authorization lists
            //for (int i = 68; i < 71; i++)
            //    KeyValues[i].OCPPFeatureId = _context.OCPPFeatures.Where(l => l.TenantId == tenantId && l.FeatureName == "LocalAuthListManagement").SingleOrDefault().Id;

            ////Add KeyValues for OCPP 1.6, Reservation
            //for (int i = 71; i < 72; i++)
            //    KeyValues[i].OCPPFeatureId = _context.OCPPFeatures.Where(l => l.TenantId == tenantId && l.FeatureName == "LocalAuthListManagement").SingleOrDefault().Id;

            ////Add KeyValues for OCPP 1.6, Smart Charging
            //for (int i = 72; i < 77; i++)
            //    KeyValues[i].OCPPFeatureId = _context.OCPPFeatures.Where(l => l.TenantId == tenantId && l.FeatureName == "SmartCharging").SingleOrDefault().Id;

            ////Add KeyValues for OCPP 2.0. General. These are just a couple of values for testing
            //for (int i = 77; i < 79; i++)
            //    KeyValues[i].OCPPFeatureId = _context.OCPPFeatures.Where(l => l.TenantId == tenantId && l.FeatureName == "General").SingleOrDefault().Id;

            return KeyValues;
        }
        public static List<Location> GetInitialLocations(int tenantId)
        {
            return new List<Location>
            {
                new Location() { LocationName = "Body", Comment="Measurement inside body of Charge Point (e.g. Temperature)", TenantId = tenantId },
                new Location() { LocationName = "Cable", Comment="Measurement taken from cable between EV and Charge Point", TenantId = tenantId },
                new Location() { LocationName = "EV", Comment="Measurement taken by EV", TenantId = tenantId },
                new Location() { LocationName = "Inlet", Comment="Measurement at network (\"grid\") inlet connection", TenantId = tenantId },
                new Location() { LocationName = "Outlet", Comment="Measurement at a Connector. Default value", TenantId = tenantId }
            };
        }
        public static List<Measurand> GetInitialMeasurands(int tenantId)
        {
            return new List<Measurand>
            {
                new Measurand() { MeasurandType = "Current.Export", Comment="Instantaneous current flow from EV", TenantId = tenantId },
                new Measurand() { MeasurandType = "Current.Import", Comment="Instantaneous current flow to EV", TenantId = tenantId },
                new Measurand() { MeasurandType = "Current.Offered", Comment="Maximum current offered to EV", TenantId = tenantId },
                new Measurand() { MeasurandType = "Energy.Active.Export.Register", Comment="Numerical value read from the \"active electrical energy\" (Wh or kWh) register of the (most authoritative) electrical meter measuring energy exported (to the grid).", TenantId = tenantId },
                new Measurand() { MeasurandType = "Energy.Active.Import.Register", Comment="Numerical value read from the \"active electrical energy\" (Wh or kWh) register of the (most authoritative) electrical meter measuring energy imported (from the grid supply).", TenantId = tenantId },
                new Measurand() { MeasurandType = "Energy.Reactive.Export.Register", Comment="Numerical value read from the \"reactive electrical energy\" (VARh or kVARh) register of the (most authoritative) electrical meter measuring energy exported (to the grid).", TenantId = tenantId },
                new Measurand() { MeasurandType = "Energy.Reactive.Import.Register", Comment="Numerical value read from the \"reactive electrical energy\" (VARh or kVARh) register of the (most authoritative) electrical meter measuring energy imported (from the grid supply).", TenantId = tenantId },
                new Measurand() { MeasurandType = "Energy.Active.Export.Interval", Comment="Absolute amount of \"active electrical energy\" (Wh or kWh) exported (to the grid) during an associated time \"interval\", specified by a Metervalues ReadingContext, and applicable interval duration configuration values (in seconds) for ClockAlignedDataInterval and TxnMeterValueSampleInterval.", TenantId = tenantId },
                new Measurand() { MeasurandType = "Energy.Active.Import.Interval", Comment="Absolute amount of \"active electrical energy\" (Wh or kWh) imported (from the grid supply) during an associated time \"interval\", specified by a Metervalues ReadingContext, and applicable interval duration configuration values (in seconds) for ClockAlignedDataInterval and TxnMeterValueSampleInterval.", TenantId = tenantId },
                new Measurand() { MeasurandType = "Energy.Reactive.Export.Interval", Comment="Absolute amount of \"reactive electrical energy\" (VARh or kVARh) exported (to the grid) during an associated time \"interval\", specified by a Metervalues ReadingContext, and applicable interval duration configuration values (in seconds) for ClockAlignedDataInterval and TxnMeterValueSampleInterval.", TenantId = tenantId },
                new Measurand() { MeasurandType = "Energy.Reactive.Import.Interval", Comment="Absolute amount of \"reactive electrical energy\" (VARh or kVARh) imported (from the grid supply) during an associated time \"interval\", specified by a Metervalues ReadingContext, and applicable interval duration configuration values (in seconds) for ClockAlignedDataInterval and TxnMeterValueSampleInterval.", TenantId = tenantId },
                new Measurand() { MeasurandType = "Frequency", Comment="Instantaneous reading of powerline frequency", TenantId = tenantId },
                new Measurand() { MeasurandType = "Power.Active.Export", Comment="Instantaneous active power exported by EV. (W or kW)", TenantId = tenantId },
                new Measurand() { MeasurandType = "Power.Active.Import", Comment="Instantaneous active power imported by EV. (W or kW)", TenantId = tenantId },
                new Measurand() { MeasurandType = "Power.Factor", Comment="Instantaneous power factor of total energy flow", TenantId = tenantId },
                new Measurand() { MeasurandType = "Power.Offered", Comment="Maximum power offered to EV", TenantId = tenantId },
                new Measurand() { MeasurandType = "Power.Reactive.Export", Comment="Instantaneous reactive power exported by EV. (var or kvar)", TenantId = tenantId },
                new Measurand() { MeasurandType = "Power.Reactive.Import", Comment="Instantaneous reactive power imported by EV. (var or kvar)", TenantId = tenantId },
                new Measurand() { MeasurandType = "RPM", Comment="Fan speed in RPM", TenantId = tenantId },
                new Measurand() { MeasurandType = "SoC", Comment="State of charge of charging vehicle in percentage", TenantId = tenantId },
                new Measurand() { MeasurandType = "Temperature", Comment="Temperature reading inside Charge Point.", TenantId = tenantId },
                new Measurand() { MeasurandType = "Voltage", Comment="Instantaneous AC RMS supply voltage", TenantId = tenantId },
            };
        }
        public static List<MeterValueType> GetInitialMeterValueTypes(int tenantId)
        {
            return new List<MeterValueType>
            {
                new MeterValueType() { Type = "Start", Comment="Meter value when charge transaction is started", TenantId = tenantId },
                new MeterValueType() { Type = "Stop", Comment="Meter value when charge transaction is stopped", TenantId = tenantId },
                new MeterValueType() { Type = "Intermediate", Comment="Meter value while in transaction", TenantId = tenantId }
            };
        }
        public static List<OCPPFeature> GetInitialOCPPFeatures(int tenantId)
        {
            List<OCPPFeature> Features = new List<OCPPFeature>
            {

                new OCPPFeature() { FeatureName="Core15", Comment="Basic Charge Point functionality for OCPP 1.5", TenantId = tenantId },
                new OCPPFeature() { FeatureName="Core", Comment="Basic Charge Point functionality comparable with OCPP 1.5 [OCPP1.5] without support for firmware updates, local authorization list management and reservations.", TenantId = tenantId },
                new OCPPFeature() { FeatureName="FirmwareManagement", Comment="Support for firmware update management and diagnostic log file download.", TenantId = tenantId },
                new OCPPFeature() { FeatureName="LocalAuthListManagement", Comment="Features to manage the local authorization list in Charge Points.", TenantId = tenantId },
                new OCPPFeature() { FeatureName="Reservation", Comment="Support for reservation of a Charge Point.", TenantId = tenantId },
                new OCPPFeature() { FeatureName="SmartCharging", Comment="Support for basic Smart Charging, for instance using control pilot.", TenantId = tenantId },
                new OCPPFeature() { FeatureName="RemoteTrigger", Comment="Support for remote triggering of Charge Point initiated messages.", TenantId = tenantId },
                new OCPPFeature() { FeatureName="General", Comment="TBD 2.0", TenantId = tenantId },
                new OCPPFeature() { FeatureName="Local Authorization List Management related", Comment="TBD 2.0", TenantId = tenantId },
                new OCPPFeature() { FeatureName="Reservation related", Comment="TBD 2.0", TenantId = tenantId },
                new OCPPFeature() { FeatureName="Smart Charging related", Comment="TBD 2.0", TenantId = tenantId },
                new OCPPFeature() { FeatureName="Tariff & Cost related", Comment="TBD 2.0", TenantId = tenantId },
                new OCPPFeature() { FeatureName="Display Message related", Comment="TBD 2.0", TenantId = tenantId }
            };

            //for (int i = 0; i < 1; i++)
            //    Features[i].OCPPVersionId = _context.OCPPVersions.Where(l => l.TenantId == tenantId && l.VersionName == "OCPP15").SingleOrDefault().Id;
            //for (int i = 1; i < 7; i++)
            //    Features[i].OCPPVersionId = _context.OCPPVersions.Where(l => l.TenantId == tenantId && l.VersionName == "OCPP16").SingleOrDefault().Id;
            //for (int i = 7; i < 13; i++)
            //    Features[i].OCPPVersionId = _context.OCPPVersions.Where(l => l.TenantId == tenantId && l.VersionName == "OCPP20").SingleOrDefault().Id;

            return Features;
        }
        public static List<OCPPMessage> GetInitialOCPPMessages(int tenantId)
        {
            List<OCPPMessage> OCPPMessages = new List<OCPPMessage>
            {
                //Messages for OCPP 1.5 below
                new OCPPMessage() { Message="Authorize.req", Comment="This contains the field definition of the Authorize.req PDU sent by the Charge Point to the Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="Authorize.conf", Comment="This contains the field definition of the Authorize.conf PDU sent by the Central System to the Charge Box as response to a Authorize.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="BootNotification.req", Comment="This contains the field definition of the BootNotification.req PDU sent by the Charge Box to the Central System. ", TenantId = tenantId },
                new OCPPMessage() { Message="BootNotification.conf", Comment="This contains the field definition of the BootNotification.conf PDU sent by the Central System to the Charge Box as response to a BootNotification.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="CancelReservation.req", Comment="This contains the field definition of the CancelReservation.req PDU sent by the Central System to the Charge Box.", TenantId = tenantId },
                new OCPPMessage() { Message="CancelReservation.conf", Comment="This contains the field definition of the CancelReservation.conf PDU sent by the Charge Box to the Central System as response to a CancelReservation.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="ChangeAvailability.req", Comment="This contains the field definition of the ChangeAvailability.req PDU sent by the Central System to the Charge Box.", TenantId = tenantId },
                new OCPPMessage() { Message="ChangeAvailability.conf", Comment="This contains the field definition of the ChangeAvailability.conf PDU return by Charge Box to Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="ChangeConfiguration.req", Comment="This contains the field definition of the ChangeConfiguration.req PDU sent by Central System to Charge Box. It is RECOMMENDED that the content and meaning of the 'key' and 'value' attributes is agreed upon between Charge Box and Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="ChangeConfiguration.conf", Comment="This contains the field definition of the ChangeConfiguration.conf PDU returned from Charge Box to Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="ClearCache.req", Comment="This contains the field definition of the ClearCache.req PDU sent by the Central System to the Charge Box.", TenantId = tenantId },
                new OCPPMessage() { Message="ClearCache.conf", Comment="This contains the field definition of the ClearCache.conf PDU sent by the Charge Box to the Charge Box as response to a ClearCache.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="DataTransfer.req", Comment="This contains the field definition of the DataTransfer.req PDU sent either by the Central System to the Charge Box or vice versa.", TenantId = tenantId },
                new OCPPMessage() { Message="DataTransfer.conf", Comment="This contains the field definition of the DataTransfer.conf PDU sent by the Charge Box to the Central System or vice versa as response to a DataTransfer.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="DiagnosticsStatusNotification.req", Comment="This contains the field definition of the DiagnosticsStatusNotification.req PDU sent by the Charge Box to the Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="DiagnosticsStatusNotification.conf", Comment="This contains the field definition of the DiagnosticsStatusNotification.conf PDU sent by the Central System to the Charge Box as response to a DiagnosticsStatusNotification.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="FirmwareStatusNotification.req", Comment="This contains the field definition of the FirmwareStatus.req PDU sent by the Charge Box to the Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="FirmwareStatusNotification.conf", Comment="This contains the field definition of the FirmwareStatus.conf PDU sent by the Central System to the Charge Box as response to a FirmwareStatus.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="GetConfiguration.req", Comment="This contains the field definition of the GetConfiguration.req PDU sent by the the Central System to the Charge Box.", TenantId = tenantId },
                new OCPPMessage() { Message="GetConfiguration.conf", Comment="This contains the field definition of the GetConfiguration.conf PDU sent by Charge Box the to the Central System in response to a GetConfiguration.req.", TenantId = tenantId },
                new OCPPMessage() { Message="GetDiagnostics.req", Comment="This contains the field definition of the GetDiagnostics.req PDU sent by the Central System to the Charge Box.", TenantId = tenantId },
                new OCPPMessage() { Message="GetDiagnostics.conf", Comment="This contains the field definition of the GetDiagnostics.conf PDU sent by the Charge Box to the Central System as response to a GetDiagnostics.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="GetLocalListVersion.req", Comment="This contains the field definition of the GetLocalListVersion.req PDU sent by the Central System to the Charge Box.", TenantId = tenantId },
                new OCPPMessage() { Message="GetLocalListVersion.conf", Comment="This contains the field definition of the GetLocalListVersion.conf PDU sent by the Charge Box to Central System in response to a GetLocalList.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="Heartbeat.req", Comment="This contains the field definition of the Hearbeat.req PDU sent by the Charge Box to the Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="Heartbeat.conf", Comment="This contains the field definition of the Heartbeat.conf PDU sent by the Central System to the Charge Box as response to a Heartbeat.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="MeterValues.req", Comment="This contains the field definition of the MeterValues.req PDU sent by the Charge Box to the Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="MeterValues.conf", Comment="This contains the field definition of the MeterValues.conf PDU sent by the Central System to the Charge Box as response to a MeterValues.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="RemoteStartTransaction.req", Comment="This contains the field definitions of the RemoteStartTransaction.req PDU sent to Charge Box by Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="RemoteStartTransaction.conf", Comment="This contains the field definitions of the RemoteStartTransaction.conf PDU sent from Charge Box to Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="RemoteStopTransaction.req", Comment="This contains the field definitions of the RemoteStopTransaction.req PDU sent to Charge Box by Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="RemoteStopTransaction.conf", Comment="This contains the field definitions of the RemoteStopTransaction.conf PDU sent from Charge Box to Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="ReserveNow.req", Comment="This contains the field definition of the ReserveNow.req PDU sent by the Central System to the Charge Box.", TenantId = tenantId },
                new OCPPMessage() { Message="ReserveNow.conf", Comment="This contains the field definition of the ReserveNow.conf PDU sent by the Charge Box to the Central System in response to a ReserveNow.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="Reset.req", Comment="This contains the field definition of the Reset.req PDU sent by the Central System to the Charge Box.", TenantId = tenantId },
                new OCPPMessage() { Message="Reset.conf", Comment="This contains the field definition of the Reset.conf PDU sent by the Charge Box to the Central System as response to a Reset.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="SendLocalList.req", Comment="This contains the field definition of the SendLocalList.req PDU sent by the Central System to the Charge Box.", TenantId = tenantId },
                new OCPPMessage() { Message="SendLocalList.conf", Comment="This contains the field definition of the SendLocalList.conf PDU sent by the Charge Box to the Central System as response to a SendLocalList.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="StartTransaction.req", Comment="This section contains the field definition of the StartTransaction.req PDU sent by the Charge Box to the Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="StartTransaction.conf", Comment="This contains the field definition of the StartTransaction.conf PDU sent by the Central System to the Charge Box as response to a StartTransaction.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="StatusNotification.req", Comment="This contains the field definition of the StatusNotification.req PDU sent by the Charge Box to the Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="StatusNotification.conf", Comment="This contains the field definition of the StatusNotifcation.conf PDU sent by the Central System to the Charge Box as response to an StatusNotification.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="StopTransaction.req", Comment="This contains the field definition of the StopTransaction.req PDU sent by the Charge Box to the Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="StopTransaction.conf", Comment="This contains the field definition of the StopTransaction.conf PDU sent by the Central System to the Charge Box as response to a StopTransaction.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="UnlockConnector.req", Comment="This contains the field definition of the UnlockConnector.req PDU sent by the Central System to the Charge Box.", TenantId = tenantId },
                new OCPPMessage() { Message="UnlockConnector.conf", Comment="This contains the field definition of the UnlockConnector.conf PDU sent by the Charge Box to the Central System as response to an UnlockConnector.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="UpdateFirmware.req", Comment="This contains the field definition of the UpdateFirmware.req PDU sent by the Central System to the Charge Box.", TenantId = tenantId },
                new OCPPMessage() { Message="UpdateFirmware.conf", Comment="This contains the field definition of the UpdateFirmware.conf PDU sent by the Charge Box to the Central System as response to a UpdateFirmware.req PDU.", TenantId = tenantId },

                //Messages for OCPP 1.6 below
                new OCPPMessage() { Message="Authorize.req", Comment="This contains the field definition of the Authorize.req PDU sent by the Charge Point to the Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="Authorize.conf", Comment="This contains the field definition of the Authorize.conf PDU sent by the Central System to the Charge Point in response to a Authorize.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="BootNotification.req", Comment="This contains the field definition of the BootNotification.req PDU sent by the Charge Point to the Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="BootNotification.conf", Comment="This contains the field definition of the BootNotification.conf PDU sent by the Central System to the Charge Point in response to a BootNotification.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="CancelReservation.req", Comment="This contains the field definition of the CancelReservation.req PDU sent by the Central System to the Charge Point.", TenantId = tenantId },
                new OCPPMessage() { Message="CancelReservation.conf", Comment="This contains the field definition of the CancelReservation.conf PDU sent by the Charge Point to the Central System in response to a CancelReservation.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="ChangeAvailability.req", Comment="This contains the field definition of the ChangeAvailability.req PDU sent by the Central System to the Charge Point.", TenantId = tenantId },
                new OCPPMessage() { Message="ChangeAvailability.conf", Comment="This contains the field definition of the ChangeAvailability.conf PDU return by Charge Point to Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="ChangeConfiguration.req", Comment="This contains the field definition of the ChangeConfiguration.req PDU sent by Central System to Charge Point. It is RECOMMENDED that the content and meaning of the 'key' and 'value' fields is agreed upon between Charge Point and Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="ChangeConfiguration.conf", Comment="This contains the field definition of the ChangeConfiguration.conf PDU returned from Charge Point to Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="ClearCache.req", Comment="This contains the field definition of the ClearCache.req PDU sent by the Central System to the Charge Point.", TenantId = tenantId },
                new OCPPMessage() { Message="ClearCache.conf", Comment="This contains the field definition of the ClearCache.conf PDU sent by the Charge Point to the Central System in response to a ClearCache.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="ClearChargingProfile.req", Comment="This contains the field definition of the ClearChargingProfile.req PDU sent by the Central System to the Charge Point.", TenantId = tenantId },
                new OCPPMessage() { Message="ClearChargingProfile.conf", Comment="This contains the field definition of the ClearChargingProfile.conf PDU sent by the Charge Point to the Central System in response to a ClearChargingProfile.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="DataTransfer.req", Comment="This contains the field definition of the DataTransfer.req PDU sent either by the Central System to the Charge Point or vice versa.", TenantId = tenantId },
                new OCPPMessage() { Message="DataTransfer.conf", Comment="This contains the field definition of the DataTransfer.conf PDU sent by the Charge Point to the Central System or vice versa in response to a DataTransfer.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="DiagnosticsStatusNotification.req", Comment="This contains the field definition of the DiagnosticsStatusNotification.req PDU sent by the Charge Point to the Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="DiagnosticsStatusNotification.conf", Comment="This contains the field definition of the DiagnosticsStatusNotification.conf PDU sent by the Central System to the Charge Point in response to a DiagnosticsStatusNotification.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="FirmwareStatusNotification.req", Comment="This contains the field definition of the FirmwareStatusNotifitacion.req PDU sent by the Charge Point to the Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="FirmwareStatusNotification.conf", Comment="This contains the field definition of the FirmwareStatusNotification.conf PDU sent by the Central System to the Charge Point in response to a FirmwareStatusNotification.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="GetCompositeSchedule.req", Comment="This contains the field definition of the GetCompositeSchedule.req PDU sent by the Central System to the Charge Point.", TenantId = tenantId },
                new OCPPMessage() { Message="GetCompositeSchedule.conf", Comment="This contains the field definition of the GetCompositeSchedule.conf PDU sent by the Charge Point to the Central System in response to a GetCompositeSchedule.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="GetConfiguration.req", Comment="This contains the field definition of the GetConfiguration.req PDU sent by the the Central System to the Charge Point.", TenantId = tenantId },
                new OCPPMessage() { Message="GetConfiguration.conf", Comment="This contains the field definition of the GetConfiguration.conf PDU sent by Charge Point the to the Central System in response to a GetConfiguration.req.", TenantId = tenantId },
                new OCPPMessage() { Message="GetDiagnostics.req", Comment="This contains the field definition of the GetDiagnostics.req PDU sent by the Central System to the Charge Point.", TenantId = tenantId },
                new OCPPMessage() { Message="GetDiagnostics.conf", Comment="This contains the field definition of the GetDiagnostics.conf PDU sent by the Charge Point to the Central System in response to a GetDiagnostics.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="GetLocalListVersion.req", Comment="This contains the field definition of the GetLocalListVersion.req PDU sent by the Central System to the Charge Point.", TenantId = tenantId },
                new OCPPMessage() { Message="GetLocalListVersion.conf", Comment="This contains the field definition of the GetLocalListVersion.conf PDU sent by the Charge Point to Central System in response to a GetLocalListVersion.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="Heartbeat.req", Comment="This contains the field definition of the Heartbeat.req PDU sent by the Charge Point to the Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="Heartbeat.conf", Comment="This contains the field definition of the Heartbeat.conf PDU sent by the Central System to the Charge Point in response to a Heartbeat.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="MeterValues.req", Comment="This contains the field definition of the MeterValues.req PDU sent by the Charge Point to the Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="MeterValues.conf", Comment="This contains the field definition of the MeterValues.conf PDU sent by the Central System to the Charge Point in response to a MeterValues.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="RemoteStartTransaction.req", Comment="This contains the field definitions of the RemoteStartTransaction.req PDU sent to Charge Point by Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="RemoteStartTransaction.conf", Comment="This contains the field definitions of the RemoteStartTransaction.conf PDU sent from Charge Point to Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="RemoteStopTransaction.req", Comment="This contains the field definitions of the RemoteStopTransaction.req PDU sent to Charge Point by Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="RemoteStopTransaction.conf", Comment="This contains the field definitions of the RemoteStopTransaction.conf PDU sent from Charge Point to Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="ReserveNow.req", Comment="This contains the field definition of the ReserveNow.req PDU sent by the Central System to the Charge Point.", TenantId = tenantId },
                new OCPPMessage() { Message="ReserveNow.conf", Comment="This contains the field definition of the ReserveNow.conf PDU sent by the Charge Point to the Central System in response to a ReserveNow.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="Reset.req", Comment="This contains the field definition of the Reset.req PDU sent by the Central System to the Charge Point.", TenantId = tenantId },
                new OCPPMessage() { Message="Reset.conf", Comment="This contains the field definition of the Reset.conf PDU sent by the Charge Point to the Central System in response to a Reset.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="SendLocalList.req", Comment="This contains the field definition of the SendLocalList.req PDU sent by the Central System to the Charge Point.", TenantId = tenantId },
                new OCPPMessage() { Message="SendLocalList.conf", Comment="This contains the field definition of the SendLocalList.conf PDU sent by the Charge Point to the Central System in response to a SendLocalList.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="SetChargingProfile.req", Comment="This contains the field definition of the SetChargingProfile.req PDU sent by the Central System to the Charge Point.", TenantId = tenantId },
                new OCPPMessage() { Message="SetChargingProfile.conf", Comment="This contains the field definition of the SetChargingProfile.conf PDU sent by the Charge Point to the Central System in response to a SetChargingProfile.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="StartTransaction.req", Comment="This section contains the field definition of the StartTransaction.req PDU sent by the Charge Point to the Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="StartTransaction.conf", Comment="This contains the field definition of the StartTransaction.conf PDU sent by the Central System to the Charge Point in response to a StartTransaction.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="StatusNotification.req", Comment="This contains the field definition of the StatusNotification.req PDU sent by the Charge Point to the Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="StatusNotification.conf", Comment="This contains the field definition of the StatusNotification.conf PDU sent by the Central System to the Charge Point in response to an StatusNotification.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="StopTransaction.req", Comment="This contains the field definition of the StopTransaction.req PDU sent by the Charge Point to the Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="StopTransaction.conf", Comment="This contains the field definition of the StopTransaction.conf PDU sent by the Central System to the Charge Point in response to a StopTransaction.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="TriggerMessage.req", Comment="This contains the field definition of the TriggerMessage.req PDU sent by the Central System to the Charge Point.", TenantId = tenantId },
                new OCPPMessage() { Message="TriggerMessage.conf", Comment="This contains the field definition of the TriggerMessage.conf PDU sent by the Charge Point to the Central System in response to a TriggerMessage.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="UnlockConnector.req", Comment="This contains the field definition of the UnlockConnector.req PDU sent by the Central System to the Charge Point.", TenantId = tenantId },
                new OCPPMessage() { Message="UnlockConnector.conf", Comment="This contains the field definition of the UnlockConnector.conf PDU sent by the Charge Point to the Central System in response to an UnlockConnector.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="UpdateFirmware.req", Comment="This contains the field definition of the UpdateFirmware.req PDU sent by the Central System to the Charge Point.", TenantId = tenantId },
                new OCPPMessage() { Message="UpdateFirmware.conf", Comment="This contains the field definition of the UpdateFirmware.conf PDU sent by the Charge Point to the Central System in response to a UpdateFirmware.req PDU.", TenantId = tenantId },

                //Messages for test of OCPP 2.0 below. 
                new OCPPMessage() { Message="Authorize.req", Comment="This contains the field definition of the Authorize.req PDU sent by the Charge Point to the Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="Authorize.conf", Comment="This contains the field definition of the Authorize.conf PDU sent by the Central System to the Charge Point in response to a Authorize.req PDU.", TenantId = tenantId },
                new OCPPMessage() { Message="BootNotification.req", Comment="This contains the field definition of the BootNotification.req PDU sent by the Charge Point to the Central System.", TenantId = tenantId },
                new OCPPMessage() { Message="BootNotification.conf", Comment="This contains the field definition of the BootNotification.conf PDU sent by the Central System to the Charge Point in response to a BootNotification.req PDU.", TenantId = tenantId }

            };

            //OCPP 1.5, add version to message
            //for (int i = 0; i < 48; i++)
            //    OCPPMessages[i].OCPPVersionId = _context.OCPPVersions.Where(l => l.TenantId == tenantId && l.VersionName == "OCPP15").SingleOrDefault().Id;

            ////OCPP 1.6, add version to message
            //for (int i = 48; i < 104; i++)
            //    OCPPMessages[i].OCPPVersionId = _context.OCPPVersions.Where(l => l.TenantId == tenantId && l.VersionName == "OCPP16").SingleOrDefault().Id;

            ////OCPP 2.0, add version to message
            //for (int i = 104; i < 108; i++)
            //    OCPPMessages[i].OCPPVersionId = _context.OCPPVersions.Where(l => l.TenantId == tenantId && l.VersionName == "OCPP20").SingleOrDefault().Id;
            return OCPPMessages;
        }
        public static List<OCPPStatus> GetInitialOCPPStatuses(int tenantId)
        {
            return new List<OCPPStatus>
            {
                new OCPPStatus() { Status = "Success", Comment = "OCPP call was a success" , TenantId = tenantId },
                new OCPPStatus() { Status = "Failed", Comment = "OCPP call failed for some reason" , TenantId = tenantId },
                new OCPPStatus(){ Status = "Pending", Comment = "Message was sent by Central System and waiting for response", TenantId = tenantId}

            };
        }
        public static List<OCPPTransport> GetInitialOCPPTransports(int tenantId)
        {
            return new List<OCPPTransport>
            {
                new OCPPTransport() { OCPPTransportName = "SOAP", Comment="Use SOAP for transport of OCPP PDU’s", TenantId = tenantId },
                new OCPPTransport() { OCPPTransportName = "JSON", Comment="Use JSON over WebSockets for transport of OCPP PDU’s", TenantId = tenantId },
            };
        }
        public static List<OCPPVersion> GetInitialOCPPVersions(int tenantId)
        {
            return new List<OCPPVersion>
            {
                new OCPPVersion() { VersionName = "None", Comment = "Used for Chargepoints in Topology that are not connected to Spine" , TenantId = tenantId },
                new OCPPVersion() { VersionName = "Custom", Comment = "Used for optional features not related to any OCPP Version" , TenantId = tenantId },
                new OCPPVersion() { VersionName = "OCPP14", Comment = "OCPP version 1.4" , TenantId = tenantId },
                new OCPPVersion() { VersionName = "OCPP15", Comment = "OCPP version 1.5" , TenantId = tenantId },
                new OCPPVersion() { VersionName = "OCPP16", Comment = "OCPP version 1.6" , TenantId = tenantId },
                new OCPPVersion() { VersionName = "OCPP20", Comment = "OCPP version 2.0" , TenantId = tenantId }

            };
        }
        public static List<Phase> GetInitialPhases(int tenantId)
        {
            return new List<Phase>
            {
                new Phase() { PhaseName = "L1", Comment="Measured on L1", TenantId = tenantId },
                new Phase() { PhaseName = "L2", Comment="Measured on L2", TenantId = tenantId },
                new Phase() { PhaseName = "L3", Comment="Measured on L3", TenantId = tenantId },
                new Phase() { PhaseName = "N", Comment="Measured on Neutral", TenantId = tenantId },
                new Phase() { PhaseName = "L1-N", Comment="Measured on L1 with respect to Neutral conductor", TenantId = tenantId },
                new Phase() { PhaseName = "L2-N", Comment="Measured on L2 with respect to Neutral conductor", TenantId = tenantId },
                new Phase() { PhaseName = "L3-N", Comment="Measured on L3 with respect to Neutral conductor", TenantId = tenantId },
                new Phase() { PhaseName = "L1-L2", Comment="Measured between L1 and L2", TenantId = tenantId },
                new Phase() { PhaseName = "L2-L3", Comment="Measured between L2 and L3", TenantId = tenantId },
                new Phase() { PhaseName = "L3-L1", Comment="Measured between L3 and L1", TenantId = tenantId },
            };
        }
        public static List<Power> GetInitialPowers(int tenantId)
        {
            return new List<Power>
            {
                new Power() { PowerName = "1-Phase", Comment="One phase", TenantId = tenantId },
                new Power() { PowerName = "3-Phase", Comment="Three phase", TenantId = tenantId },
                new Power() { PowerName = "DC", Comment="Direct current", TenantId = tenantId }
            };
        }
        public static List<Reason> GetInitialReasons(int tenantId)
        {
            return new List<Reason>
            {
                new Reason() { ReasonName = "DeAuthorized", Comment="The transaction was stopped because of the authorization status in a StartTransaction.conf", TenantId = tenantId },
                new Reason() { ReasonName = "EmergencyStop", Comment="Emergency stop button was used.", TenantId = tenantId },
                new Reason() { ReasonName = "EnergyLimitReached", Comment="EV charging session reached a locally enforced maximum energy transfer limit", TenantId = tenantId },
                new Reason() { ReasonName = "EVDisconnected", Comment="Disconnecting of cable, vehicle moved away from inductive charge unit.", TenantId = tenantId },
                new Reason() { ReasonName = "GroundFault", Comment="A GroundFault har occurred", TenantId = tenantId },
                new Reason() { ReasonName = "ImmediateReset", Comment="A Reset(Immediate) command was received.", TenantId = tenantId },
                new Reason() { ReasonName = "LawEnforcement", Comment="The transaction was stopped using a token with a LawEnforcementGroupId.", TenantId = tenantId },
                new Reason() { ReasonName = "Local", Comment="Stopped locally on request of the user at the Charge Point. This is a regular termination of a transaction. Examples: presenting an RFID tag, pressing a button to stop.", TenantId = tenantId },
                new Reason() { ReasonName = "LocalOutOfCredit", Comment="A local credit limit enforced through the Charge Point has been exceeded.", TenantId = tenantId },
                new Reason() { ReasonName = "Other", Comment="Any other reason.", TenantId = tenantId },
                new Reason() { ReasonName = "OvercurrentFault", Comment="A larger than intended electric current has occurred", TenantId = tenantId },
                new Reason() { ReasonName = "PowerLoss", Comment="Complete loss of power.", TenantId = tenantId },
                new Reason() { ReasonName = "PowerQuality", Comment="Quality of power too low, e.g. voltage too low/high, phase imbalance, etc.", TenantId = tenantId },
                new Reason() { ReasonName = "Reboot", Comment="A locally initiated reset/reboot occurred. (for instance watchdog kicked in)", TenantId = tenantId },
                new Reason() { ReasonName = "Remote", Comment="Stopped remotely on request of the user. This is a regular termination of a transaction. Examples: termination using a smartphone app, exceeding a (non local) prepaid credit.", TenantId = tenantId },
                new Reason() { ReasonName = "HardReset", Comment="A hard reset command was received.", TenantId = tenantId },
                new Reason() { ReasonName = "SoftReset", Comment="A soft reset command was received.", TenantId = tenantId },
                new Reason() { ReasonName = "UnlockCommand", Comment="Central System sent an Unlock Connector", TenantId = tenantId },
                new Reason() { ReasonName = "SOCLimitReached", Comment="Electric vehicle has reported reaching a locally enforced maximum battery State of Charge (SOC)", TenantId = tenantId },
                new Reason() { ReasonName = "StoppedByEV", Comment="The transaction was stopped by the EV", TenantId = tenantId },
                new Reason() { ReasonName = "TimeLimitReached", Comment="EV charging session reached a locally enforced time limit", TenantId = tenantId },
                new Reason() { ReasonName = "Timeout", Comment="EV not connected within timeout", TenantId = tenantId },
            };
        }
        public static List<RegistrationStatus> GetInitialRegistrationStatuses(int tenantId)
        {
            return new List<RegistrationStatus>
            {
                new RegistrationStatus() { Value = "Accepted", Comment="Charge point is accepted by Central System.", TenantId = tenantId },
                new RegistrationStatus() { Value = "Pending", Comment="Central System is not yet ready to accept the Charge Point. Central System may send messages to retrieve information or prepare the Charge Point.", TenantId = tenantId },
                new RegistrationStatus() { Value = "Rejected", Comment="Charge point is not accepted by Central System. This may happen when the Charge Point id is not known by Central System.", TenantId = tenantId },
            };
        }
        public static List<RemoteStartStopEventType> GetInitialRemoteStartStopEventTypes(int tenantId)
        {
            return new List<RemoteStartStopEventType>
            {
                new RemoteStartStopEventType() { EventType = "Start", Comment = "Start Remote Transaction", TenantId = tenantId },
                new RemoteStartStopEventType() { EventType = "Stop", Comment = "Stop Remote Transaction", TenantId = tenantId }
            };
        }
        public static List<RemoteStartStopStatus> GetInitialRemoteStartStopStatuses(int tenantId)
        {
            return new List<RemoteStartStopStatus>
            {
                new RemoteStartStopStatus() { Value = "Accepted", TenantId = tenantId },
                new RemoteStartStopStatus() { Value = "Rejected", TenantId = tenantId },
                new RemoteStartStopStatus() { Value = "Initiated", TenantId = tenantId }
            };

        }
        public static List<ReservationStatus> GetInitialReservationStatuses(int tenantId)
        {
            return new List<ReservationStatus>
            {
                new ReservationStatus() { Value = "Accepted", Comment="Reservation has been made.", TenantId = tenantId },
                new ReservationStatus() { Value = "Faulted", Comment="Reservation has not been made, because evse, connectors or specified connector are in a faulted state.", TenantId = tenantId },
                new ReservationStatus() { Value = "Occupied", Comment="Reservation has not been made. The evse or the specified connector is occupied.", TenantId = tenantId },
                new ReservationStatus() { Value = "Rejected", Comment="Reservation has not been made. Charge Point is not configured to accept reservations.", TenantId = tenantId },
                new ReservationStatus() { Value = "Unavailable", Comment="Reservation has not been made, because connectors or specified connector are in an unavailable state.", TenantId = tenantId },
            };
        }
        public static List<ResetStatus> GetInitialResetStatuses(int tenantId)
        {
            return new List<ResetStatus>
            {
                new ResetStatus() { ResetStatusValue = "Accepted", TenantId = tenantId },
                new ResetStatus() { ResetStatusValue = "Rejected", TenantId = tenantId },
                new ResetStatus() { ResetStatusValue = "Initiated", TenantId = tenantId }
            };
        }
        public static List<ResetType> GetInitialResetTypes(int tenantId)
        {
            return new List<ResetType>
            {
                new ResetType() { Type = "Hard", Comment = "Hard reboot", TenantId = tenantId },
                new ResetType() { Type = "Soft", Comment = "Soft reset", TenantId = tenantId }
            };
        }
        public static List<RuleRelation> GetInitialRuleConditionRelations(int tenantId)
        {
            return new List<RuleRelation>
            {
                new RuleRelation() { Value = "OR", Comment = "Rule Condition: OR", TenantId = tenantId },
                new RuleRelation() { Value = "AND", Comment = "Rule Condition: AND", TenantId = tenantId },
                new RuleRelation() { Value = "(", Comment = "Rule Condition: (", TenantId = tenantId },
                new RuleRelation() { Value = ")", Comment = "Rule Condition: )", TenantId = tenantId },
                new RuleRelation() { Value = ") OR", Comment = "Rule Condition: ) OR", TenantId = tenantId },
                new RuleRelation() { Value = ") AND", Comment = "Rule Condition: ) AND", TenantId = tenantId }
            };
        }
        public static List<TagTransactionType> GetInitialTagTransationTypes(int tenantId)
        {
            return new List<TagTransactionType>
            {
                new TagTransactionType() { Value = "Start", Comment="Indicates that a new Transaction has started", TenantId = tenantId },
                new TagTransactionType() { Value = "Stop", Comment="Indicates that a Transaction has stopped", TenantId = tenantId },
            };
        }
        public static List<TransactionStatus> GetInitialTransactionStatuses(int tenantId)
        {
            return new List<TransactionStatus>
            {
                new TransactionStatus() { Value = "Idle", Comment="Waiting for EV to charge", TenantId = tenantId },
                new TransactionStatus() { Value = "Charging", Comment="Providing energy to EV", TenantId = tenantId },
                new TransactionStatus() { Value = "Faulted", Comment="Error state", TenantId = tenantId },
                new TransactionStatus() { Value = "Completed", Comment="Transaction Completed", TenantId = tenantId },
            };
        }
        public static List<TransactionType> GetInitialTransactionTypes(int tenantId)
        {
            return new List<TransactionType>
            {
                new TransactionType() { Type = "UserTag", Comment="Transaction started by user and a Tag", TenantId = tenantId },
                new TransactionType() { Type = "RemoteTag", Comment="Transaction started by remote request from Onyx", TenantId = tenantId },
                new TransactionType() { Type = "AlwaysOpen", Comment="Charge point is always open and doesn't require authentication. User is unknown.", TenantId = tenantId }
            };
        }
        public static List<Unit> GetInitialUnits(int tenantId)
        {
            return new List<Unit>
            {
                new Unit() { UnitName = "Wh", Comment="Watt-hours (energy). Default.", TenantId = tenantId },
                new Unit() { UnitName = "kWh", Comment="kiloWatt-hours (energy).", TenantId = tenantId },
                new Unit() { UnitName = "varh", Comment="Var-hours (reactive energy).", TenantId = tenantId },
                new Unit() { UnitName = "kvarh", Comment="kilovar-hours (reactive energy).", TenantId = tenantId },
                new Unit() { UnitName = "W", Comment="Watts (power).", TenantId = tenantId },
                new Unit() { UnitName = "kW", Comment="kilowatts (power).", TenantId = tenantId },
                new Unit() { UnitName = "VA", Comment="VoltAmpere (apparent power).", TenantId = tenantId },
                new Unit() { UnitName = "kVA", Comment="kiloVolt Ampere (apparent power).", TenantId = tenantId },
                new Unit() { UnitName = "var", Comment="Vars (reactive power).", TenantId = tenantId },
                new Unit() { UnitName = "kvar", Comment="kilovars (reactive power).", TenantId = tenantId },
                new Unit() { UnitName = "A", Comment="Amperes (current).", TenantId = tenantId },
                new Unit() { UnitName = "V", Comment="Voltage (r.m.s. AC).", TenantId = tenantId },
                new Unit() { UnitName = "Celcius", Comment="Degrees (temperature).", TenantId = tenantId },
                new Unit() { UnitName = "Fahrenheit", Comment="Degrees (temperature).", TenantId = tenantId },
                new Unit() { UnitName = "K", Comment="Degrees Kelvin (temperature).", TenantId = tenantId },
                new Unit() { UnitName = "ASU", Comment="Arbitrary Strength Unit (Signal Strength)", TenantId = tenantId },
                new Unit() { UnitName = "dB", Comment="Decibel (for example Signal Strength)", TenantId = tenantId },
                new Unit() { UnitName = "dBm", Comment="Power relative to 1mW (10log(P/1mW)).", TenantId = tenantId },
                new Unit() { UnitName = "Deg", Comment="Degrees (angle/rotation)", TenantId = tenantId },
                new Unit() { UnitName = "Hz", Comment="Hertz (frequency)", TenantId = tenantId },
                new Unit() { UnitName = "kPa", Comment="kiloPascal (Pressure)", TenantId = tenantId },
                new Unit() { UnitName = "lx", Comment="Lux (Light Intensity)", TenantId = tenantId },
                new Unit() { UnitName = "ms2", Comment="ms2 (Acceleration)", TenantId = tenantId },
                new Unit() { UnitName = "N", Comment="Newtons (Force)", TenantId = tenantId },
                new Unit() { UnitName = "Percent", Comment="Percentage", TenantId = tenantId },
                new Unit() { UnitName = "RH", Comment="Relative Humidity%", TenantId = tenantId },
                new Unit() { UnitName = "RPM", Comment="Revolutions per Minute", TenantId = tenantId },
                new Unit() { UnitName = "s", Comment="Seconds (Time)", TenantId = tenantId },
            };
        }
        public static List<UnlockStatus> GetInitialUnlockStatuses(int tenantId)
        {
            return new List<UnlockStatus>
            {
                new UnlockStatus() { Value = "Unlocked", Comment="Connector has successfully been unlocked.", TenantId = tenantId },
                new UnlockStatus() { Value = "UnlockFailed", Comment="Failed to unlock the connector.", TenantId = tenantId },
                new UnlockStatus() { Value = "NotSupported", Comment="Charge Point has no connector lock.", TenantId = tenantId },
                new UnlockStatus() { Value = "Initiated", Comment="Unlock Status Initiated from UI.", TenantId = tenantId }
            };
        }
        public static List<UpdateStatus> GetInitialUpdateStatuses(int tenantId)
        {
            return new List<UpdateStatus>
            {
                new UpdateStatus() { Value = "Accepted", Comment="Local Authorization List successfully updated.", TenantId = tenantId },
                new UpdateStatus() { Value = "Failed", Comment="Failed to update the Local Authorization List.", TenantId = tenantId },
                new UpdateStatus() { Value = "NotSupported", Comment="Update of Local Authorization List is not supported by Charge Point.", TenantId = tenantId },
                new UpdateStatus() { Value = "VersionMismatch", Comment="Version number in the request for a differential update is less or equal then version number of current list.", TenantId = tenantId },
            };
        }
        public static List<UpdateType> GetInitialUpdateTypes(int tenantId)
        {
            return new List<UpdateType>
            {
                new UpdateType() { Value = "Differential", Comment="Indicates that the current Local Authorization List must be updated with the values in this message.", TenantId = tenantId },
                new UpdateType() { Value = "Full", Comment="Indicates that the current Local Authorization List must be replaced by the values in this message.", TenantId = tenantId },
            };
        }

    }
}
