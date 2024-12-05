using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Sixoclock.Onyx.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));
            pages.CreateChildPermission(AppPermissions.Pages_DemoUiComponents, L("DemoUiComponents"));

            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));

            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));

            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("CreatingNewUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("EditingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("DeletingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangePermissions, L("ChangingPermissions"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Impersonation, L("LoginForUsers"));

            var languages = administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create, L("CreatingNewLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete, L("DeletingLanguages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts, L("ChangingTexts"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            var organizationUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembers"));

            //TENANT-SPECIFIC PERMISSIONS

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement, L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);

            //HOST-SPECIFIC PERMISSIONS

            var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);

            var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("Maintenance"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_HangfireDashboard, L("HangfireDashboard"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Host);

            //Spine permissions
            var definitions = pages.CreateChildPermission(AppPermissions.Pages_Definitions, L("Definitions"));

            definitions.CreateChildPermission(AppPermissions.Pages_Definitions_Solar, L("Solar"));
            definitions.CreateChildPermission(AppPermissions.Pages_Definitions_Wind, L("Wind"));
            definitions.CreateChildPermission(AppPermissions.Pages_Definitions_Storage, L("Storage"));
            definitions.CreateChildPermission(AppPermissions.Pages_Definitions_Grid, L("Grid"));
            
            var chargers = definitions.CreateChildPermission(AppPermissions.Pages_Definitions_Chargers, L("Chargers"));
            chargers.CreateChildPermission(AppPermissions.Pages_Definitions_Chargers_Create, L("CreatingNewCharger"));
            chargers.CreateChildPermission(AppPermissions.Pages_Definitions_Chargers_Edit, L("EditingCharger"));
            chargers.CreateChildPermission(AppPermissions.Pages_Definitions_Chargers_Delete, L("DeletingCharger"));
            
            var chargersConnectors = chargers.CreateChildPermission(AppPermissions.Pages_Definitions_Chargers_Connectors, L("ChargersConnectors"));
            chargersConnectors.CreateChildPermission(AppPermissions.Pages_Definitions_Chargers_Connectors_Create, L("CreatingNewChargersConnectors"));
            chargersConnectors.CreateChildPermission(AppPermissions.Pages_Definitions_Chargers_Connectors_Edit, L("EditingChargersConnectors"));
            chargersConnectors.CreateChildPermission(AppPermissions.Pages_Definitions_Chargers_Connectors_Delete, L("DeletingChargersConnectors"));

            var chargersOCPPConfigs = chargers.CreateChildPermission(AppPermissions.Pages_Definitions_Chargers_OCPPConfigs, L("ChargersOCPPConfigs"));
            chargersOCPPConfigs.CreateChildPermission(AppPermissions.Pages_Definitions_Chargers_OCPPConfigs_Edit, L("EditingChargersOCPPConfigs"));

            var options = definitions.CreateChildPermission(AppPermissions.Pages_Definitions_Options, L("Options"));

            var vendors = options.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Vendors, L("Vendors"));
            vendors.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Vendors_Create, L("CreatingNewVendor"));
            vendors.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Vendors_Edit, L("EditingVendor"));
            vendors.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Vendors_Delete, L("DeletingVendor"));

            var errorCodes = options.CreateChildPermission(AppPermissions.Pages_Definitions_Options_ErrorCodes, L("ErrorCodes"));
            errorCodes.CreateChildPermission(AppPermissions.Pages_Definitions_Options_ErrorCodes_Create, L("CreatingNewErrorCode"));
            errorCodes.CreateChildPermission(AppPermissions.Pages_Definitions_Options_ErrorCodes_Edit, L("EditingErrorCode"));
            errorCodes.CreateChildPermission(AppPermissions.Pages_Definitions_Options_ErrorCodes_Delete, L("DeletingErrorCode"));

            var mounts = options.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Mounts, L("Mounts"));
            mounts.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Mounts_Create, L("CreatingNewMount"));
            mounts.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Mounts_Edit, L("EditingMount"));
            mounts.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Mounts_Delete, L("DeletingMount"));

            var releases = options.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Releases, L("Releases"));
            releases.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Releases_Create, L("CreatingNewRelease"));
            releases.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Releases_Edit, L("EditingRelease"));
            releases.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Releases_Delete, L("DeletingRelease"));

            var communications = options.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Communications, L("Communications"));
            communications.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Communications_Create, L("CreatingNewCommunication"));
            communications.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Communications_Edit, L("EditingCommunication"));
            communications.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Communications_Delete, L("DeletingCommunication"));

            var electrics = options.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Electrics, L("Electrics"));
            electrics.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Electrics_Create, L("CreatingNewElectric"));
            electrics.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Electrics_Edit, L("EditingElectric"));
            electrics.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Electrics_Delete, L("DeletingElectric"));

            var meters = options.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Meters, L("Meters"));
            meters.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Meters_Create, L("CreatingNewMeter"));
            meters.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Meters_Edit, L("EditingMeter"));
            meters.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Meters_Delete, L("DeletingMeter"));

            var others = options.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Others, L("Others"));
            others.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Others_Create, L("CreatingNewOther"));
            others.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Others_Edit, L("EditingOther"));
            others.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Others_Delete, L("DeletingOther"));

            var images = options.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Images, L("Images"));
            images.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Images_Create, L("CreatingNewImage"));
            images.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Images_Delete, L("DeletingImage"));

            var capacities = options.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Capacities, L("Capacities"));
            capacities.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Capacities_Create, L("CreatingNewCapacity"));
            capacities.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Capacities_Edit, L("EditingCapacity"));
            capacities.CreateChildPermission(AppPermissions.Pages_Definitions_Options_Capacities_Delete, L("DeletingCapacity"));

            var statusCodes = options.CreateChildPermission(AppPermissions.Pages_Definitions_Options_AdminStatuses, L("AdminStatuses"));
            statusCodes.CreateChildPermission(AppPermissions.Pages_Definitions_Options_AdminStatuses_Create, L("CreatingNewAdminStatus"));
            statusCodes.CreateChildPermission(AppPermissions.Pages_Definitions_Options_AdminStatuses_Edit, L("EditingAdminStatus"));
            statusCodes.CreateChildPermission(AppPermissions.Pages_Definitions_Options_AdminStatuses_Delete, L("DeletingAdminStatus"));

            var oCPP= options.CreateChildPermission(AppPermissions.Pages_Definitions_Options_OCPP, L("OCPP"));
            oCPP.CreateChildPermission(AppPermissions.Pages_Definitions_Options_OCPP_Edit, L("EditingOCPP"));

            var topology = pages.CreateChildPermission(AppPermissions.Pages_Topology, L("Topology"));

            var segments = topology.CreateChildPermission(AppPermissions.Pages_Topology_Segments, L("Segments"));
            segments.CreateChildPermission(AppPermissions.Pages_Topology_Segments_Create, L("CreatingNewSegment"));
            segments.CreateChildPermission(AppPermissions.Pages_Topology_Segments_Edit, L("EditingSegment"));
            segments.CreateChildPermission(AppPermissions.Pages_Topology_Segments_Delete, L("DeletingSegments"));

            var clients = topology.CreateChildPermission(AppPermissions.Pages_Topology_Clients, L("Clients"));
            clients.CreateChildPermission(AppPermissions.Pages_Topology_Clients_Create, L("CreatingNewClient"));
            clients.CreateChildPermission(AppPermissions.Pages_Topology_Clients_Edit, L("EditingClient"));
            clients.CreateChildPermission(AppPermissions.Pages_Topology_Clients_Delete, L("DeletingClient"));

            var countries = topology.CreateChildPermission(AppPermissions.Pages_Topology_Countries, L("Countries"));
            countries.CreateChildPermission(AppPermissions.Pages_Topology_Countries_Create, L("CreatingNewCountry"));
            countries.CreateChildPermission(AppPermissions.Pages_Topology_Countries_Edit, L("EditingCountry"));
            countries.CreateChildPermission(AppPermissions.Pages_Topology_Countries_Delete, L("DeletingCountry"));

            var regions = topology.CreateChildPermission(AppPermissions.Pages_Topology_Regions, L("Regions"));
            regions.CreateChildPermission(AppPermissions.Pages_Topology_Regions_Create, L("CreatingNewRegion"));
            regions.CreateChildPermission(AppPermissions.Pages_Topology_Regions_Edit, L("EditingRegion"));
            regions.CreateChildPermission(AppPermissions.Pages_Topology_Regions_Delete, L("DeletingRegion"));

            var installs = topology.CreateChildPermission(AppPermissions.Pages_Topology_Installs, L("Installs"));
            installs.CreateChildPermission(AppPermissions.Pages_Topology_Installs_Create, L("CreatingNewInstall"));
            installs.CreateChildPermission(AppPermissions.Pages_Topology_Installs_Edit, L("EditingInstall"));
            installs.CreateChildPermission(AppPermissions.Pages_Topology_Installs_Delete, L("DeletingInstall"));

            var groups = topology.CreateChildPermission(AppPermissions.Pages_Topology_Groups, L("Groups"));
            groups.CreateChildPermission(AppPermissions.Pages_Topology_Groups_Create, L("CreatingNewGroup"));
            groups.CreateChildPermission(AppPermissions.Pages_Topology_Groups_Edit, L("EditingGroup"));
            groups.CreateChildPermission(AppPermissions.Pages_Topology_Groups_Delete, L("DeletingGroup"));

            var chargePoints = topology.CreateChildPermission(AppPermissions.Pages_Topology_ChargePoints, L("ChargePoints"));

            var chargePointsBoxes = chargePoints.CreateChildPermission(AppPermissions.Pages_Topology_ChargePoints_Boxes, L("Boxes"));
            chargePointsBoxes.CreateChildPermission(AppPermissions.Pages_Topology_ChargePoints_Boxes_Create, L("CreatingNewBox"));
            chargePointsBoxes.CreateChildPermission(AppPermissions.Pages_Topology_ChargePoints_Boxes_Edit, L("EditingBox"));
            chargePointsBoxes.CreateChildPermission(AppPermissions.Pages_Topology_ChargePoints_Boxes_Delete, L("DeletingBox"));

            var chargePointsConnectors = chargePoints.CreateChildPermission(AppPermissions.Pages_Topology_ChargePoints_Connectors, L("BoxesConnectors"));
            chargePointsConnectors.CreateChildPermission(AppPermissions.Pages_Topology_ChargePoints_Connectors_Create, L("CreatingNewBoxesConnector"));
            chargePointsConnectors.CreateChildPermission(AppPermissions.Pages_Topology_ChargePoints_Connectors_Edit, L("EditingBoxesConnector"));
            chargePointsConnectors.CreateChildPermission(AppPermissions.Pages_Topology_ChargePoints_Connectors_Delete, L("DeletingBoxesConnector"));

            var chargePointsOCPPConfig = chargePoints.CreateChildPermission(AppPermissions.Pages_Topology_ChargePoints_OCPPConfig, L("BoxesOCPPConfig"));
            chargePointsOCPPConfig.CreateChildPermission(AppPermissions.Pages_Topology_ChargePoints_OCPPConfig_Edit, L("EditingNewBoxesOCPPConfig"));

            var tags = pages.CreateChildPermission(AppPermissions.Pages_Tags, L("Tags"));

            var parentTags = tags.CreateChildPermission(AppPermissions.Pages_Tags_ParentTags, L("ParentTags"));
            parentTags.CreateChildPermission(AppPermissions.Pages_Tags_ParentTags_Create, L("CreatingNewParentTag"));
            parentTags.CreateChildPermission(AppPermissions.Pages_Tags_ParentTags_Edit, L("EditingParentTag"));
            parentTags.CreateChildPermission(AppPermissions.Pages_Tags_ParentTags_Delete, L("DeletingParentTag"));

            var userTags = tags.CreateChildPermission(AppPermissions.Pages_Tags_UserTags, L("UserTags"));
            userTags.CreateChildPermission(AppPermissions.Pages_Tags_UserTags_Create, L("CreatingNewUserTag"));
            userTags.CreateChildPermission(AppPermissions.Pages_Tags_UserTags_Edit, L("EditingUserTag"));
            userTags.CreateChildPermission(AppPermissions.Pages_Tags_UserTags_Delete, L("DeletingUserTag"));

            var operation = pages.CreateChildPermission(AppPermissions.Pages_Operation, L("ViewOperation"));

            operation.CreateChildPermission(AppPermissions.Pages_Operation_Overview, L("ViewOperationsOverview"));
            operation.CreateChildPermission(AppPermissions.Pages_Operation_Transactions, L("ViewTransactions"));
            operation.CreateChildPermission(AppPermissions.Pages_Operation_Reservations, L("ViewReservations"));
            operation.CreateChildPermission(AppPermissions.Pages_Operation_OCPPMessages, L("ViewOCPPMessages"));

            var grants = pages.CreateChildPermission(AppPermissions.Pages_Grants, L("Grants"));
            grants.CreateChildPermission(AppPermissions.Pages_Grants_Create, L("Create_Grants"));
            grants.CreateChildPermission(AppPermissions.Pages_Grants_Edit, L("Edit_Grants"));
            grants.CreateChildPermission(AppPermissions.Pages_Grants_Delete, L("Delete_Grants"));

            var userRuleSets = pages.CreateChildPermission(AppPermissions.Pages_UserRuleSets, L("UserRuleSets"));
            userRuleSets.CreateChildPermission(AppPermissions.Pages_UserRuleSets_View, L("ViewUserRuleSets"));
            userRuleSets.CreateChildPermission(AppPermissions.Pages_UserRuleSets_Add, L("AddUserRuleSet"));
            userRuleSets.CreateChildPermission(AppPermissions.Pages_UserRuleSets_Delete, L("RemoveUserRuleSet"));

            var overview = pages.CreateChildPermission(AppPermissions.Pages_Overview, L("Overview"));

            var leaderBoard = pages.CreateChildPermission(AppPermissions.Pages_LeaderBoard, L("LeaderBoard"));

            var account = pages.CreateChildPermission(AppPermissions.Pages_Account, L("Account"));
            account.CreateChildPermission(AppPermissions.Pages_Account_Create, L("Create_Account"));
            account.CreateChildPermission(AppPermissions.Pages_Account_Edit, L("Edit_Account"));
            account.CreateChildPermission(AppPermissions.Pages_Account_Delete, L("Delete_Account"));

            var keyCards = pages.CreateChildPermission(AppPermissions.Pages_KeyCards, L("KeyCards"));
            keyCards.CreateChildPermission(AppPermissions.Pages_KeyCards_Create, L("Create_KeyCards"));
            keyCards.CreateChildPermission(AppPermissions.Pages_KeyCards_Edit, L("Edit_KeyCards"));
            keyCards.CreateChildPermission(AppPermissions.Pages_KeyCards_Delete, L("Delete_KeyCards"));

            var paymentSetup = pages.CreateChildPermission(AppPermissions.Pages_PaymentSetup, L("PaymentSetup"));
            paymentSetup.CreateChildPermission(AppPermissions.Pages_PaymentSetup_Create, L("Create_PaymentSetup"));
            paymentSetup.CreateChildPermission(AppPermissions.Pages_PaymentSetup_Edit, L("Edit_PaymentSetup"));
            paymentSetup.CreateChildPermission(AppPermissions.Pages_PaymentSetup_Delete, L("Delete_PaymentSetup"));

            var userTransactions = pages.CreateChildPermission(AppPermissions.Pages_UserTransactions, L("UserTransactions"));
            userTransactions.CreateChildPermission(AppPermissions.Pages_UserTransactions_Create, L("Create_UserTransactions"));
            userTransactions.CreateChildPermission(AppPermissions.Pages_UserTransactions_Edit, L("Edit_UserTransactions"));
            userTransactions.CreateChildPermission(AppPermissions.Pages_UserTransactions_Delete, L("Delete_UserTransactions"));

            var bills = pages.CreateChildPermission(AppPermissions.Pages_Bills, L("Bills"));
            bills.CreateChildPermission(AppPermissions.Pages_Bills_Create, L("Create_Bills"));
            bills.CreateChildPermission(AppPermissions.Pages_Bills_Edit, L("Edit_Bills"));
            bills.CreateChildPermission(AppPermissions.Pages_Bills_Delete, L("Delete_Bills"));

            var map = pages.CreateChildPermission(AppPermissions.Pages_Map, L("Map"));

            var userChargepoints = pages.CreateChildPermission(AppPermissions.Pages_UserChargepoints, L("UserChargepoints"));

            var outerServices = pages.CreateChildPermission(AppPermissions.Pages_Services, L("Services"));
            outerServices.CreateChildPermission(AppPermissions.Pages_Services_Create, L("Create_Services"));
            outerServices.CreateChildPermission(AppPermissions.Pages_Services_Edit, L("Edit_Services"));
            outerServices.CreateChildPermission(AppPermissions.Pages_Services_Delete, L("Delete_Services"));

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, OnyxConsts.LocalizationSourceName);
        }
    }
}
