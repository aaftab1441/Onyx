using Abp.IdentityServer4;
using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sixoclock.Onyx.Authorization.Roles;
using Sixoclock.Onyx.Authorization.Users;
using Sixoclock.Onyx.Chat;
using Sixoclock.Onyx.Editions;
using Sixoclock.Onyx.Friendships;
using Sixoclock.Onyx.Grants;
using Sixoclock.Onyx.MultiTenancy;
using Sixoclock.Onyx.MultiTenancy.Accounting;
using Sixoclock.Onyx.MultiTenancy.Payments;
using Sixoclock.Onyx.Storage;

namespace Sixoclock.Onyx.EntityFrameworkCore
{
    public class OnyxDbContext : AbpZeroDbContext<Tenant, Role, User, OnyxDbContext>, IAbpPersistedGrantDbContext
    {
        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Country> Countries { get; set; }

        public virtual DbSet<Market> Markets { get; set; }

        public virtual DbSet<Region> Regions { get; set; }

        public virtual DbSet<Install> Installs { get; set; }

        public virtual DbSet<Group> Groups { get; set; }

        public virtual DbSet<Segment> Segments { get; set; }

        public virtual DbSet<ChargepointModel> ChargepointModels { get; set; }

        public virtual DbSet<ChargeReleaseOption> ChargeReleaseOptions { get; set; }

        public virtual DbSet<ReleaseOptionModel> ReleaseOptionModels { get; set; }

        public virtual DbSet<ComOption> ComOptions { get; set; }

        public virtual DbSet<ComOptionModel> ComOptionModels { get; set; }

        public virtual DbSet<ElectricalOption> ElectricalOptions { get; set; }

        public virtual DbSet<ElectricalOptionModel> ElectricalOptionModels { get; set; }

        public virtual DbSet<OtherOption> OtherOptions { get; set; }

        public virtual DbSet<OtherOptionModel> OtherOptionModels { get; set; }

        public virtual DbSet<Vendor> Vendors { get; set; }

        public virtual DbSet<VendorErrorCode> VendorErrorCodes { get; set; }

        public virtual DbSet<MountType> MountTypes { get; set; }

        public virtual DbSet<ChargepointModelImage> ChargepointModelImages { get; set; }

        public virtual DbSet<AvailabilityType> AvailabilityTypes { get; set; }

        public virtual DbSet<AvailabilityStatus> AvailabilityStatuses { get; set; }

        public virtual DbSet<AvailabilityEvent> AvailabilityEvents { get; set; }

        public virtual DbSet<AuthorizationStatus> AuthorizationStatuses { get; set; }

        public virtual DbSet<ResetType> ResetTypes { get; set; }

        public virtual DbSet<ResetStatus> ResetStatuses { get; set; }

        public virtual DbSet<CancelReservationStatus> CancelReservationStatuses { get; set; }

        public virtual DbSet<Unit> Units { get; set; }

        public virtual DbSet<Capacity> Capacities { get; set; }

        public virtual DbSet<Power> Powers { get; set; }

        public virtual DbSet<ConnectorType> ConnectorTypes { get; set; }

        public virtual DbSet<Context> Contexts { get; set; }

        public virtual DbSet<OCPPVersion> OCPPVersions { get; set; }

        public virtual DbSet<OCPPFeature> OCPPFeatures { get; set; }

        public virtual DbSet<FirmwareStatus> FirmwareStatuses { get; set; }

        public virtual DbSet<Format> Formats { get; set; }

        public virtual DbSet<KeyValue> KeyValues { get; set; }

        public virtual DbSet<Location> Locations { get; set; }

        public virtual DbSet<Measurand> Measurands { get; set; }

        public virtual DbSet<OCPPTransport> OCPPTransports { get; set; }

        public virtual DbSet<Phase> Phases { get; set; }

        public virtual DbSet<Reason> Reasons { get; set; }

        public virtual DbSet<RegistrationStatus> RegistrationStatuses { get; set; }

