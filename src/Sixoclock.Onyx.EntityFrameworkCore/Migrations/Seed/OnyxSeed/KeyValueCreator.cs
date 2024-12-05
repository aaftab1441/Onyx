using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class KeyValueCreator
    {
        public List<KeyValue> InitialKeyValues => GetInitialKeyValues();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<KeyValue> GetInitialKeyValues()
        {
            List<KeyValue> KeyValues = new List<KeyValue>
            {
                //KeyValues for OCPP 1.5 below (Lines 1-34)
                new KeyValue(){ Key ="AllowOfflineTxForUnknownId", DefaultValue ="FALSE", RW ="RW", Comment ="If this key exists, the Charge Point supports Unknown Offline Authorization. If this key reports a value of true, Unknown Offline Authorization is enabled.", TenantId = _tenantId },
                new KeyValue(){ Key ="AuthorizationCacheEnabled", DefaultValue ="FALSE", RW ="RW", Comment ="If this key exists, the Charge Point supports an Authorization Cache. If this key reports a value of true, the Authorization Cache is enabled.", TenantId = _tenantId },
                new KeyValue(){ Key ="AuthorizeRemoteTxRequests", DefaultValue ="FALSE", RW ="RW", Comment ="Whether a remote request to start a transaction in the form of a RemoteStartTransaction.req message should be authorized beforehand like a local action to start a transaction.", TenantId = _tenantId },
                new KeyValue(){ Key ="BlinkRepeat", DefaultValue ="2", RW ="RW", Comment ="Number of times to blink Charge Point lighting when signalling", TenantId = _tenantId },
                new KeyValue(){ Key ="ClockAlignedDataInterval", DefaultValue ="0", RW ="RW", Comment ="Size (in seconds) of the clock-aligned data interval. This is the size (in seconds) of the set of evenly spaced aggregation intervals per day, starting at 00:00:00 (midnight). For example, a value of 900 (15 minutes) indicates that every day should be broken into 96 15-minute intervals. When clock aligned data is being transmitted, the interval in question is identified by the start time and (optional) duration interval value, represented according to the ISO8601 standard. All \"per-period\" data (e.g. energy readings) should be accumulated (for \"flow\" type measurands such as energy), or averaged (for other values) across the entire interval (or partial interval, at the beginning or end of a charging session), and transmitted (if so enabled) at the end of each interval, bearing the interval start time timestamp. A value of \"0\" (numeric zero), by convention, is to be interpreted to mean that no clock-aligned data should be transmitted.", TenantId = _tenantId },
                new KeyValue(){ Key ="ConnectionTimeOut", DefaultValue ="20", RW ="RW", Comment ="Interval (from successful authorization) until incipient charging session is automatically canceled due to failure of EV user to (correctly) insert the charging cable connector(s) into the appropriate connector(s).", TenantId = _tenantId },
                new KeyValue(){ Key ="GetConfigurationMaxKeys", DefaultValue ="1", RW ="R", Comment ="Maximum number of requested configuration keys in a GetConfiguration.req PDU.", TenantId = _tenantId },
                new KeyValue(){ Key ="HeartbeatInterval", DefaultValue ="900", RW ="RW", Comment ="Interval of inactivity (no OCPP exchanges) with central system after which the Charge Point should send a Heartbeat.req PDU", TenantId = _tenantId },
                new KeyValue(){ Key ="LightIntensity", DefaultValue ="50", RW ="RW", Comment ="Percentage of maximum intensity at which to illuminate Charge Point lighting", TenantId = _tenantId },
                new KeyValue(){ Key ="LocalAuthorizeOffline", DefaultValue ="FALSE", RW ="RW", Comment ="whether the Charge Point, when offline, will start a transaction for locally authorized identifiers.", TenantId = _tenantId },
                new KeyValue(){ Key ="LocalPreAuthorize", DefaultValue ="FALSE", RW ="RW", Comment ="whether the Charge Point, when online, will start a transaction for locallyauthorized identifiers without waiting for or requesting an Authorize.conf from the Central System", TenantId = _tenantId },
                new KeyValue(){ Key ="MaxEnergyOnInvalidId", DefaultValue ="10000", RW ="RW", Comment ="Maximum energy in Wh delivered when an identifier is invalidated by the Central System after start of a transaction.", TenantId = _tenantId },
                new KeyValue(){ Key ="MeterValuesAlignedData", DefaultValue ="5", RW ="RW", Comment ="Clock-aligned measurand(s) to be included in a MeterValues.req PDU, every ClockAlignedDataInterval seconds", TenantId = _tenantId },
                new KeyValue(){ Key ="MeterValuesAlignedDataMaxLength", DefaultValue ="10", RW ="R", Comment ="Maximum number of items in a MeterValuesAlignedData Configuration Key.", TenantId = _tenantId },
                new KeyValue(){ Key ="MeterValuesSampledData", DefaultValue ="5", RW ="RW", Comment ="Sampled measurands to be included in a MeterValues.req PDU, every MeterValueSampleInterval seconds. Where applicable, the Measurand is combined with the optional phase; for instance: Voltage.L1 Default: \"Energy.Active.Import.Register\"", TenantId = _tenantId },
                new KeyValue(){ Key ="MeterValuesSampledDataMaxLength", DefaultValue ="10", RW ="R", Comment ="Maximum number of items in a MeterValuesSampledData Configuration Key.", TenantId = _tenantId },
                new KeyValue(){ Key ="MeterValueSampleInterval", DefaultValue ="0", RW ="RW", Comment ="Interval between sampling of metering (or other) data, intended to be transmitted by \"MeterValues\" PDUs. For charging session data (ConnectorId>0), samples are acquired and transmitted periodically at this interval from the start of the charging transaction. A value of \"0\" (numeric zero), by convention, is to be interpreted to mean that no sampled data should be transmitted.", TenantId = _tenantId },
                new KeyValue(){ Key ="MinimumStatusDuration", DefaultValue ="5", RW ="RW", Comment ="The minimum duration that a Charge Point or Connector status is stable before a StatusNotification.req PDU is sent to the Central System.", TenantId = _tenantId },
                new KeyValue(){ Key ="NumberOfConnectors", DefaultValue ="1", RW ="R", Comment ="The number of physical charging connectors of this Charge Point.", TenantId = _tenantId },
                new KeyValue(){ Key ="ResetRetries", DefaultValue ="2", RW ="RW", Comment ="Number of times to retry an unsuccessful reset of the Charge Point.", TenantId = _tenantId },
                new KeyValue(){ Key ="ConnectorPhaseRotation", DefaultValue ="1", RW ="RW", Comment ="The phase rotation per connector in respect to the connector’s energy meter (or if absent, the grid connection). Possible values per connector are: NotApplicable (for Single phase or DC Charge Points) Unknown (not (yet) known) RST (Standard Reference Phasing) RTS (Reversed Reference Phasing) SRT (Reversed 240 degree rotation) STR (Standard 120 degree rotation) TRS (Standard 240 degree rotation) TSR (Reversed 120 degree rotation) R can be identified as phase 1 (L1), S as phase 2 (L2), T as phase 3 (L3). If known, the Charge Point MAY also report the phase rotation between the grid connection and the main energymeter by using index number Zero (0). Values are reported in CSL, formatted: 0.RST, 1.RST, 2.RTS", TenantId = _tenantId },
                new KeyValue(){ Key ="ConnectorPhaseRotationMaxLength", DefaultValue ="1", RW ="R", Comment ="Maximum number of items in a ConnectorPhaseRotation Configuration Key.", TenantId = _tenantId },
                new KeyValue(){ Key ="StopTransactionOnEVSideDisconnect", DefaultValue ="TRUE", RW ="RW", Comment ="When set to true, the Charge Point SHALL administratively stop the transaction when the cable is unplugged from the EV.", TenantId = _tenantId },
                new KeyValue(){ Key ="StopTransactionOnInvalidId", DefaultValue ="FALSE", RW ="RW", Comment ="whether the Charge Point will stop an ongoing transaction when it receives a non- Accepted authorization status in a StartTransaction.conf for this transaction", TenantId = _tenantId },
                new KeyValue(){ Key ="StopTxnAlignedData", DefaultValue ="10", RW ="RW", Comment ="Clock-aligned periodic measurand(s) to be included in the TransactionData element of StopTransaction.req MeterValues.req PDU for every ClockAlignedDataInterval of the charging session", TenantId = _tenantId },
                new KeyValue(){ Key ="StopTxnAlignedDataMaxLength", DefaultValue ="100", RW ="R", Comment ="Maximum number of items in a StopTxnAlignedData Configuration Key.", TenantId = _tenantId },
                new KeyValue(){ Key ="StopTxnSampledData", DefaultValue ="10", RW ="RW", Comment ="Sampled measurands to be included in the TransactionData element of StopTransaction.req PDU, every MeterValueSampleInterval seconds from the start of the charging session", TenantId = _tenantId },
                new KeyValue(){ Key ="StopTxnSampledDataMaxLength", DefaultValue ="100", RW ="R", Comment ="Maximum number of items in a StopTxnSampledData Configuration Key.", TenantId = _tenantId },
                new KeyValue(){ Key ="SupportedFeatureProfiles", DefaultValue ="1", RW ="R", Comment ="A list of supported Feature Profiles. Possible profile identifiers: Core, FirmwareManagement, LocalAuthListManagement, Reservation, SmartCharging and RemoteTrigger.", TenantId = _tenantId },
                new KeyValue(){ Key ="SupportedFeatureProfilesMaxLength", DefaultValue ="100", RW ="R", Comment ="Maximum number of items in a SupportedFeatureProfiles Configuration key", TenantId = _tenantId },
                new KeyValue(){ Key ="TransactionMessageAttempts", DefaultValue ="3", RW ="RW", Comment ="How often the Charge Point should try to submit a transaction-related message when the Central System fails to process it.", TenantId = _tenantId },
                new KeyValue(){ Key ="TransactionMessageRetryInterval", DefaultValue ="2", RW ="RW", Comment ="How long the Charge Point should wait before resubmitting a transactionrelated message that the Central System failed to process.", TenantId = _tenantId },
                new KeyValue(){ Key ="UnlockConnectorOnEVSideDisconnect", DefaultValue ="TRUE", RW ="RW", Comment ="When set to true, the Charge Point SHALL unlock the cable on Charge Point side when the cable is unplugged at the EV.", TenantId = _tenantId },
                new KeyValue(){ Key ="WebSocketPingInterval", DefaultValue ="10", RW ="RW", Comment ="Only relevant for websocket implementations. 0 disables client side websocket Ping/Pong. In this case there is either no ping/pong or the server initiates the ping and client responds with Pong. Positive values are interpreted as number of seconds between pings. Negative values are not allowed. ChangeConfiguration is expected to return a REJECTED result.", TenantId = _tenantId },
                
                //KeyValues for Core OCPP 1.6 below (Lines 35-68)
                new KeyValue(){ Key ="AllowOfflineTxForUnknownId", DefaultValue ="FALSE", RW ="RW", Comment ="If this key exists, the Charge Point supports Unknown Offline Authorization. If this key reports a value of true, Unknown Offline Authorization is enabled.", TenantId = _tenantId },
                new KeyValue(){ Key ="AuthorizationCacheEnabled", DefaultValue ="FALSE", RW ="RW", Comment ="If this key exists, the Charge Point supports an Authorization Cache. If this key reports a value of true, the Authorization Cache is enabled.", TenantId = _tenantId },
                new KeyValue(){ Key ="AuthorizeRemoteTxRequests", DefaultValue ="FALSE", RW ="RW", Comment ="Whether a remote request to start a transaction in the form of a RemoteStartTransaction.req message should be authorized beforehand like a local action to start a transaction.", TenantId = _tenantId },
                new KeyValue(){ Key ="BlinkRepeat", DefaultValue ="2", RW ="RW", Comment ="Number of times to blink Charge Point lighting when signalling", TenantId = _tenantId },
                new KeyValue(){ Key ="ClockAlignedDataInterval", DefaultValue ="0", RW ="RW", Comment ="Size (in seconds) of the clock-aligned data interval. This is the size (in seconds) of the set of evenly spaced aggregation intervals per day, starting at 00:00:00 (midnight). For example, a value of 900 (15 minutes) indicates that every day should be broken into 96 15-minute intervals. When clock aligned data is being transmitted, the interval in question is identified by the start time and (optional) duration interval value, represented according to the ISO8601 standard. All \"per-period\" data (e.g. energy readings) should be accumulated (for \"flow\" type measurands such as energy), or averaged (for other values) across the entire interval (or partial interval, at the beginning or end of a charging session), and transmitted (if so enabled) at the end of each interval, bearing the interval start time timestamp. A value of \"0\" (numeric zero), by convention, is to be interpreted to mean that no clock-aligned data should be transmitted.", TenantId = _tenantId },
                new KeyValue(){ Key ="ConnectionTimeOut", DefaultValue ="20", RW ="RW", Comment ="Interval (from successful authorization) until incipient charging session is automatically canceled due to failure of EV user to (correctly) insert the charging cable connector(s) into the appropriate connector(s).", TenantId = _tenantId },
                new KeyValue(){ Key ="GetConfigurationMaxKeys", DefaultValue ="1", RW ="R", Comment ="Maximum number of requested configuration keys in a GetConfiguration.req PDU.", TenantId = _tenantId },
                new KeyValue(){ Key ="HeartbeatInterval", DefaultValue ="900", RW ="RW", Comment ="Interval of inactivity (no OCPP exchanges) with central system after which the Charge Point should send a Heartbeat.req PDU", TenantId = _tenantId },
                new KeyValue(){ Key ="LightIntensity", DefaultValue ="50", RW ="RW", Comment ="Percentage of maximum intensity at which to illuminate Charge Point lighting", TenantId = _tenantId },
                new KeyValue(){ Key ="LocalAuthorizeOffline", DefaultValue ="FALSE", RW ="RW", Comment ="whether the Charge Point, when offline, will start a transaction for locally authorized identifiers.", TenantId = _tenantId },
                new KeyValue(){ Key ="LocalPreAuthorize", DefaultValue ="FALSE", RW ="RW", Comment ="whether the Charge Point, when online, will start a transaction for locallyauthorized identifiers without waiting for or requesting an Authorize.conf from the Central System", TenantId = _tenantId },
                new KeyValue(){ Key ="MaxEnergyOnInvalidId", DefaultValue ="10000", RW ="RW", Comment ="Maximum energy in Wh delivered when an identifier is invalidated by the Central System after start of a transaction.", TenantId = _tenantId },
                new KeyValue(){ Key ="MeterValuesAlignedData", DefaultValue ="5", RW ="RW", Comment ="Clock-aligned measurand(s) to be included in a MeterValues.req PDU, every ClockAlignedDataInterval seconds", TenantId = _tenantId },
                new KeyValue(){ Key ="MeterValuesAlignedDataMaxLength", DefaultValue ="10", RW ="R", Comment ="Maximum number of items in a MeterValuesAlignedData Configuration Key.", TenantId = _tenantId },
                new KeyValue(){ Key ="MeterValuesSampledData", DefaultValue ="5", RW ="RW", Comment ="Sampled measurands to be included in a MeterValues.req PDU, every MeterValueSampleInterval seconds. Where applicable, the Measurand is combined with the optional phase; for instance: Voltage.L1 Default: \"Energy.Active.Import.Register\"", TenantId = _tenantId },
                new KeyValue(){ Key ="MeterValuesSampledDataMaxLength", DefaultValue ="10", RW ="R", Comment ="Maximum number of items in a MeterValuesSampledData Configuration Key.", TenantId = _tenantId },
                new KeyValue(){ Key ="MeterValueSampleInterval", DefaultValue ="0", RW ="RW", Comment ="Interval between sampling of metering (or other) data, intended to be transmitted by \"MeterValues\" PDUs. For charging session data (ConnectorId>0), samples are acquired and transmitted periodically at this interval from the start of the charging transaction. A value of \"0\" (numeric zero), by convention, is to be interpreted to mean that no sampled data should be transmitted.", TenantId = _tenantId },
                new KeyValue(){ Key ="MinimumStatusDuration", DefaultValue ="5", RW ="RW", Comment ="The minimum duration that a Charge Point or Connector status is stable before a StatusNotification.req PDU is sent to the Central System.", TenantId = _tenantId },
                new KeyValue(){ Key ="NumberOfConnectors", DefaultValue ="1", RW ="R", Comment ="The number of physical charging connectors of this Charge Point.", TenantId = _tenantId },
                new KeyValue(){ Key ="ResetRetries", DefaultValue ="2", RW ="RW", Comment ="Number of times to retry an unsuccessful reset of the Charge Point.", TenantId = _tenantId },
                new KeyValue(){ Key ="ConnectorPhaseRotation", DefaultValue ="1", RW ="RW", Comment ="The phase rotation per connector in respect to the connector’s energy meter (or if absent, the grid connection). Possible values per connector are: NotApplicable (for Single phase or DC Charge Points) Unknown (not (yet) known) RST (Standard Reference Phasing) RTS (Reversed Reference Phasing) SRT (Reversed 240 degree rotation) STR (Standard 120 degree rotation) TRS (Standard 240 degree rotation) TSR (Reversed 120 degree rotation) R can be identified as phase 1 (L1), S as phase 2 (L2), T as phase 3 (L3). If known, the Charge Point MAY also report the phase rotation between the grid connection and the main energymeter by using index number Zero (0). Values are reported in CSL, formatted: 0.RST, 1.RST, 2.RTS", TenantId = _tenantId },
                new KeyValue(){ Key ="ConnectorPhaseRotationMaxLength", DefaultValue ="1", RW ="R", Comment ="Maximum number of items in a ConnectorPhaseRotation Configuration Key.", TenantId = _tenantId },
                new KeyValue(){ Key ="StopTransactionOnEVSideDisconnect", DefaultValue ="TRUE", RW ="RW", Comment ="When set to true, the Charge Point SHALL administratively stop the transaction when the cable is unplugged from the EV.", TenantId = _tenantId },
                new KeyValue(){ Key ="StopTransactionOnInvalidId", DefaultValue ="FALSE", RW ="RW", Comment ="whether the Charge Point will stop an ongoing transaction when it receives a non- Accepted authorization status in a StartTransaction.conf for this transaction", TenantId = _tenantId },
                new KeyValue(){ Key ="StopTxnAlignedData", DefaultValue ="10", RW ="RW", Comment ="Clock-aligned periodic measurand(s) to be included in the TransactionData element of StopTransaction.req MeterValues.req PDU for every ClockAlignedDataInterval of the charging session", TenantId = _tenantId },
                new KeyValue(){ Key ="StopTxnAlignedDataMaxLength", DefaultValue ="100", RW ="R", Comment ="Maximum number of items in a StopTxnAlignedData Configuration Key.", TenantId = _tenantId },
                new KeyValue(){ Key ="StopTxnSampledData", DefaultValue ="10", RW ="RW", Comment ="Sampled measurands to be included in the TransactionData element of StopTransaction.req PDU, every MeterValueSampleInterval seconds from the start of the charging session", TenantId = _tenantId },
                new KeyValue(){ Key ="StopTxnSampledDataMaxLength", DefaultValue ="100", RW ="R", Comment ="Maximum number of items in a StopTxnSampledData Configuration Key.", TenantId = _tenantId },
                new KeyValue(){ Key ="SupportedFeatureProfiles", DefaultValue ="1", RW ="R", Comment ="A list of supported Feature Profiles. Possible profile identifiers: Core, FirmwareManagement, LocalAuthListManagement, Reservation, SmartCharging and RemoteTrigger.", TenantId = _tenantId },
                new KeyValue(){ Key ="SupportedFeatureProfilesMaxLength", DefaultValue ="100", RW ="R", Comment ="Maximum number of items in a SupportedFeatureProfiles Configuration key", TenantId = _tenantId },
                new KeyValue(){ Key ="TransactionMessageAttempts", DefaultValue ="3", RW ="RW", Comment ="How often the Charge Point should try to submit a transaction-related message when the Central System fails to process it.", TenantId = _tenantId },
                new KeyValue(){ Key ="TransactionMessageRetryInterval", DefaultValue ="2", RW ="RW", Comment ="How long the Charge Point should wait before resubmitting a transactionrelated message that the Central System failed to process.", TenantId = _tenantId },
                new KeyValue(){ Key ="UnlockConnectorOnEVSideDisconnect", DefaultValue ="TRUE", RW ="RW", Comment ="When set to true, the Charge Point SHALL unlock the cable on Charge Point side when the cable is unplugged at the EV.", TenantId = _tenantId },
                new KeyValue(){ Key ="WebSocketPingInterval", DefaultValue ="10", RW ="RW", Comment ="Only relevant for websocket implementations. 0 disables client side websocket Ping/Pong. In this case there is either no ping/pong or the server initiates the ping and client responds with Pong. Positive values are interpreted as number of seconds between pings. Negative values are not allowed. ChangeConfiguration is expected to return a REJECTED result.", TenantId = _tenantId },

                //KeyValues for LocalAuthListManagement OCPP 1.6 below (Lines 69-71)
                new KeyValue(){ Key ="LocalAuthListEnabled", DefaultValue ="FALSE", RW ="RW", Comment ="whether the Local Authorization List is enabled", TenantId = _tenantId },
                new KeyValue(){ Key ="LocalAuthListMaxLength", DefaultValue ="100", RW ="R", Comment ="Maximum number of identifications that can be stored in the Local Authorization List", TenantId = _tenantId },
                new KeyValue(){ Key ="SendLocalListMaxLength", DefaultValue ="100", RW ="R", Comment ="Maximum number of identifications that can be send in a single SendLocalList.req", TenantId = _tenantId },

                //KeyValues for Reservation OCPP 1.6 below (Lines 72-72)
                new KeyValue(){ Key ="ReserveConnectorZeroSupported", DefaultValue ="FALSE", RW ="R", Comment ="If this configuration key is present and set to true: Charge Point support reservations on connector 0.", TenantId = _tenantId },

                //KeyValues for Smart Charging OCPP 1.6 below (Lines 73-77)
                new KeyValue(){ Key ="ChargeProfileMaxStackLevel", DefaultValue ="1", RW ="R", Comment ="Max StackLevel of a ChargingProfile. The number defined also indicates the max allowed number of installed charging schedules per Charging Profile Purposes.", TenantId = _tenantId },
                new KeyValue(){ Key ="ChargingScheduleAllowedChargingRateUnit", DefaultValue ="1", RW ="R", Comment ="A list of supported quantities for use in a ChargingSchedule. Allowed values: 'Current' and 'Power'", TenantId = _tenantId },
                new KeyValue(){ Key ="ChargingScheduleMaxPeriods", DefaultValue ="1", RW ="R", Comment ="Maximum number of periods that may be defined per ChargingSchedule.", TenantId = _tenantId },
                new KeyValue(){ Key ="ConnectorSwitch3to1PhaseSupported", DefaultValue ="1", RW ="R", Comment ="If defined and true, this Charge Point support switching from 3 to 1 phase during a charging session.", TenantId = _tenantId },
                new KeyValue(){ Key ="MaxChargingProfilesInstalled", DefaultValue ="1", RW ="R", Comment ="Maximum number of Charging profiles installed at a time", TenantId = _tenantId },
                
                //KeyValues for General OCPP 2.0 below (Lines 78-79)
                new KeyValue(){ Key ="ActiveNetworkProfile", DefaultValue ="1", RW ="RW", Comment ="Indicates configuration profile the station uses at that moment to connect to the network.", TenantId = _tenantId },
                new KeyValue(){ Key ="AllowOfflineTxForUnknownId", DefaultValue ="1", RW ="R", Comment ="If this key exists, the Charge Point supports Unknown Offline Authorization. If this key reports a value of true, Unknown Offline Authorization is enabled.", TenantId = _tenantId }
                
                //Add rest of OCPP 2.0 KeyValues here
            };

            //Add KeyValues for OCPP 1.5. There are no features in OCPP 1.5, so we use a Dummy feature named Core15
            for (int i = 0; i < 34; i++)
                KeyValues[i].OCPPFeatureId = _context.OCPPFeatures.Where(l => l.TenantId == _tenantId && l.FeatureName == "Core15").SingleOrDefault().Id;

            //Add KeyValues for OCPP 1.6 Core
            for (int i = 34; i < 68; i++)
                KeyValues[i].OCPPFeatureId = _context.OCPPFeatures.Where(l => l.TenantId == _tenantId && l.FeatureName == "Core").SingleOrDefault().Id;

            //Add KeyValues for OCPP 1.6, Local authorization lists
            for (int i = 68; i < 71; i++)
                KeyValues[i].OCPPFeatureId = _context.OCPPFeatures.Where(l => l.TenantId == _tenantId && l.FeatureName == "LocalAuthListManagement").SingleOrDefault().Id;

            //Add KeyValues for OCPP 1.6, Reservation
            for (int i = 71; i < 72; i++)
                KeyValues[i].OCPPFeatureId = _context.OCPPFeatures.Where(l => l.TenantId == _tenantId && l.FeatureName == "LocalAuthListManagement").SingleOrDefault().Id;

            //Add KeyValues for OCPP 1.6, Smart Charging
            for (int i = 72; i < 77; i++)
                KeyValues[i].OCPPFeatureId = _context.OCPPFeatures.Where(l => l.TenantId == _tenantId && l.FeatureName == "SmartCharging").SingleOrDefault().Id;

            //Add KeyValues for OCPP 2.0. General. These are just a couple of values for testing
            for (int i = 77; i < 79; i++)
                KeyValues[i].OCPPFeatureId = _context.OCPPFeatures.Where(l => l.TenantId == _tenantId && l.FeatureName == "General").SingleOrDefault().Id;

            return KeyValues;
        }

        public KeyValueCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateKeyValuees();
        }

        private void CreateKeyValuees()
        {
            foreach (var keyValue in InitialKeyValues)
            {
                AddKeyValueIfNotExists(keyValue);
            }
        }

        private void AddKeyValueIfNotExists(KeyValue keyValue)
        {
            if (_context.KeyValues.Any(l => l.TenantId == _tenantId && l.Key == keyValue.Key && l.OCPPFeatureId == keyValue.OCPPFeatureId))
            {
                return;
            }

            _context.KeyValues.Add(keyValue);

            _context.SaveChanges();
        }
    }
}
