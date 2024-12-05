namespace Sixoclock.Onyx.Web.Areas.App.Startup
{
    public class AppPageNames
    {
        public static class Common
        {
            public const string Administration = "Administration";
            public const string Roles = "Administration.Roles";
            public const string Users = "Administration.Users";
            public const string AuditLogs = "Administration.AuditLogs";
            public const string OrganizationUnits = "Administration.OrganizationUnits";
            public const string Languages = "Administration.Languages";
            public const string DemoUiComponents = "Administration.DemoUiComponents";
            public const string Grants = "Administration.Grants";
            public const string Overview = "Administration.Overview";
            public const string LeaderBoard = "Administration.LeaderBoard";
            public const string Account = "Administration.Account";
            public const string KeyCards = "Administration.KeyCards";
            public const string PaymentSetup = "Administration.PaymentSetup";
            public const string UserTransactions = "Administration.UserTransactions";
            public const string Bills = "Administration.Bills";
            public const string Map = "Administration.Map";
            public const string UserChargepoints = "Administration.UserChargepoints";
            public const string Services = "Administration.Services";


            public const string Definitions = "Definitions";
            public const string Solar = "Solar";
            public const string Wind = "Wind";
            public const string Storage = "Storage";
            public const string Grid = "Grid";
            public const string Chargers= "Chargers";
            public const string Models = "Chargers.ChargepointModels";
            public const string ModelsConnectors = "Chargers.ModelConnectors";
            public const string ModelEVSEs = "Chargers.ModelEVSEs";
            public const string ModelsOCPPConfig = "Chargers.ModelOCPPConfig";

            public const string Topology = "Topology";
            public const string Segment = "Topology.Segment";
            public const string Customer = "Topology.Customer";
            public const string Market = "Topology.Market";
            public const string Region = "Topology.Region";
            public const string Install = "Topology.Install";
            public const string Group = "Topology.Group";
            public const string ChargePoint = "ChargePoint";
            public const string ChargePointBox = "ChargePoint.Box";
            public const string ChargePointEVSEs = "ChargePoint.EVSEs";
            public const string ChargePointConnectors = "ChargePoint.Connectors";
            public const string ChargePointOCPPConfig = "ChargePoint.OCPPConfig";

            public const string Connector = "Topology.Connector";

            public const string TagManagement = "TagManagement";
            public const string ParentTags = "TagManagement.ParentTags";
            public const string UserTags = "TagManagement.UserTags";

            public const string Operation = "OperationOverview.Operation";
            public const string OperationOverview = "OperationOverview";
            public const string Transactions = "OperationOverview.Transactions";
            public const string Reservations = "OperationOverview.Reservations";
            public const string OCPPMessages = "OperationOverview.OCPPMessages";

            public const string Options = "Options";
            public const string ChargeReleaseOptions = "Options.ChargeReleaseOptions";
            public const string ComOptions = "Options.Communication";
            public const string ElectricOptions = "Options.Electric";
            public const string Meters = "Options.Meters";
            public const string OtherOptions = "Options.Other";
            public const string Vendors = "Options.Vendors";
            public const string ErrorCodes = "Options.ErrorCodes";
            public const string Mounts = "Options.Mounts";
            public const string ChargepointModelImages = "Options.Images";
            public const string Capacities = "Options.Capacities";
            public const string AdminStatuses = "Options.Statuses";
            public const string ConfigKeys = "Options.ConfigKeys";
            

        }

        public static class Host
        {
            public const string Tenants = "Tenants";
            public const string Editions = "Editions";
            public const string Maintenance = "Administration.Maintenance";
            public const string Settings = "Administration.Settings.Host";
            public const string Dashboard = "Dashboard";
        }

        public static class Tenant
        {
            public const string Dashboard = "Dashboard.Tenant";
            public const string Settings = "Administration.Settings.Tenant";
            public const string SubscriptionManagement = "Administration.SubscriptionManagement.Tenant";
        }
    }
}
