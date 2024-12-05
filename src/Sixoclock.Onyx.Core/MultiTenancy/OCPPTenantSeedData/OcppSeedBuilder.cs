using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Sixoclock.Onyx.Grants;

namespace Sixoclock.Onyx.MultiTenancy.OCPPTenantSeedData
{
    public class OcppSeedBuilder:OnyxDomainServiceBase,IOcppSeedBuilder
    {
        private readonly IRepository<AuthorizationStatus> _authorizationStatusRepository;
        private readonly IRepository<AvailabilityStatus> _availabilityStatusRepository;
        private readonly IRepository<AvailabilityType> _availabilityTypeRepository;
        private readonly IRepository<BillingStatus> _billingStatusRepository;
        private readonly IRepository<BillingType> _billingTypeRepository;
        private readonly IRepository<CancelReservationStatus> _cancelReservationStatusRepository;
        private readonly IRepository<Capacity> _capacityRepository;
        private readonly IRepository<Power> _powerRepository;
        private readonly IRepository<Unit> _unitRepository;
        private readonly IRepository<ClearCacheStatus> _clearCahceStatusRepository;
        private readonly IRepository<ConfigStatus> _configStatusRepository;
        private readonly IRepository<ConfigType> _configTypeRepository;
        private readonly IRepository<ConnectorStatusCode> _connectorStatusCodeRepository;
        private readonly IRepository<ConnectorType> _connectorTypeRepository;
        private readonly IRepository<Context> _contextRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<Currency> _currencyRepository;
        private readonly IRepository<ErrorCode> _errorCodeRepository;
        private readonly IRepository<FirmwareStatus> _firmwareStatusRepository;
        private readonly IRepository<Format> _formatRepository;
        private readonly IRepository<RuleCondition> _ruleConditionRepository;
        private readonly IRepository<KeyValue> _keyValueRepository;
        private readonly IRepository<OCPPFeature> _ocppFeatuRepository;
        private readonly IRepository<Location> _locationRepository;
        private readonly IRepository<Measurand> _measurandRepository;
        private readonly IRepository<MeterValueType> _meterValueTypeRepository;
        private readonly IRepository<OCPPMessage> _ocppMessageRepository;
        private readonly IRepository<OCPPVersion> _ocppVersionRepository;
        private readonly IRepository<OCPPStatus> _ocppStatusRepository;
        private readonly IRepository<OCPPTransport> _ocppTransportRepository;
        private readonly IRepository<UpdateType> _updateTypeRepository;
        private readonly IRepository<UpdateStatus> _updateStatusRepository;
        private readonly IRepository<UnlockStatus> _unlockStatusRepository;
        private readonly IRepository<TransactionType> _transactionTypeRepository;
        private readonly IRepository<TransactionStatus> _transactionStatusRepository;
        private readonly IRepository<TagTransactionType> _tagTransactionTypeRepository;
        private readonly IRepository<ResetStatus> _resetStatusRepository;
        private readonly IRepository<ResetType> _resetTypeRepository;
        private readonly IRepository<RuleRelation> _ruleRelationRepository;
        private readonly IRepository<ReservationStatus> _reservationStatusRepository;
        private readonly IRepository<RemoteStartStopEventType> _remoteStartStopEventTypeRepository;
        private readonly IRepository<RemoteStartStopStatus> _remoteStartStopStatusRepository;
        private readonly IRepository<RegistrationStatus> _registrationStatusRepository;
        private readonly IRepository<Reason> _reasonRepository;
        private readonly IRepository<Phase> _phaseRepository;