        public virtual DbSet<ReservationStatus> ReservationStatuses { get; set; }

        public virtual DbSet<TagTransactionType> TagTransactionType { get; set; }

        public virtual DbSet<TransactionStatus> TransactionStatuses { get; set; }

        public virtual DbSet<UnlockStatus> UnlockStatuses { get; set; }

        public virtual DbSet<UnlockEvent> UnlockEvents { get; set; }

        public virtual DbSet<ClearCacheStatus> ClearCacheStatuses { get; set; }

        public virtual DbSet<ClearCacheEvent> ClearCacheEvents { get; set; }

        public virtual DbSet<UpdateStatus> UpdateStatuses { get; set; }

        public virtual DbSet<UpdateType> UpdateTypes { get; set; }

        public virtual DbSet<LangDescription> LangDescriptions { get; set; }

        public virtual DbSet<AdminStatus> AdminStatuses { get; set; }

        public virtual DbSet<AdminStatusEvent> AdminStatusEvents { get; set; }

        public virtual DbSet<ErrorCode> ErrorCodes { get; set; }

        public virtual DbSet<EVSEStatus> EVSEStatuses { get; set; }

        public virtual DbSet<Chargepoint> Chargepoints { get; set; }

        public virtual DbSet<ChargepointFeature> ChargepointFeatures { get; set; }

        public virtual DbSet<ChargepointKeyValue> ChargepointKeyValues { get; set; }

        public virtual DbSet<ModelFeature> ModelFeatures { get; set; }

        public virtual DbSet<ModelKeyValue> ModelKeyValues { get; set; }

        public virtual DbSet<MeterType> MeterTypes { get; set; }

        public virtual DbSet<Reservation> Reservations { get; set; }

        public virtual DbSet<ReservationEventType> ReservationEventTypes { get; set; }

        public virtual DbSet<ReservationEvent> ReservationEvents { get; set; }

        public virtual DbSet<Heartbeat> Heartbeats { get; set; }

        public virtual DbSet<ParentTag> ParentTag { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }

        public virtual DbSet<TagStatus> TagStatuses { get; set; }

        public virtual DbSet<AuthTagMember> AuthTagMembers { get; set; }

        public virtual DbSet<LocalAuthList> LocalAuthLists { get; set; }

        public virtual DbSet<TagTransaction> TagTransactions { get; set; }

        public virtual DbSet<Transaction> Transactions { get; set; }

        public virtual DbSet<RemoteStartStopEventType> RemoteStartStopEventTypes { get; set; }

        public virtual DbSet<RemoteStartStopEvent> RemoteStartStopEvents { get; set; }

        public virtual DbSet<RemoteStartStopStatus> RemoteStartStopStatuses { get; set; }

        public virtual DbSet<MeterValue> MeterValues { get; set; }

        public virtual DbSet<MeterValueType> MeterValueTypes { get; set; }

        public virtual DbSet<ResetEvent> ResetEvents { get; set; }

        public virtual DbSet<TransactionType> TransactionTypes { get; set; }

        public virtual DbSet<EventType> EventTypes { get; set; }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<OCPPStatus> OCPPStatuses { get; set; }

        public virtual DbSet<OCPPMessageEvent> OCPPMessageEvents { get; set; }

        public virtual DbSet<OCPPMessage> OCPPMessages { get; set; }

        public virtual DbSet<ConnectorStatusCode> ConnectorStatusCodes { get; set; }

        public virtual DbSet<ModelEVSE> ModelEVSEs { get; set; }

        public virtual DbSet<EVSE> EVSEs { get; set; }

        public virtual DbSet<Connector> Connectors { get; set; }

        public virtual DbSet<ModelConnector> ModelConnectors { get; set; }
        
        public virtual DbSet<ConfigType> ConfigTypes { get; set; }

        public virtual DbSet<ConfigStatus> ConfigStatuses { get; set; }

