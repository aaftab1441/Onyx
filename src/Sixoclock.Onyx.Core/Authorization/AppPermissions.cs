namespace Sixoclock.Onyx.Authorization
{
    /// <summary>
    /// Defines string constants for application's permission names.
    /// <see cref="AppAuthorizationProvider"/> for permission definitions.
    /// </summary>
    public static class AppPermissions
    {
        //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

        public const string Pages = "Pages";

        public const string Pages_DemoUiComponents= "Pages.DemoUiComponents";
        public const string Pages_Administration = "Pages.Administration";

        public const string Pages_Administration_Roles = "Pages.Administration.Roles";
        public const string Pages_Administration_Roles_Create = "Pages.Administration.Roles.Create";
        public const string Pages_Administration_Roles_Edit = "Pages.Administration.Roles.Edit";
        public const string Pages_Administration_Roles_Delete = "Pages.Administration.Roles.Delete";

        public const string Pages_Administration_Users = "Pages.Administration.Users";
        public const string Pages_Administration_Users_Create = "Pages.Administration.Users.Create";
        public const string Pages_Administration_Users_Edit = "Pages.Administration.Users.Edit";
        public const string Pages_Administration_Users_Delete = "Pages.Administration.Users.Delete";
        public const string Pages_Administration_Users_ChangePermissions = "Pages.Administration.Users.ChangePermissions";
        public const string Pages_Administration_Users_Impersonation = "Pages.Administration.Users.Impersonation";

        public const string Pages_Administration_Languages = "Pages.Administration.Languages";
        public const string Pages_Administration_Languages_Create = "Pages.Administration.Languages.Create";
        public const string Pages_Administration_Languages_Edit = "Pages.Administration.Languages.Edit";
        public const string Pages_Administration_Languages_Delete = "Pages.Administration.Languages.Delete";
        public const string Pages_Administration_Languages_ChangeTexts = "Pages.Administration.Languages.ChangeTexts";

        public const string Pages_Administration_AuditLogs = "Pages.Administration.AuditLogs";

        public const string Pages_Administration_OrganizationUnits = "Pages.Administration.OrganizationUnits";
        public const string Pages_Administration_OrganizationUnits_ManageOrganizationTree = "Pages.Administration.OrganizationUnits.ManageOrganizationTree";
        public const string Pages_Administration_OrganizationUnits_ManageMembers = "Pages.Administration.OrganizationUnits.ManageMembers";

        public const string Pages_Administration_HangfireDashboard = "Pages.Administration.HangfireDashboard";

        //TENANT-SPECIFIC PERMISSIONS

        public const string Pages_Tenant_Dashboard = "Pages.Tenant.Dashboard";

        public const string Pages_Administration_Tenant_Settings = "Pages.Administration.Tenant.Settings";

        public const string Pages_Administration_Tenant_SubscriptionManagement = "Pages.Administration.Tenant.SubscriptionManagement";

        //HOST-SPECIFIC PERMISSIONS

        public const string Pages_Editions = "Pages.Editions";
        public const string Pages_Editions_Create = "Pages.Editions.Create";
        public const string Pages_Editions_Edit = "Pages.Editions.Edit";
        public const string Pages_Editions_Delete = "Pages.Editions.Delete";

        public const string Pages_Tenants = "Pages.Tenants";
        public const string Pages_Tenants_Create = "Pages.Tenants.Create";
        public const string Pages_Tenants_Edit = "Pages.Tenants.Edit";
        public const string Pages_Tenants_ChangeFeatures = "Pages.Tenants.ChangeFeatures";
        public const string Pages_Tenants_Delete = "Pages.Tenants.Delete";
        public const string Pages_Tenants_Impersonation = "Pages.Tenants.Impersonation";

        public const string Pages_Administration_Host_Maintenance = "Pages.Administration.Host.Maintenance";
        public const string Pages_Administration_Host_Settings = "Pages.Administration.Host.Settings";
        public const string Pages_Administration_Host_Dashboard = "Pages.Administration.Host.Dashboard";

        //Spine specific permissions
        public const string Pages_Definitions = "Pages.Definitions";
        public const string Pages_Definitions_Options = "Pages.Definitions.Options";

        public const string Pages_Definitions_Options_Vendors = "Pages.Definitions.Options.Vendors";
        public const string Pages_Definitions_Options_Vendors_Create = "Pages.Definitions.Options.Vendors.Create";
        public const string Pages_Definitions_Options_Vendors_Edit = "Pages.Definitions.Options.Vendors.Edit";
        public const string Pages_Definitions_Options_Vendors_Delete = "Pages.Definitions.Options.Vendors.Delete";

        public const string Pages_Definitions_Options_ErrorCodes = "Pages.Definitions.ErrorCodes";
        public const string Pages_Definitions_Options_ErrorCodes_Create = "Pages.Definitions.ErrorCodes.Create";
        public const string Pages_Definitions_Options_ErrorCodes_Edit = "Pages.Definitions.ErrorCodes.Edit";
        public const string Pages_Definitions_Options_ErrorCodes_Delete = "Pages.Definitions.ErrorCodes.Delete";

        public const string Pages_Definitions_Options_Mounts = "Pages.Definitions.Mounts";
        public const string Pages_Definitions_Options_Mounts_Create = "Pages.Definitions.Mounts.Create";
        public const string Pages_Definitions_Options_Mounts_Edit = "Pages.Definitions.Mounts.Edit";
        public const string Pages_Definitions_Options_Mounts_Delete = "Pages.Definitions.Mounts.Delete";

        public const string Pages_Definitions_Options_Releases = "Pages.Definitions.Releases";
        public const string Pages_Definitions_Options_Releases_Create = "Pages.Definitions.Releases.Create";
        public const string Pages_Definitions_Options_Releases_Edit = "Pages.Definitions.Releases.Edit";
        public const string Pages_Definitions_Options_Releases_Delete = "Pages.Definitions.Releases.Delete";

        public const string Pages_Definitions_Options_Communications = "Pages.Definitions.Communication";
        public const string Pages_Definitions_Options_Communications_Create = "Pages.Definitions.Communication.Create";
        public const string Pages_Definitions_Options_Communications_Edit = "Pages.Definitions.Communication.Edit";
        public const string Pages_Definitions_Options_Communications_Delete = "Pages.Definitions.Communication.Delete";

        public const string Pages_Definitions_Options_Electrics = "Pages.Definitions.Electric";
        public const string Pages_Definitions_Options_Electrics_Create = "Pages.Definitions.Electric.Create";
        public const string Pages_Definitions_Options_Electrics_Edit = "Pages.Definitions.Electric.Edit";
        public const string Pages_Definitions_Options_Electrics_Delete = "Pages.Definitions.Electric.Delete";

        public const string Pages_Definitions_Options_Meters = "Pages.Definitions.Meters";
        public const string Pages_Definitions_Options_Meters_Create = "Pages.Definitions.Meters.Create";
        public const string Pages_Definitions_Options_Meters_Edit = "Pages.Definitions.Meters.Edit";
        public const string Pages_Definitions_Options_Meters_Delete = "Pages.Definitions.Meters.Delete";

        public const string Pages_Definitions_Options_Others = "Pages.Definitions.Other";
        public const string Pages_Definitions_Options_Others_Create = "Pages.Definitions.Other.Create";
        public const string Pages_Definitions_Options_Others_Edit = "Pages.Definitions.Other.Edit";
        public const string Pages_Definitions_Options_Others_Delete = "Pages.Definitions.Other.Delete";

        public const string Pages_Definitions_Options_Images = "Pages.Definitions.Images.";
        public const string Pages_Definitions_Options_Images_Create = "Pages.Definitions.Images..Create";
        public const string Pages_Definitions_Options_Images_Delete = "Pages.Definitions.Images.Delete";

        public const string Pages_Definitions_Options_Capacities = "Pages.Definitions.Capacities";
        public const string Pages_Definitions_Options_Capacities_Create = "Pages.Definitions.Capacities.Create";
        public const string Pages_Definitions_Options_Capacities_Edit = "Pages.Definitions.Capacities.Edit";
        public const string Pages_Definitions_Options_Capacities_Delete = "Pages.Definitions.Capacities.Delete";

        public const string Pages_Definitions_Options_AdminStatuses = "Pages.Definitions.AdminStatuses";
        public const string Pages_Definitions_Options_AdminStatuses_Create = "Pages.Definitions.AdminStatuses.Create";
        public const string Pages_Definitions_Options_AdminStatuses_Edit = "Pages.Definitions.AdminStatuses.Edit";
        public const string Pages_Definitions_Options_AdminStatuses_Delete = "Pages.Definitions.AdminStatuses.Delete";

        public const string Pages_Definitions_Options_OCPP = "Pages.Definitions.OCPP";
        public const string Pages_Definitions_Options_OCPP_Edit = "Pages.Definitions.OCPP.Edit";

        public const string Pages_Definitions_Solar = "Pages.Definitions.Solar";
        public const string Pages_Definitions_Wind = "Pages.Definitions.Wind";
        public const string Pages_Definitions_Storage = "Pages.Definitions.Storage";
        public const string Pages_Definitions_Grid = "Pages.Definitions.Grid";

        public const string Pages_Definitions_Chargers = "Pages.Definitions.Chargers";
        public const string Pages_Definitions_Chargers_Create = "Pages.Definitions.Chargers.Create";
        public const string Pages_Definitions_Chargers_Edit = "Pages.Definitions.Chargers.Edit";
        public const string Pages_Definitions_Chargers_Delete = "Pages.Definitions.Chargers.Delete";

        public const string Pages_Definitions_Chargers_Connectors = "Pages.Definitions.Chargers.Connectors";
        public const string Pages_Definitions_Chargers_Connectors_Create = "Pages.Definitions.Chargers.Connectors.Create";
        public const string Pages_Definitions_Chargers_Connectors_Edit = "Pages.Definitions.Chargers.Connectors.Edit";
        public const string Pages_Definitions_Chargers_Connectors_Delete = "Pages.Definitions.Chargers.Connectors.Delete";

        public const string Pages_Definitions_Chargers_OCPPConfigs = "Pages.Definitions.Chargers.OCPPConfigs";
        public const string Pages_Definitions_Chargers_OCPPConfigs_Edit = "Pages.Definitions.Chargers.OCPPConfigs.Edit";

        public const string Pages_Topology = "Pages.Topology";
        public const string Pages_Topology_Segments = "Pages.Topology.Segments";
        public const string Pages_Topology_Segments_Create = "Pages.Topology.Segments.Create";
        public const string Pages_Topology_Segments_Edit = "Pages.Topology.Segments.Edit";
        public const string Pages_Topology_Segments_Delete = "Pages.Topology.Segments.Delete";

        public const string Pages_Topology_Clients = "Pages.Topology.Clients";
        public const string Pages_Topology_Clients_Create = "Pages.Topology.Clients.Create";
        public const string Pages_Topology_Clients_Edit = "Pages.Topology.Clients.Edit";
        public const string Pages_Topology_Clients_Delete = "Pages.Topology.Clients.Delete";

        public const string Pages_Topology_Countries = "Pages.Topology.Countries";
        public const string Pages_Topology_Countries_Create = "Pages.Topology.Countries.Create";
        public const string Pages_Topology_Countries_Edit = "Pages.Topology.Countries.Edit";
        public const string Pages_Topology_Countries_Delete = "Pages.Topology.Countries.Delete";

        public const string Pages_Topology_Regions = "Pages.Topology.Regions";
        public const string Pages_Topology_Regions_Create = "Pages.Topology.Regions.Create";
        public const string Pages_Topology_Regions_Edit = "Pages.Topology.Regions.Edit";
        public const string Pages_Topology_Regions_Delete = "Pages.Topology.Regions.Delete";

        public const string Pages_Topology_Installs = "Pages.Topology.Installs";
        public const string Pages_Topology_Installs_Create = "Pages.Topology.Installs.Create";
        public const string Pages_Topology_Installs_Edit = "Pages.Topology.Installs.Edit";
        public const string Pages_Topology_Installs_Delete = "Pages.Topology.Installs.Delete";

        public const string Pages_Topology_Groups = "Pages.Topology.Groups";
        public const string Pages_Topology_Groups_Create = "Pages.Topology.Groups.Create";
        public const string Pages_Topology_Groups_Edit = "Pages.Topology.Groups.Edit";
        public const string Pages_Topology_Groups_Delete = "Pages.Topology.Groups.Delete";

        public const string Pages_Topology_ChargePoints = "Pages.Topology.ChargePoints";
        public const string Pages_Topology_ChargePoints_Boxes = "Pages.Topology.ChargePoints.Boxes";
        public const string Pages_Topology_ChargePoints_Boxes_Create = "Pages.Topology.ChargePoints.Boxes.Create";
        public const string Pages_Topology_ChargePoints_Boxes_Edit = "Pages.Topology.ChargePoints.Boxes.Edit";
        public const string Pages_Topology_ChargePoints_Boxes_Delete = "Pages.Topology.ChargePoints.Boxes.Delete";

        public const string Pages_Topology_ChargePoints_Connectors = "Pages.Topology.ChargePoints.Connectors";
        public const string Pages_Topology_ChargePoints_Connectors_Create = "Pages.Topology.ChargePoints.Connectors.Create";
        public const string Pages_Topology_ChargePoints_Connectors_Edit = "Pages.Topology.ChargePoints.Connectors.Edit";
        public const string Pages_Topology_ChargePoints_Connectors_Delete = "Pages.Topology.ChargePoints.Connectors.Delete";

        public const string Pages_Topology_ChargePoints_OCPPConfig = "Pages.Topology.ChargePoints.OCPPConfig";
        public const string Pages_Topology_ChargePoints_OCPPConfig_Edit = "Pages.Topology.ChargePoints.OCPPConfig.Edit";

        public const string Pages_Tags = "Pages.Tags";
        public const string Pages_Tags_ParentTags = "Pages.Tags.ParentTags";
        public const string Pages_Tags_ParentTags_Create = "Pages.Tags.ParentTags.Create";
        public const string Pages_Tags_ParentTags_Edit = "Pages.Tags.ParentTags.Edit";
        public const string Pages_Tags_ParentTags_Delete = "Pages.Tags.ParentTags.Delete";

        public const string Pages_Tags_UserTags = "Pages.Tags.UserTags";
        public const string Pages_Tags_UserTags_Create = "Pages.Tags.UserTags.Create";
        public const string Pages_Tags_UserTags_Edit = "Pages.Tags.UserTags.Edit";
        public const string Pages_Tags_UserTags_Delete = "Pages.Tags.UserTags.Delete";

        public const string Pages_Operation = "Pages.Operation";
        public const string Pages_Operation_Overview = "Pages.Operation.Overview";
        public const string Pages_Operation_Transactions = "Pages.Operation.Transactions";
        public const string Pages_Operation_Reservations = "Pages.Operation.Reservations";
        public const string Pages_Operation_OCPPMessages = "Pages.Operation.OCPPMessages";

        public const string Pages_Grants = "Pages.Grants";
        public const string Pages_Grants_Create = "Pages.Grants.Create";
        public const string Pages_Grants_Edit = "Pages.Grants.Edit";
        public const string Pages_Grants_Delete = "Pages.Grants.Delete";

        public const string Pages_UserRuleSets = "Pages.RuleSets";
        public const string Pages_UserRuleSets_View = "Pages.RuleSets.View";
        public const string Pages_UserRuleSets_Add = "Pages.RuleSets.Add";
        public const string Pages_UserRuleSets_Delete = "Pages.RuleSets.Delete";

        public const string Pages_Overview = "Pages.Overview";

        public const string Pages_LeaderBoard = "Pages.LeaderBoard";

        public const string Pages_Account = "Pages.Account";
        public const string Pages_Account_Create = "Pages.Account.Create";
        public const string Pages_Account_Edit = "Pages.Account.Edit";
        public const string Pages_Account_Delete = "Pages.Account.Delete";

        public const string Pages_KeyCards = "Pages.KeyCards";
        public const string Pages_KeyCards_Create = "Pages.KeyCards.Create";
        public const string Pages_KeyCards_Edit = "Pages.KeyCards.Edit";
        public const string Pages_KeyCards_Delete = "Pages.KeyCards.Delete";

        public const string Pages_PaymentSetup = "Pages.PaymentSetup";
        public const string Pages_PaymentSetup_Create = "Pages.PaymentSetup.Create";
        public const string Pages_PaymentSetup_Edit = "Pages.PaymentSetup.Edit";
        public const string Pages_PaymentSetup_Delete = "Pages.PaymentSetup.Delete";

        public const string Pages_UserTransactions = "Pages.UserTransactions";
        public const string Pages_UserTransactions_Create = "Pages.UserTransactions.Create";
        public const string Pages_UserTransactions_Edit = "Pages.UserTransactions.Edit";
        public const string Pages_UserTransactions_Delete = "Pages.UserTransactions.Delete";

        public const string Pages_Bills = "Pages.Bills";
        public const string Pages_Bills_Create = "Pages.Bills.Create";
        public const string Pages_Bills_Edit = "Pages.Bills.Edit";
        public const string Pages_Bills_Delete = "Pages.Bills.Delete";

        public const string Pages_Map = "Pages.Map";

        public const string Pages_UserChargepoints = "Pages.UserChargePoints";

        public const string Pages_Services = "Pages.Services";        
        public const string Pages_Services_Create = "Pages.Services.Create";
        public const string Pages_Services_Edit = "Pages.Services.Edit";
        public const string Pages_Services_Delete = "Pages.Services.Delete";

    }
}