        public OcppSeedBuilder(IRepository<AuthorizationStatus> authorizationStatusRepository,
            IRepository<AvailabilityStatus> availabilityStatusRepository,
            IRepository<AvailabilityType> availabilityTypeRepository,
            IRepository<BillingStatus> billingStatusRepository, IRepository<BillingType> billingTypeRepository,
            IRepository<CancelReservationStatus> cancelReservationStatusRepository,
            IRepository<Capacity> capacityRepository, IRepository<Power> powerRepository,
            IRepository<Unit> unitRepository, IRepository<ClearCacheStatus> clearCahceStatusRepository,
            IRepository<ConfigStatus> configStatusRepository, IRepository<ConfigType> configTypeRepository,
            IRepository<ConnectorStatusCode> connectorStatusCodeRepository,
            IRepository<ConnectorType> connectorTypeRepository, IRepository<Context> contextRepository,
            IRepository<Country> countryRepository, IRepository<Currency> currencyRepository,
            IRepository<ErrorCode> errorCodeRepository, IRepository<FirmwareStatus> firmwareStatusRepository,
            IRepository<Format> formatRepository, IRepository<RuleCondition> ruleConditionRepository,
            IRepository<KeyValue> keyValueRepository, IRepository<OCPPFeature> ocppFeatuRepository,
            IRepository<Location> locationRepository, IRepository<Measurand> measurandRepository,
            IRepository<MeterValueType> meterValueTypeRepository, IRepository<OCPPMessage> ocppMessageRepository,
            IRepository<OCPPVersion> ocppVersionRepository, IRepository<OCPPStatus> ocppStatusRepository,
            IRepository<OCPPTransport> ocppTransportRepository, IRepository<UpdateType> updateTypeRepository,
            IRepository<UnlockStatus> unlockStatusRepository, IRepository<UpdateStatus> updateStatusRepository,
            IRepository<TransactionType> transactionTypeRepository,
            IRepository<TagTransactionType> tagTransactionTypeRepository,
            IRepository<TransactionStatus> transactionStatusRepository,
            IRepository<RuleRelation> ruleRelationRepository, IRepository<ResetType> resetTypeRepository,
            IRepository<ResetStatus> resetStatusRepository,
            IRepository<RemoteStartStopStatus> remoteStartStopStatusRepository,
            IRepository<RemoteStartStopEventType> remoteStartStopEventTypeRepository,
            IRepository<ReservationStatus> reservationStatusRepository, IRepository<Reason> reasonRepository, IRepository<RegistrationStatus> registrationStatusRepository, IRepository<Phase> phaseRepository)
        {
            _authorizationStatusRepository = authorizationStatusRepository;
            _availabilityStatusRepository = availabilityStatusRepository;
            _availabilityTypeRepository = availabilityTypeRepository;
            _billingStatusRepository = billingStatusRepository;
            _billingTypeRepository = billingTypeRepository;
            _cancelReservationStatusRepository = cancelReservationStatusRepository;
            _capacityRepository = capacityRepository;
            _powerRepository = powerRepository;
            _unitRepository = unitRepository;
            _clearCahceStatusRepository = clearCahceStatusRepository;
            _configStatusRepository = configStatusRepository;
            _configTypeRepository = configTypeRepository;
            _connectorStatusCodeRepository = connectorStatusCodeRepository;
            _connectorTypeRepository = connectorTypeRepository;
            _contextRepository = contextRepository;
            _countryRepository = countryRepository;
            _currencyRepository = currencyRepository;
            _errorCodeRepository = errorCodeRepository;
            _firmwareStatusRepository = firmwareStatusRepository;
            _formatRepository = formatRepository;
            _ruleConditionRepository = ruleConditionRepository;
            _keyValueRepository = keyValueRepository;
            _ocppFeatuRepository = ocppFeatuRepository;
            _locationRepository = locationRepository;
            _measurandRepository = measurandRepository;
            _meterValueTypeRepository = meterValueTypeRepository;
            _ocppMessageRepository = ocppMessageRepository;
            _ocppVersionRepository = ocppVersionRepository;
            _ocppStatusRepository = ocppStatusRepository;
            _ocppTransportRepository = ocppTransportRepository;
            _updateTypeRepository = updateTypeRepository;
            _unlockStatusRepository = unlockStatusRepository;
            _updateStatusRepository = updateStatusRepository;
            _transactionTypeRepository = transactionTypeRepository;
            _tagTransactionTypeRepository = tagTransactionTypeRepository;
            _transactionStatusRepository = transactionStatusRepository;
            _ruleRelationRepository = ruleRelationRepository;
            _resetTypeRepository = resetTypeRepository;
            _resetStatusRepository = resetStatusRepository;
            _remoteStartStopStatusRepository = remoteStartStopStatusRepository;
            _remoteStartStopEventTypeRepository = remoteStartStopEventTypeRepository;
            _reservationStatusRepository = reservationStatusRepository;
            _reasonRepository = reasonRepository;
            _registrationStatusRepository = registrationStatusRepository;
            _phaseRepository = phaseRepository;
        }