        public virtual DbSet<ConfigEvent> ConfigEvents { get; set; }

        public virtual DbSet<BillingType> BillingTypes { get; set; }
        public virtual DbSet<BillingStatus> BillingStatuses { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServicePriceParameter> ServicePriceParameters { get; set; }
        public virtual DbSet<SegmentService> SegmentServices { get; set; }
        public virtual DbSet<SegmentServicePriceParameter> SegmentServicePriceParameters { get; set; }
        public virtual DbSet<CustomerService> CustomerServices { get; set; }
        public virtual DbSet<CustomerServicePriceParameter> CustomerServicePriceParameters { get; set; }
        public virtual DbSet<MarketService> MarketServices { get; set; }
        public virtual DbSet<MarketServicePriceParameter> MarketServicePriceParameters { get; set; }
        public virtual DbSet<RegionService> RegionServices { get; set; }
        public virtual DbSet<RegionServicePriceParameter> RegionServicePriceParameters { get; set; }
        public virtual DbSet<InstallService> InstallServices { get; set; }
        public virtual DbSet<InstallServicePriceParameter> InstallServicePriceParameters { get; set; }
        public virtual DbSet<GroupService> GroupServices { get; set; }
        public virtual DbSet<GroupServicePriceParameter> GroupServicePriceParameters { get; set; }
        public virtual DbSet<ChargepointService> ChargepointServices { get; set; }
        public virtual DbSet<ChargepointServicePriceParameter> ChargepointServicePriceParameters { get; set; }


        public virtual DbSet<Currency> Currencies { get; set; }

        public virtual DbSet<BinaryObject> BinaryObjects { get; set; }

        public virtual DbSet<Friendship> Friendships { get; set; }

        public virtual DbSet<ChatMessage> ChatMessages { get; set; }

        public virtual DbSet<SubscribableEdition> SubscribableEditions { get; set; }

        public virtual DbSet<SubscriptionPayment> SubscriptionPayments { get; set; }

        public virtual DbSet<Invoice> Invoices { get; set; }

        public DbSet<PersistedGrantEntity> PersistedGrants { get; set; }
        //Onyx Grants
        public DbSet<RuleCondition> RuleConditions { get; set; }
        public virtual DbSet<RuleRelation> RuleRelations { get; set; }
        //public virtual DbSet<SegmentRuleCondition> SegmentRuleConditions { get; set; }
        public virtual DbSet<Rule> Rules { get; set; }
        public virtual DbSet<RuleSet> RuleSets { get; set; }
        public virtual DbSet<UserRuleSet> UserRuleSets { get; set; }


        public OnyxDbContext(DbContextOptions<OnyxDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BinaryObject>(b =>
            {
                b.HasIndex(e => new { e.TenantId });
            });

            modelBuilder.Entity<ChatMessage>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId, e.ReadState });
                b.HasIndex(e => new { e.TenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.TargetUserId, e.ReadState });
                b.HasIndex(e => new { e.TargetTenantId, e.UserId, e.ReadState });
            });

            modelBuilder.Entity<Friendship>(b =>
            {
                b.HasIndex(e => new { e.TenantId, e.UserId });
                b.HasIndex(e => new { e.TenantId, e.FriendUserId });
                b.HasIndex(e => new { e.FriendTenantId, e.UserId });
                b.HasIndex(e => new { e.FriendTenantId, e.FriendUserId });
            });

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasIndex(e => new { e.SubscriptionEndDateUtc });
                b.HasIndex(e => new { e.CreationTime });
            });

            modelBuilder.Entity<SubscriptionPayment>(b =>
            {
                b.HasIndex(e => new { e.Status, e.CreationTime });
                b.HasIndex(e => new { e.PaymentId, e.Gateway });
            });

            modelBuilder.Entity<EVSE>()
                        .HasOne(a => a.EVSEStatus)
                        .WithOne(b => b.EVSE);

            modelBuilder.ConfigurePersistedGrantEntity();
        }
    }
}
