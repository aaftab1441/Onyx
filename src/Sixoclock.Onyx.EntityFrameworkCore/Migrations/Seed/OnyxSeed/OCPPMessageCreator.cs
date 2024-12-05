using Sixoclock.Onyx.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class OCPPMessageCreator
    {
        public List<OCPPMessage> InitialOCPPMessages => GetInitialOCPPMessages();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<OCPPMessage> GetInitialOCPPMessages()
        {
            List<OCPPMessage> OCPPMessages = new List<OCPPMessage>
            {
                //Messages for OCPP 1.5 below
                new OCPPMessage() { Message="Authorize.req", Comment="This contains the field definition of the Authorize.req PDU sent by the Charge Point to the Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="Authorize.conf", Comment="This contains the field definition of the Authorize.conf PDU sent by the Central System to the Charge Box as response to a Authorize.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="BootNotification.req", Comment="This contains the field definition of the BootNotification.req PDU sent by the Charge Box to the Central System. ", TenantId = _tenantId },
                new OCPPMessage() { Message="BootNotification.conf", Comment="This contains the field definition of the BootNotification.conf PDU sent by the Central System to the Charge Box as response to a BootNotification.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="CancelReservation.req", Comment="This contains the field definition of the CancelReservation.req PDU sent by the Central System to the Charge Box.", TenantId = _tenantId },
                new OCPPMessage() { Message="CancelReservation.conf", Comment="This contains the field definition of the CancelReservation.conf PDU sent by the Charge Box to the Central System as response to a CancelReservation.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="ChangeAvailability.req", Comment="This contains the field definition of the ChangeAvailability.req PDU sent by the Central System to the Charge Box.", TenantId = _tenantId },
                new OCPPMessage() { Message="ChangeAvailability.conf", Comment="This contains the field definition of the ChangeAvailability.conf PDU return by Charge Box to Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="ChangeConfiguration.req", Comment="This contains the field definition of the ChangeConfiguration.req PDU sent by Central System to Charge Box. It is RECOMMENDED that the content and meaning of the 'key' and 'value' attributes is agreed upon between Charge Box and Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="ChangeConfiguration.conf", Comment="This contains the field definition of the ChangeConfiguration.conf PDU returned from Charge Box to Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="ClearCache.req", Comment="This contains the field definition of the ClearCache.req PDU sent by the Central System to the Charge Box.", TenantId = _tenantId },
                new OCPPMessage() { Message="ClearCache.conf", Comment="This contains the field definition of the ClearCache.conf PDU sent by the Charge Box to the Charge Box as response to a ClearCache.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="DataTransfer.req", Comment="This contains the field definition of the DataTransfer.req PDU sent either by the Central System to the Charge Box or vice versa.", TenantId = _tenantId },
                new OCPPMessage() { Message="DataTransfer.conf", Comment="This contains the field definition of the DataTransfer.conf PDU sent by the Charge Box to the Central System or vice versa as response to a DataTransfer.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="DiagnosticsStatusNotification.req", Comment="This contains the field definition of the DiagnosticsStatusNotification.req PDU sent by the Charge Box to the Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="DiagnosticsStatusNotification.conf", Comment="This contains the field definition of the DiagnosticsStatusNotification.conf PDU sent by the Central System to the Charge Box as response to a DiagnosticsStatusNotification.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="FirmwareStatusNotification.req", Comment="This contains the field definition of the FirmwareStatus.req PDU sent by the Charge Box to the Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="FirmwareStatusNotification.conf", Comment="This contains the field definition of the FirmwareStatus.conf PDU sent by the Central System to the Charge Box as response to a FirmwareStatus.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="GetConfiguration.req", Comment="This contains the field definition of the GetConfiguration.req PDU sent by the the Central System to the Charge Box.", TenantId = _tenantId },
                new OCPPMessage() { Message="GetConfiguration.conf", Comment="This contains the field definition of the GetConfiguration.conf PDU sent by Charge Box the to the Central System in response to a GetConfiguration.req.", TenantId = _tenantId },
                new OCPPMessage() { Message="GetDiagnostics.req", Comment="This contains the field definition of the GetDiagnostics.req PDU sent by the Central System to the Charge Box.", TenantId = _tenantId },
                new OCPPMessage() { Message="GetDiagnostics.conf", Comment="This contains the field definition of the GetDiagnostics.conf PDU sent by the Charge Box to the Central System as response to a GetDiagnostics.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="GetLocalListVersion.req", Comment="This contains the field definition of the GetLocalListVersion.req PDU sent by the Central System to the Charge Box.", TenantId = _tenantId },
                new OCPPMessage() { Message="GetLocalListVersion.conf", Comment="This contains the field definition of the GetLocalListVersion.conf PDU sent by the Charge Box to Central System in response to a GetLocalList.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="Heartbeat.req", Comment="This contains the field definition of the Hearbeat.req PDU sent by the Charge Box to the Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="Heartbeat.conf", Comment="This contains the field definition of the Heartbeat.conf PDU sent by the Central System to the Charge Box as response to a Heartbeat.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="MeterValues.req", Comment="This contains the field definition of the MeterValues.req PDU sent by the Charge Box to the Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="MeterValues.conf", Comment="This contains the field definition of the MeterValues.conf PDU sent by the Central System to the Charge Box as response to a MeterValues.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="RemoteStartTransaction.req", Comment="This contains the field definitions of the RemoteStartTransaction.req PDU sent to Charge Box by Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="RemoteStartTransaction.conf", Comment="This contains the field definitions of the RemoteStartTransaction.conf PDU sent from Charge Box to Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="RemoteStopTransaction.req", Comment="This contains the field definitions of the RemoteStopTransaction.req PDU sent to Charge Box by Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="RemoteStopTransaction.conf", Comment="This contains the field definitions of the RemoteStopTransaction.conf PDU sent from Charge Box to Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="ReserveNow.req", Comment="This contains the field definition of the ReserveNow.req PDU sent by the Central System to the Charge Box.", TenantId = _tenantId },
                new OCPPMessage() { Message="ReserveNow.conf", Comment="This contains the field definition of the ReserveNow.conf PDU sent by the Charge Box to the Central System in response to a ReserveNow.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="Reset.req", Comment="This contains the field definition of the Reset.req PDU sent by the Central System to the Charge Box.", TenantId = _tenantId },
                new OCPPMessage() { Message="Reset.conf", Comment="This contains the field definition of the Reset.conf PDU sent by the Charge Box to the Central System as response to a Reset.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="SendLocalList.req", Comment="This contains the field definition of the SendLocalList.req PDU sent by the Central System to the Charge Box.", TenantId = _tenantId },
                new OCPPMessage() { Message="SendLocalList.conf", Comment="This contains the field definition of the SendLocalList.conf PDU sent by the Charge Box to the Central System as response to a SendLocalList.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="StartTransaction.req", Comment="This section contains the field definition of the StartTransaction.req PDU sent by the Charge Box to the Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="StartTransaction.conf", Comment="This contains the field definition of the StartTransaction.conf PDU sent by the Central System to the Charge Box as response to a StartTransaction.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="StatusNotification.req", Comment="This contains the field definition of the StatusNotification.req PDU sent by the Charge Box to the Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="StatusNotification.conf", Comment="This contains the field definition of the StatusNotifcation.conf PDU sent by the Central System to the Charge Box as response to an StatusNotification.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="StopTransaction.req", Comment="This contains the field definition of the StopTransaction.req PDU sent by the Charge Box to the Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="StopTransaction.conf", Comment="This contains the field definition of the StopTransaction.conf PDU sent by the Central System to the Charge Box as response to a StopTransaction.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="UnlockConnector.req", Comment="This contains the field definition of the UnlockConnector.req PDU sent by the Central System to the Charge Box.", TenantId = _tenantId },
                new OCPPMessage() { Message="UnlockConnector.conf", Comment="This contains the field definition of the UnlockConnector.conf PDU sent by the Charge Box to the Central System as response to an UnlockConnector.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="UpdateFirmware.req", Comment="This contains the field definition of the UpdateFirmware.req PDU sent by the Central System to the Charge Box.", TenantId = _tenantId },
                new OCPPMessage() { Message="UpdateFirmware.conf", Comment="This contains the field definition of the UpdateFirmware.conf PDU sent by the Charge Box to the Central System as response to a UpdateFirmware.req PDU.", TenantId = _tenantId },

                //Messages for OCPP 1.6 below
                new OCPPMessage() { Message="Authorize.req", Comment="This contains the field definition of the Authorize.req PDU sent by the Charge Point to the Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="Authorize.conf", Comment="This contains the field definition of the Authorize.conf PDU sent by the Central System to the Charge Point in response to a Authorize.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="BootNotification.req", Comment="This contains the field definition of the BootNotification.req PDU sent by the Charge Point to the Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="BootNotification.conf", Comment="This contains the field definition of the BootNotification.conf PDU sent by the Central System to the Charge Point in response to a BootNotification.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="CancelReservation.req", Comment="This contains the field definition of the CancelReservation.req PDU sent by the Central System to the Charge Point.", TenantId = _tenantId },
                new OCPPMessage() { Message="CancelReservation.conf", Comment="This contains the field definition of the CancelReservation.conf PDU sent by the Charge Point to the Central System in response to a CancelReservation.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="ChangeAvailability.req", Comment="This contains the field definition of the ChangeAvailability.req PDU sent by the Central System to the Charge Point.", TenantId = _tenantId },
                new OCPPMessage() { Message="ChangeAvailability.conf", Comment="This contains the field definition of the ChangeAvailability.conf PDU return by Charge Point to Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="ChangeConfiguration.req", Comment="This contains the field definition of the ChangeConfiguration.req PDU sent by Central System to Charge Point. It is RECOMMENDED that the content and meaning of the 'key' and 'value' fields is agreed upon between Charge Point and Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="ChangeConfiguration.conf", Comment="This contains the field definition of the ChangeConfiguration.conf PDU returned from Charge Point to Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="ClearCache.req", Comment="This contains the field definition of the ClearCache.req PDU sent by the Central System to the Charge Point.", TenantId = _tenantId },
                new OCPPMessage() { Message="ClearCache.conf", Comment="This contains the field definition of the ClearCache.conf PDU sent by the Charge Point to the Central System in response to a ClearCache.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="ClearChargingProfile.req", Comment="This contains the field definition of the ClearChargingProfile.req PDU sent by the Central System to the Charge Point.", TenantId = _tenantId },
                new OCPPMessage() { Message="ClearChargingProfile.conf", Comment="This contains the field definition of the ClearChargingProfile.conf PDU sent by the Charge Point to the Central System in response to a ClearChargingProfile.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="DataTransfer.req", Comment="This contains the field definition of the DataTransfer.req PDU sent either by the Central System to the Charge Point or vice versa.", TenantId = _tenantId },
                new OCPPMessage() { Message="DataTransfer.conf", Comment="This contains the field definition of the DataTransfer.conf PDU sent by the Charge Point to the Central System or vice versa in response to a DataTransfer.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="DiagnosticsStatusNotification.req", Comment="This contains the field definition of the DiagnosticsStatusNotification.req PDU sent by the Charge Point to the Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="DiagnosticsStatusNotification.conf", Comment="This contains the field definition of the DiagnosticsStatusNotification.conf PDU sent by the Central System to the Charge Point in response to a DiagnosticsStatusNotification.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="FirmwareStatusNotification.req", Comment="This contains the field definition of the FirmwareStatusNotifitacion.req PDU sent by the Charge Point to the Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="FirmwareStatusNotification.conf", Comment="This contains the field definition of the FirmwareStatusNotification.conf PDU sent by the Central System to the Charge Point in response to a FirmwareStatusNotification.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="GetCompositeSchedule.req", Comment="This contains the field definition of the GetCompositeSchedule.req PDU sent by the Central System to the Charge Point.", TenantId = _tenantId },
                new OCPPMessage() { Message="GetCompositeSchedule.conf", Comment="This contains the field definition of the GetCompositeSchedule.conf PDU sent by the Charge Point to the Central System in response to a GetCompositeSchedule.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="GetConfiguration.req", Comment="This contains the field definition of the GetConfiguration.req PDU sent by the the Central System to the Charge Point.", TenantId = _tenantId },
                new OCPPMessage() { Message="GetConfiguration.conf", Comment="This contains the field definition of the GetConfiguration.conf PDU sent by Charge Point the to the Central System in response to a GetConfiguration.req.", TenantId = _tenantId },
                new OCPPMessage() { Message="GetDiagnostics.req", Comment="This contains the field definition of the GetDiagnostics.req PDU sent by the Central System to the Charge Point.", TenantId = _tenantId },
                new OCPPMessage() { Message="GetDiagnostics.conf", Comment="This contains the field definition of the GetDiagnostics.conf PDU sent by the Charge Point to the Central System in response to a GetDiagnostics.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="GetLocalListVersion.req", Comment="This contains the field definition of the GetLocalListVersion.req PDU sent by the Central System to the Charge Point.", TenantId = _tenantId },
                new OCPPMessage() { Message="GetLocalListVersion.conf", Comment="This contains the field definition of the GetLocalListVersion.conf PDU sent by the Charge Point to Central System in response to a GetLocalListVersion.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="Heartbeat.req", Comment="This contains the field definition of the Heartbeat.req PDU sent by the Charge Point to the Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="Heartbeat.conf", Comment="This contains the field definition of the Heartbeat.conf PDU sent by the Central System to the Charge Point in response to a Heartbeat.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="MeterValues.req", Comment="This contains the field definition of the MeterValues.req PDU sent by the Charge Point to the Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="MeterValues.conf", Comment="This contains the field definition of the MeterValues.conf PDU sent by the Central System to the Charge Point in response to a MeterValues.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="RemoteStartTransaction.req", Comment="This contains the field definitions of the RemoteStartTransaction.req PDU sent to Charge Point by Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="RemoteStartTransaction.conf", Comment="This contains the field definitions of the RemoteStartTransaction.conf PDU sent from Charge Point to Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="RemoteStopTransaction.req", Comment="This contains the field definitions of the RemoteStopTransaction.req PDU sent to Charge Point by Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="RemoteStopTransaction.conf", Comment="This contains the field definitions of the RemoteStopTransaction.conf PDU sent from Charge Point to Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="ReserveNow.req", Comment="This contains the field definition of the ReserveNow.req PDU sent by the Central System to the Charge Point.", TenantId = _tenantId },
                new OCPPMessage() { Message="ReserveNow.conf", Comment="This contains the field definition of the ReserveNow.conf PDU sent by the Charge Point to the Central System in response to a ReserveNow.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="Reset.req", Comment="This contains the field definition of the Reset.req PDU sent by the Central System to the Charge Point.", TenantId = _tenantId },
                new OCPPMessage() { Message="Reset.conf", Comment="This contains the field definition of the Reset.conf PDU sent by the Charge Point to the Central System in response to a Reset.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="SendLocalList.req", Comment="This contains the field definition of the SendLocalList.req PDU sent by the Central System to the Charge Point.", TenantId = _tenantId },
                new OCPPMessage() { Message="SendLocalList.conf", Comment="This contains the field definition of the SendLocalList.conf PDU sent by the Charge Point to the Central System in response to a SendLocalList.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="SetChargingProfile.req", Comment="This contains the field definition of the SetChargingProfile.req PDU sent by the Central System to the Charge Point.", TenantId = _tenantId },
                new OCPPMessage() { Message="SetChargingProfile.conf", Comment="This contains the field definition of the SetChargingProfile.conf PDU sent by the Charge Point to the Central System in response to a SetChargingProfile.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="StartTransaction.req", Comment="This section contains the field definition of the StartTransaction.req PDU sent by the Charge Point to the Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="StartTransaction.conf", Comment="This contains the field definition of the StartTransaction.conf PDU sent by the Central System to the Charge Point in response to a StartTransaction.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="StatusNotification.req", Comment="This contains the field definition of the StatusNotification.req PDU sent by the Charge Point to the Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="StatusNotification.conf", Comment="This contains the field definition of the StatusNotification.conf PDU sent by the Central System to the Charge Point in response to an StatusNotification.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="StopTransaction.req", Comment="This contains the field definition of the StopTransaction.req PDU sent by the Charge Point to the Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="StopTransaction.conf", Comment="This contains the field definition of the StopTransaction.conf PDU sent by the Central System to the Charge Point in response to a StopTransaction.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="TriggerMessage.req", Comment="This contains the field definition of the TriggerMessage.req PDU sent by the Central System to the Charge Point.", TenantId = _tenantId },
                new OCPPMessage() { Message="TriggerMessage.conf", Comment="This contains the field definition of the TriggerMessage.conf PDU sent by the Charge Point to the Central System in response to a TriggerMessage.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="UnlockConnector.req", Comment="This contains the field definition of the UnlockConnector.req PDU sent by the Central System to the Charge Point.", TenantId = _tenantId },
                new OCPPMessage() { Message="UnlockConnector.conf", Comment="This contains the field definition of the UnlockConnector.conf PDU sent by the Charge Point to the Central System in response to an UnlockConnector.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="UpdateFirmware.req", Comment="This contains the field definition of the UpdateFirmware.req PDU sent by the Central System to the Charge Point.", TenantId = _tenantId },
                new OCPPMessage() { Message="UpdateFirmware.conf", Comment="This contains the field definition of the UpdateFirmware.conf PDU sent by the Charge Point to the Central System in response to a UpdateFirmware.req PDU.", TenantId = _tenantId },

                //Messages for test of OCPP 2.0 below. 
                new OCPPMessage() { Message="Authorize.req", Comment="This contains the field definition of the Authorize.req PDU sent by the Charge Point to the Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="Authorize.conf", Comment="This contains the field definition of the Authorize.conf PDU sent by the Central System to the Charge Point in response to a Authorize.req PDU.", TenantId = _tenantId },
                new OCPPMessage() { Message="BootNotification.req", Comment="This contains the field definition of the BootNotification.req PDU sent by the Charge Point to the Central System.", TenantId = _tenantId },
                new OCPPMessage() { Message="BootNotification.conf", Comment="This contains the field definition of the BootNotification.conf PDU sent by the Central System to the Charge Point in response to a BootNotification.req PDU.", TenantId = _tenantId }

            };

            //OCPP 1.5, add version to message
            for (int i = 0; i < 48; i++)
                OCPPMessages[i].OCPPVersionId = _context.OCPPVersions.Where(l => l.TenantId == _tenantId && l.VersionName == "OCPP15").SingleOrDefault().Id;

            //OCPP 1.6, add version to message
            for (int i = 48; i < 104; i++)
                OCPPMessages[i].OCPPVersionId = _context.OCPPVersions.Where(l => l.TenantId == _tenantId && l.VersionName == "OCPP16").SingleOrDefault().Id;

            //OCPP 2.0, add version to message
            for (int i = 104; i < 108; i++)
                OCPPMessages[i].OCPPVersionId = _context.OCPPVersions.Where(l => l.TenantId == _tenantId && l.VersionName == "OCPP20").SingleOrDefault().Id;
            return OCPPMessages;
        }

        public OCPPMessageCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateOCPPMessages();
        }

        private void CreateOCPPMessages()
        {
            foreach (var oCPPMessage in InitialOCPPMessages)
            {
                AddOCPPMessageIfNotExists(oCPPMessage);
            }
        }

        private void AddOCPPMessageIfNotExists(OCPPMessage oCPPMessage)
        {
            if (_context.OCPPMessages.Any(l => l.TenantId == _tenantId && l.Message == oCPPMessage.Message && l.OCPPVersionId == oCPPMessage.OCPPVersionId))
            {
                return;
            }

            _context.OCPPMessages.Add(oCPPMessage);

            _context.SaveChanges();
        }
    }
}