        public async Task SeedOcppDataAsync(int tenantId)
        {
            await SeedOcppTransports(tenantId);
            await SeedOcppVersions(tenantId);
            await SeedOcppPhases(tenantId);
            await SeedOcppPowers(tenantId);
            await SeedOcppReasons(tenantId);
            await SeedAuthorizationStatusAsync(tenantId);
            await SeedAvailabilityStatuses(tenantId);
            await SeedAvailabilityTypes(tenantId);
            await SeedBillingStatuses(tenantId);
            await SeedBillingTypes(tenantId);
            await SeedCancelReservationStatuses(tenantId);
            await SeedUnits(tenantId);
            await SeedOcppFeatures(tenantId);
            await SeedCapacities(tenantId);
            await SeedClearcacheStatuses(tenantId);
            await SeedConfigStatuses(tenantId);
            await SeedConfigTypes(tenantId);
            await SeedConnectorStatusCodes(tenantId);
            await SeedConnectorTypes(tenantId);
            await SeedContexts(tenantId);
            await SeedCountries(tenantId);
            await SeedCurrencies(tenantId);
            await SeedErrorCodes(tenantId);
            await SeedFirmwareStatuses(tenantId);
            await SeedFormats(tenantId);
            await SeedRuleConditionTypes(tenantId);
            await SeedKeyValues(tenantId);
            await SeedLocations(tenantId);
            await SeedMeasurands(tenantId);
            await SeedMeterValueTypes(tenantId);
          
            await SeedOcppMessages(tenantId);
            await SeedOcppStatuses(tenantId);
            await SeedRegistrationStatuses(tenantId);
            await SeedRemoteStartStopEvents(tenantId);
            await SeedRemoteStartStopStatues(tenantId);
            await SeedReservationStatues(tenantId);
            await SeedResetStatues(tenantId);
            await SeedResetTypes(tenantId);
            await SeedRuleConditionRelations(tenantId);
            await SeedTagTransactionTypes(tenantId);
            await SeedTransactionStatuses(tenantId);
            await SeedTransactionTypes(tenantId);
            await SeedUnlockStatuses(tenantId);
            await SeedUpdateStatuses(tenantId);
            await SeedUpdateTypes(tenantId);
        }

