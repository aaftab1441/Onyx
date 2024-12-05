using Abp.Application.Navigation;
using Abp.Localization;
using Sixoclock.Onyx.Authorization;

namespace Sixoclock.Onyx.Web.Areas.App.Startup
{
    public class AppNavigationProvider : NavigationProvider
    {
        public const string MenuName = "App";

        public override void SetNavigation(INavigationProviderContext context)
        {
            var menu = context.Manager.Menus[MenuName] = new MenuDefinition(MenuName, new FixedLocalizableString("Main Menu"));

            menu
                .AddItem(new MenuItemDefinition(
                        AppPageNames.Host.Dashboard,
                        L("Dashboard"),
                        url: "App/HostDashboard",
                        icon: "icon-home",
                        requiredPermissionName: AppPermissions.Pages_Administration_Host_Dashboard
                    )
                ).AddItem(new MenuItemDefinition(
                    AppPageNames.Host.Tenants,
                    L("Tenants"),
                    url: "App/Tenants",
                    icon: "icon-globe",
                    requiredPermissionName: AppPermissions.Pages_Tenants
                    )
                ).AddItem(new MenuItemDefinition(
                        AppPageNames.Host.Editions,
                        L("Editions"),
                        url: "App/Editions",
                        icon: "icon-grid",
                        requiredPermissionName: AppPermissions.Pages_Editions
                    )
                ).AddItem(new MenuItemDefinition(
                        AppPageNames.Tenant.Dashboard,
                        L("Dashboard"),
                        url: "App/Dashboard",
                        icon: "icon-home",
                        requiredPermissionName: AppPermissions.Pages_Tenant_Dashboard
                    )
                ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.Administration,
                        L("Administration"),
                        icon: "icon-wrench"
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.OrganizationUnits,
                            L("OrganizationUnits"),
                            url: "App/OrganizationUnits",
                            icon: "icon-layers",
                            requiredPermissionName: AppPermissions.Pages_Administration_OrganizationUnits
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.Roles,
                            L("Roles"),
                            url: "App/Roles",
                            icon: "icon-briefcase",
                            requiredPermissionName: AppPermissions.Pages_Administration_Roles
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.Users,
                            L("Users"),
                            url: "App/Users",
                            icon: "icon-people",
                            requiredPermissionName: AppPermissions.Pages_Administration_Users
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.Languages,
                            L("Languages"),
                            url: "App/Languages",
                            icon: "icon-flag",
                            requiredPermissionName: AppPermissions.Pages_Administration_Languages
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.AuditLogs,
                            L("AuditLogs"),
                            url: "App/AuditLogs",
                            icon: "icon-lock",
                            requiredPermissionName: AppPermissions.Pages_Administration_AuditLogs
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Host.Maintenance,
                            L("Maintenance"),
                            url: "App/Maintenance",
                            icon: "icon-wrench",
                            requiredPermissionName: AppPermissions.Pages_Administration_Host_Maintenance
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Tenant.SubscriptionManagement,
                            L("Subscription"),
                            url: "App/SubscriptionManagement",
                            icon: "icon-refresh"
                            ,
                            requiredPermissionName: AppPermissions.Pages_Administration_Tenant_SubscriptionManagement
                        )
                    )
                    .AddItem(new MenuItemDefinition(
                            AppPageNames.Host.Settings,
                            L("Settings"),
                            url: "App/HostSettings",
                            icon: "icon-settings",
                            requiredPermissionName: AppPermissions.Pages_Administration_Host_Settings
                        )
                    ).AddItem(new MenuItemDefinition(
                            AppPageNames.Tenant.Settings,
                            L("Settings"),
                            url: "App/Settings",
                            icon: "icon-settings",
                            requiredPermissionName: AppPermissions.Pages_Administration_Tenant_Settings
                        )
                    )
                ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.DemoUiComponents,
                        L("DemoUiComponents"),
                        url: "App/DemoUiComponents",
                        icon: "icon-puzzle",
                        requiredPermissionName: AppPermissions.Pages_DemoUiComponents
                    )
                ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.Definitions,
                        L("Definitions"),
                        icon: "icon-wrench",
                        requiredPermissionName: AppPermissions.Pages_Definitions
                        ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.Solar,
                            L("Solar"),
                            url: "App/Solar",
                            icon: "icon-wrench",
                            requiredPermissionName: AppPermissions.Pages_Definitions_Solar
                            )
                        ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.Wind,
                            L("Wind"),
                            url: "App/Wind",
                            icon: "icon-wrench",
                            requiredPermissionName: AppPermissions.Pages_Definitions_Wind
                            )
                        ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.Storage,
                            L("Storage"),
                            url: "App/Storage",
                            icon: "icon-wrench",
                            requiredPermissionName: AppPermissions.Pages_Definitions_Storage
                            )
                        ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.Grid,
                            L("Grid"),
                            url: "App/Grid",
                            icon: "icon-wrench",
                            requiredPermissionName: AppPermissions.Pages_Definitions_Grid
                            )
                        ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.Chargers,
                            L("Chargers"),
                            icon: "icon-wrench",
                            requiredPermissionName: AppPermissions.Pages_Definitions_Chargers
                            ).AddItem(new MenuItemDefinition(
                                    AppPageNames.Common.Chargers,
                                    L("ModelChargepoints"),
                                    url: "App/ChargepointModels",
                                    icon: "icon-wrench",
                                    requiredPermissionName: AppPermissions.Pages_Definitions_Chargers
                                    )
                                ).AddItem(new MenuItemDefinition(
                                    AppPageNames.Common.ConfigKeys,
                                    L("EVSEs"),
                                    url: "App/ModelEVSEs",
                                    icon: "icon-people",
                                    requiredPermissionName: AppPermissions.Pages_Definitions_Chargers_Connectors
                                    )
                                ).AddItem(new MenuItemDefinition(
                                    AppPageNames.Common.ModelsConnectors,
                                    L("ModelConnectors"),
                                    url: "App/ModelConnectors",
                                    icon: "icon-people",
                                    requiredPermissionName: AppPermissions.Pages_Definitions_Chargers_Connectors
                                    )
                                ).AddItem(new MenuItemDefinition(
                                    AppPageNames.Common.ConfigKeys,
                                    L("ModelOCPPConfigs"),
                                    url: "App/ModelOCPPConfigs",
                                    icon: "icon-people",
                                    requiredPermissionName: AppPermissions.Pages_Definitions_Chargers_OCPPConfigs
                                    )
                                )
                            ).AddItem(new MenuItemDefinition(
                                AppPageNames.Common.Options,
                                L("Options"),
                                icon: "icon-wrench",
                                requiredPermissionName: AppPermissions.Pages_Definitions_Options
                                ).AddItem(new MenuItemDefinition(
                                    AppPageNames.Common.Vendors,
                                    L("Vendors"),
                                    url: "App/Vendors",
                                    icon: "icon-people",
                                    requiredPermissionName: AppPermissions.Pages_Definitions_Options_Vendors
                                    )
                                ).AddItem(new MenuItemDefinition(
                                    AppPageNames.Common.ErrorCodes,
                                    L("ErrorCodes"),
                                    url: "App/VendorErrorCodes",
                                    icon: "icon-people",
                                    requiredPermissionName: AppPermissions.Pages_Definitions_Options_ErrorCodes
                                    )
                                ).AddItem(new MenuItemDefinition(
                                    AppPageNames.Common.Mounts,
                                    L("Mounts"),
                                    url: "App/MountTypes",
                                    icon: "icon-people",
                                    requiredPermissionName: AppPermissions.Pages_Definitions_Options_Mounts
                                    )
                                ).AddItem(new MenuItemDefinition(
                                    AppPageNames.Common.ChargeReleaseOptions,
                                    L("Releases"),
                                    url: "App/ChargeReleaseOptions",
                                    icon: "icon-people",
                                    requiredPermissionName: AppPermissions.Pages_Definitions_Options_Releases
                                    )
                                ).AddItem(new MenuItemDefinition(
                                    AppPageNames.Common.ComOptions,
                                    L("Communication"),
                                    url: "App/ComOptions",
                                    icon: "icon-people",
                                    requiredPermissionName: AppPermissions.Pages_Definitions_Options_Communications
                                    )
                                ).AddItem(new MenuItemDefinition(
                                    AppPageNames.Common.ElectricOptions,
                                    L("Electric"),
                                    url: "App/ElectricalOptions",
                                    icon: "icon-people",
                                    requiredPermissionName: AppPermissions.Pages_Definitions_Options_Electrics
                                    )
                                ).AddItem(new MenuItemDefinition(
                                    AppPageNames.Common.Meters,
                                    L("Meters"),
                                    url: "App/MeterTypes",
                                    icon: "icon-people",
                                    requiredPermissionName: AppPermissions.Pages_Definitions_Options_Meters
                                    )
                                ).AddItem(new MenuItemDefinition(
                                    AppPageNames.Common.OtherOptions,
                                    L("Other"),
                                    url: "App/OtherOptions",
                                    icon: "icon-people",
                                    requiredPermissionName: AppPermissions.Pages_Definitions_Options_Others
                                    )
                                ).AddItem(new MenuItemDefinition(
                                    AppPageNames.Common.ChargepointModelImages,
                                    L("ChargepointModelImages"),
                                    url: "App/ChargepointModelImages",
                                    icon: "icon-people",
                                    requiredPermissionName: AppPermissions.Pages_Definitions_Options_Images
                                    )
                                ).AddItem(new MenuItemDefinition(
                                    AppPageNames.Common.Capacities,
                                    L("Capacities"),
                                    url: "App/Capacities",
                                    icon: "icon-people",
                                    requiredPermissionName: AppPermissions.Pages_Definitions_Options_Capacities
                                    )
                                ).AddItem(new MenuItemDefinition(
                                    AppPageNames.Common.AdminStatuses,
                                    L("AdminStatuses"),
                                    url: "App/AdminStatuses",
                                    icon: "icon-people",
                                    requiredPermissionName: AppPermissions.Pages_Definitions_Options_AdminStatuses
                                    )
                                ).AddItem(new MenuItemDefinition(
                                    AppPageNames.Common.ConfigKeys,
                                    L("ConfigKeys"),
                                    url: "App/Configs",
                                    icon: "icon-people",
                                    requiredPermissionName: AppPermissions.Pages_Definitions_Options_OCPP
                                    )
                            )
                        )
                ).AddItem(new MenuItemDefinition(
                    AppPageNames.Common.Topology,
                    L("Topology"),
                    icon: "icon-wrench",
                    requiredPermissionName: AppPermissions.Pages_Topology
                    ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.Segment,
                        L("Segment"),
                        url: "App/Segments",
                        icon: "icon-people",
                        requiredPermissionName: AppPermissions.Pages_Topology_Segments
                        )
                    ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.Customer,
                        L("Customer"),
                        url: "App/Customer",
                        icon: "icon-people",
                        requiredPermissionName: AppPermissions.Pages_Topology_Clients
                        )
                    ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.Market,
                        L("Markets"),
                        url: "App/Markets",
                        icon: "icon-flag",
                        requiredPermissionName: AppPermissions.Pages_Topology_Countries
                        )
                    ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.Region,
                        L("Regions"),
                        url: "App/Regions",
                        icon: "icon-people",
                        requiredPermissionName: AppPermissions.Pages_Topology_Regions
                        )
                    ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.Install,
                        L("Installs"),
                        url: "App/Installs",
                        icon: "icon-wrench",
                        requiredPermissionName: AppPermissions.Pages_Topology_Installs
                        )
                    ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.Group,
                        L("Groups"),
                        url: "App/Groups",
                        icon: "icon-wrench",
                        requiredPermissionName: AppPermissions.Pages_Topology_Groups
                        )
                     )
                ).AddItem(new MenuItemDefinition(
                    AppPageNames.Common.ChargePoint,
                    L("ChargePoints"),
                    icon: "icon-wrench",
                    requiredPermissionName: AppPermissions.Pages_Topology_ChargePoints
                    ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.ChargePointBox,
                        L("Boxes"),
                        url: "App/ChargePoints",
                        icon: "icon-people",
                        requiredPermissionName: AppPermissions.Pages_Topology_ChargePoints_Boxes
                        )
                    ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.ChargePointEVSEs,
                        L("EVSEs"),
                        url: "App/EVSEs",
                        icon: "icon-people",
                        requiredPermissionName: AppPermissions.Pages_Topology_Clients
                        )
                    ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.ChargePointConnectors,
                        L("Connectors"),
                        url: "App/Connectors",
                        icon: "icon-flag",
                        requiredPermissionName: AppPermissions.Pages_Topology_Countries
                        )
                    ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.ChargePointOCPPConfig,
                        L("BoxOCPPConfigs"),
                        url: "App/ChargepointOCPPConfigs",
                        icon: "icon-people",
                        requiredPermissionName: AppPermissions.Pages_Topology_ChargePoints_OCPPConfig
                        )
                    )
                ).AddItem(new MenuItemDefinition(
                    AppPageNames.Common.TagManagement,
                    L("TagManagement"),
                    icon: "icon-wrench",
                    requiredPermissionName: AppPermissions.Pages_Tags
                    ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.ParentTags,
                        L("ParentTags"),
                        url: "App/ParentTags",
                        icon: "icon-people",
                        requiredPermissionName: AppPermissions.Pages_Tags_ParentTags
                        )
                    ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.UserTags,
                        L("UserTags"),
                        url: "App/Tags",
                        icon: "icon-people",
                        requiredPermissionName: AppPermissions.Pages_Tags_UserTags
                        )
                    )
                    ).AddItem(new MenuItemDefinition(
                        AppPageNames.Common.Operation,
                        L("Operation"),
                        icon: "icon-people",
                        requiredPermissionName: AppPermissions.Pages_Operation
                         ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.OperationOverview,
                                L("OperationOverview"),
                                url: "App/OperationOverview",
                                icon: "icon-people",
                                requiredPermissionName: AppPermissions.Pages_Operation_Overview
                                )
                            ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.Transactions,
                                L("Transactions"),
                                url: "App/TenantTransactions",
                                icon: "icon-people",
                                requiredPermissionName: AppPermissions.Pages_Operation_Transactions
                                )
                            ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.Reservations,
                                L("Reservations"),
                                url: "App/Reservations",
                                icon: "icon-people",
                                requiredPermissionName: AppPermissions.Pages_Operation_Reservations
                                )
                            ).AddItem(new MenuItemDefinition(
                            AppPageNames.Common.OCPPMessages,
                                L("OCPPMessages"),
                                url: "App/OCPPMessages",
                                icon: "icon-people",
                                requiredPermissionName: AppPermissions.Pages_Operation_OCPPMessages
                                )
                            )
                ).AddItem(new MenuItemDefinition(
                    AppPageNames.Common.Grants,
                    L("Grants"),
                    url: "App/Grants",
                    icon: "icon-globe",
                    requiredPermissionName: AppPermissions.Pages_Grants
                    )
                 ).AddItem(new MenuItemDefinition(
                    AppPageNames.Common.Overview,
                    L("Overview"),
                    url: "App/Overview",
                    icon: "icon-globe",
                    requiredPermissionName: AppPermissions.Pages_Overview
                    )
                 ).AddItem(new MenuItemDefinition(
                    AppPageNames.Common.LeaderBoard,
                    L("LeaderBoard"),
                    url: "App/LeaderBoard",
                    icon: "icon-globe",
                    requiredPermissionName: AppPermissions.Pages_LeaderBoard
                    )
                ).AddItem(new MenuItemDefinition(
                    AppPageNames.Common.Account,
                    L("Account"),
                    url: "App/Account",
                    icon: "icon-globe",
                    requiredPermissionName: AppPermissions.Pages_Account
                    )
                 ).AddItem(new MenuItemDefinition(
                    AppPageNames.Common.KeyCards,
                    L("KeyCards"),
                    url: "App/KeyCards",
                    icon: "icon-globe",
                    requiredPermissionName: AppPermissions.Pages_KeyCards
                    )
                 ).AddItem(new MenuItemDefinition(
                    AppPageNames.Common.PaymentSetup,
                    L("PaymentSetup"),
                    url: "App/PaymentSetup",
                    icon: "icon-globe",
                    requiredPermissionName: AppPermissions.Pages_PaymentSetup
                    )
                 ).AddItem(new MenuItemDefinition(
                    AppPageNames.Common.UserTransactions,
                    L("UserTransactions"),
                    url: "App/UserTransactions",
                    icon: "icon-globe",
                    requiredPermissionName: AppPermissions.Pages_UserTransactions
                    )
                 ).AddItem(new MenuItemDefinition(
                    AppPageNames.Common.Bills,
                    L("Bills"),
                    url: "App/Bills",
                    icon: "icon-globe",
                    requiredPermissionName: AppPermissions.Pages_Bills
                    )
                 ).AddItem(new MenuItemDefinition(
                    AppPageNames.Common.Map,
                    L("Map"),
                    url: "App/Map",
                    icon: "icon-globe",
                    requiredPermissionName: AppPermissions.Pages_Map
                    )
                 ).AddItem(new MenuItemDefinition(
                    AppPageNames.Common.UserChargepoints,
                    L("UserChargePoints"),
                    url: "App/UserChargepoints",
                    icon: "icon-globe",
                    requiredPermissionName: AppPermissions.Pages_UserChargepoints
                    )
                 ).AddItem(new MenuItemDefinition(
                    AppPageNames.Common.Services,
                    L("Services"),
                    url: "App/Services",
                    icon: "icon-globe",
                    requiredPermissionName: AppPermissions.Pages_Services
                    ))

                    ;
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, OnyxConsts.LocalizationSourceName);
        }
    }
}