        private async Task SeedAuthorizationStatusAsync(int tenantId)
        {
            foreach (var authorizationStatuse in OCPPSeedData.GetInitialAuthorizationStatuses(tenantId))
            {
                await _authorizationStatusRepository.InsertAsync(authorizationStatuse);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedAvailabilityStatuses(int tenantId)
        {
            foreach (var availabilityStatuse in OCPPSeedData.GetInitialAvailabilityStatuses(tenantId))
            {
                await _availabilityStatusRepository.InsertAsync(availabilityStatuse);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedAvailabilityTypes(int tenantId)
        {
            foreach (var availabilityType in OCPPSeedData.GetInitialAvailabilityTypes(tenantId))
            {
                await _availabilityTypeRepository.InsertAsync(availabilityType);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedBillingStatuses(int tenantId)
        {
            foreach (var billingStatuse in OCPPSeedData.GetInitialBillingStatuss(tenantId))
            {
                await _billingStatusRepository.InsertAsync(billingStatuse);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedBillingTypes(int tenantId)
        {
            foreach (var billingType in OCPPSeedData.GetInitialBillingTypes(tenantId))
            {
                await _billingTypeRepository.InsertAsync(billingType);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedCancelReservationStatuses(int tenantId)
        {
            foreach (var reservationStatus in OCPPSeedData.GetInitialCancelReservationStatuses(tenantId))
            {
                await _cancelReservationStatusRepository.InsertAsync(reservationStatus);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedCapacities(int tenantId)
        {
            foreach (var capacity in OCPPSeedData.GetInitialCapacities(tenantId))
            {
                if (capacity.PowerId == 1)
                    capacity.PowerId = (await _powerRepository.FirstOrDefaultAsync(l => l.PowerName == "1-Phase")).Id;
                else if (capacity.PowerId == 2)
                    capacity.PowerId = (await _powerRepository.FirstOrDefaultAsync(l =>  l.PowerName == "3-Phase")).Id;
                else if (capacity.PowerId == 3)
                    capacity.PowerId = (await _powerRepository.FirstOrDefaultAsync(l => l.PowerName == "DC")).Id;

                if (capacity.UnitId == 6)
                    capacity.UnitId = (await _unitRepository.FirstOrDefaultAsync(l => l.UnitName == "kW")).Id;
                await _capacityRepository.InsertAsync(capacity);
            }

            await CurrentUnitOfWork.SaveChangesAsync();

        }
        private async Task SeedClearcacheStatuses(int tenantId)
        {
            foreach (var clearCacheStatuse in OCPPSeedData.GetInitialclearCacheStatuses(tenantId))
            {
                await _clearCahceStatusRepository.InsertAsync(clearCacheStatuse);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedConfigStatuses(int tenantId)
        {
            foreach (var configStatuse in OCPPSeedData.GetInitialConfigStatuses(tenantId))
            {
                await _configStatusRepository.InsertAsync(configStatuse);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedConfigTypes(int tenantId)
        {
            foreach (var configType in OCPPSeedData.GetInitialConfigTypes(tenantId))
            {
                await _configTypeRepository.InsertAsync(configType);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedConnectorStatusCodes(int tenantId)
        {
            foreach (var connectorStatusCode in OCPPSeedData.GetInitialConnectorStatusCodes(tenantId))
            {
                await _connectorStatusCodeRepository.InsertAsync(connectorStatusCode);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedConnectorTypes(int tenantId)
        {
            foreach (var connectorType in OCPPSeedData.GetInitialConnectorTypes(tenantId))
            {
                await _connectorTypeRepository.InsertAsync(connectorType);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedContexts(int tenantId)
        {
            foreach (var context in OCPPSeedData.GetInitialContexts(tenantId))
            {
                await _contextRepository.InsertAsync(context);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedCountries(int tenantId)
        {
            foreach (var country in OCPPSeedData.GetInitialCountries(tenantId))
            {
                await _countryRepository.InsertAsync(country);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedCurrencies(int tenantId)
        {
            foreach (var currency in OCPPSeedData.GetInitialCurrencys(tenantId))
            {
                await _currencyRepository.InsertAsync(currency);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedErrorCodes(int tenantId)
        {
            foreach (var errorCode in OCPPSeedData.GetInitialErrorCodes(tenantId))
            {
                await _errorCodeRepository.InsertAsync(errorCode);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedFirmwareStatuses(int tenantId)
        {
            foreach (var firmware in OCPPSeedData.GetInitialFirmwareStatuses(tenantId))
            {
                await _firmwareStatusRepository.InsertAsync(firmware);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedFormats(int tenantId)
        {
            foreach (var formate in OCPPSeedData.GetInitialFormates(tenantId))
            {
                await _formatRepository.InsertAsync(formate);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedRuleConditionTypes(int tenantId)
        {
            foreach (var ruleConditionType in OCPPSeedData.GetInitialGrantRuleConditionTypes(tenantId))
            {
                await _ruleConditionRepository.InsertAsync(ruleConditionType);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedKeyValues(int tenantId)
        {
            var KeyValues = OCPPSeedData.GetInitialKeyValues(tenantId);
            var ocppFeature15 =
                await _ocppFeatuRepository.FirstOrDefaultAsync(l =>  l.FeatureName == "Core15");
            var ocppFeatureCore =
                await _ocppFeatuRepository.FirstOrDefaultAsync(l =>
                    l.TenantId == tenantId && l.FeatureName == "Core");
            var localAuthListManagermentFeature= await _ocppFeatuRepository.FirstOrDefaultAsync(l =>  l.FeatureName == "LocalAuthListManagement");
            var smartChargningFeature = await _ocppFeatuRepository.FirstOrDefaultAsync(l =>  l.FeatureName == "SmartCharging");
            var GeneralFeature = await _ocppFeatuRepository.FirstOrDefaultAsync(l =>  l.FeatureName == "General");
            //Add KeyValues for OCPP 1.5. There are no features in OCPP 1.5, so we use a Dummy feature named Core15
            for (int i = 0; i < 34; i++)
                KeyValues[i].OCPPFeatureId = ocppFeature15.Id;

            //Add KeyValues for OCPP 1.6 Core
            for (int i = 34; i < 68; i++)
                KeyValues[i].OCPPFeatureId = ocppFeatureCore.Id;

            //Add KeyValues for OCPP 1.6, Local authorization lists
            for (int i = 68; i < 71; i++)
                KeyValues[i].OCPPFeatureId = localAuthListManagermentFeature.Id;

            //Add KeyValues for OCPP 1.6, Reservation
            for (int i = 71; i < 72; i++)
                KeyValues[i].OCPPFeatureId = localAuthListManagermentFeature.Id;

            //Add KeyValues for OCPP 1.6, Smart Charging
            for (int i = 72; i < 77; i++)
                KeyValues[i].OCPPFeatureId = smartChargningFeature.Id;

            //Add KeyValues for OCPP 2.0. General. These are just a couple of values for testing
            for (int i = 77; i < 79; i++)
                KeyValues[i].OCPPFeatureId = GeneralFeature.Id;

            foreach (var keyValue in KeyValues)
            {

                await _keyValueRepository.InsertAsync(keyValue);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedLocations(int tenantId)
        {
            foreach (var location in OCPPSeedData.GetInitialLocations(tenantId))
            {
                await _locationRepository.InsertAsync(location);
            }

            await CurrentUnitOfWork.SaveChangesAsync();

        }
        private async Task SeedMeasurands(int tenantId)
        {
            foreach (var measurand in OCPPSeedData.GetInitialMeasurands(tenantId))
            {
                await _measurandRepository.InsertAsync(measurand);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedMeterValueTypes(int tenantId)
        {
            foreach (var meterValueType in OCPPSeedData.GetInitialMeterValueTypes(tenantId))
            {
                await _meterValueTypeRepository.InsertAsync(meterValueType);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedOcppFeatures(int tenantId)
        {
            var version15 = await _ocppVersionRepository.FirstOrDefaultAsync(l => l.VersionName == "OCPP15");
            var version16 = await _ocppVersionRepository.FirstOrDefaultAsync(l => l.VersionName == "OCPP16");
            var version20 = await _ocppVersionRepository.FirstOrDefaultAsync(l => l.VersionName == "OCPP20");
            var features = OCPPSeedData.GetInitialOCPPFeatures(tenantId);
            for (int i = 0; i < 1; i++)
                features[i].OCPPVersionId = version15.Id;
            for (int i = 1; i < 7; i++)
                features[i].OCPPVersionId = version16.Id;
            for (int i = 7; i < 13; i++)
                features[i].OCPPVersionId = version20.Id;

            foreach (var ocppFeature in features)
            {
                await _ocppFeatuRepository.InsertAsync(ocppFeature);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedOcppMessages(int tenantId)
        {
            var OCPPMessages = OCPPSeedData.GetInitialOCPPMessages(tenantId);
            var version15= await _ocppVersionRepository.FirstOrDefaultAsync(l =>  l.VersionName == "OCPP15");
            var version16 = await _ocppVersionRepository.FirstOrDefaultAsync(l =>  l.VersionName == "OCPP16");
            var version20 = await _ocppVersionRepository.FirstOrDefaultAsync(l =>  l.VersionName == "OCPP20");
            //OCPP 1.5, add version to message
            for (int i = 0; i < 48; i++)
                OCPPMessages[i].OCPPVersionId = version15.Id;

            ////OCPP 1.6, add version to message
            for (int i = 48; i < 104; i++)
                OCPPMessages[i].OCPPVersionId = version16.Id;

            ////OCPP 2.0, add version to message
            for (int i = 104; i < 108; i++)
                OCPPMessages[i].OCPPVersionId = version20.Id;
            foreach (var ocppMessage in OCPPMessages)
            {
                await _ocppMessageRepository.InsertAsync(ocppMessage);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedOcppStatuses(int tenantId)
        {
            foreach (var ocppStatuse in OCPPSeedData.GetInitialOCPPStatuses(tenantId))
            {
                await _ocppStatusRepository.InsertAsync(ocppStatuse);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedOcppTransports(int tenantId)
        {
            foreach (var ocppteTransport in OCPPSeedData.GetInitialOCPPTransports(tenantId))
            {
                await _ocppTransportRepository.InsertAsync(ocppteTransport);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedOcppVersions(int tenantId)
        {
            foreach (var ocppVersion in OCPPSeedData.GetInitialOCPPVersions(tenantId))
            {
                await _ocppVersionRepository.InsertAsync(ocppVersion);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedOcppPhases(int tenantId)
        {
            foreach (var ocppPhase in OCPPSeedData.GetInitialPhases(tenantId))
            {
                await _phaseRepository.InsertAsync(ocppPhase);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedOcppPowers(int tenantId)
        {
            foreach (var ocppPower in OCPPSeedData.GetInitialPowers(tenantId))
            {
                await _powerRepository.InsertAsync(ocppPower);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedOcppReasons(int tenantId)
        {
            foreach (var ocppReason in OCPPSeedData.GetInitialReasons(tenantId))
            {
                await _reasonRepository.InsertAsync(ocppReason);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedRegistrationStatuses(int tenantId)
        {
            foreach (var registrationStatuse in OCPPSeedData.GetInitialRegistrationStatuses(tenantId))
            {
                await _registrationStatusRepository.InsertAsync(registrationStatuse);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedRemoteStartStopEvents(int tenantId)
        {
            foreach (var remoteStartStopEvent in OCPPSeedData.GetInitialRemoteStartStopEventTypes(tenantId))
            {
                await _remoteStartStopEventTypeRepository.InsertAsync(remoteStartStopEvent);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedRemoteStartStopStatues(int tenantId)
        {
            foreach (var remoteStartStopStatuse in OCPPSeedData.GetInitialRemoteStartStopStatuses(tenantId))
            {
                await _remoteStartStopStatusRepository.InsertAsync(remoteStartStopStatuse);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedReservationStatues(int tenantId)
        {
            foreach (var reservationStatuse in OCPPSeedData.GetInitialReservationStatuses(tenantId))
            {
                await _reservationStatusRepository.InsertAsync(reservationStatuse);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedResetStatues(int tenantId)
        {
            foreach (var resetStatus in OCPPSeedData.GetInitialResetStatuses(tenantId))
            {
                await _resetStatusRepository.InsertAsync(resetStatus);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedResetTypes(int tenantId)
        {
            foreach (var resetType in OCPPSeedData.GetInitialResetTypes(tenantId))
            {
                await _resetTypeRepository.InsertAsync(resetType);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedRuleConditionRelations(int tenantId)
        {
            foreach (var ruleConditionRelation in OCPPSeedData.GetInitialRuleConditionRelations(tenantId))
            {
                await _ruleRelationRepository.InsertAsync(ruleConditionRelation);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedTagTransactionTypes(int tenantId)
        {
            foreach (var tagTransactionType in OCPPSeedData.GetInitialTagTransationTypes(tenantId))
            {
                await _tagTransactionTypeRepository.InsertAsync(tagTransactionType);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedTransactionStatuses(int tenantId)
        {
            foreach (var transactionStatuse in OCPPSeedData.GetInitialTransactionStatuses(tenantId))
            {
                await _transactionStatusRepository.InsertAsync(transactionStatuse);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedTransactionTypes(int tenantId)
        {
            foreach (var transactionType in OCPPSeedData.GetInitialTransactionTypes(tenantId))
            {
                await _transactionTypeRepository.InsertAsync(transactionType);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedUnlockStatuses(int tenantId)
        {
            foreach (var unlockStatuse in OCPPSeedData.GetInitialUnlockStatuses(tenantId))
            {
                await _unlockStatusRepository.InsertAsync(unlockStatuse);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedUpdateStatuses(int tenantId)
        {
            foreach (var updateStatuse in OCPPSeedData.GetInitialUpdateStatuses(tenantId))
            {
                await _updateStatusRepository.InsertAsync(updateStatuse);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedUpdateTypes(int tenantId)
        {
            foreach (var updateType in OCPPSeedData.GetInitialUpdateTypes(tenantId))
            {
                await _updateTypeRepository.InsertAsync(updateType);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }
        private async Task SeedUnits(int tenantId)
        {
            foreach (var unit in OCPPSeedData.GetInitialUnits(tenantId))
            {
                await _unitRepository.InsertAsync(unit);
            }

            await CurrentUnitOfWork.SaveChangesAsync();
        }

    }
}
