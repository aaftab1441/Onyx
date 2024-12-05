using Sixoclock.Onyx.EntityFrameworkCore;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class InitialOnyxSeedBuilder
    {
        private readonly OnyxDbContext _context;

        public InitialOnyxSeedBuilder(OnyxDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            foreach(var tenant in _context.Tenants)
            {
                int id = tenant.Id;
                new AvailabilityTypesCreator(_context, id).Create();
                new AvailabilityStatusCreator(_context,id).Create();
                new BillingTypeCreator(_context,id).Create();
                new CountryCreator(_context, id).Create();
                new ConfigTypesCreator(_context,id).Create();
                new CurrencyCreator(_context,id).Create();
                new ConfigStatusCreator(_context,id).Create();
                new AuthorizationStatusCreator(_context, id).Create();
                new ResetTypeCreator(_context, id).Create();
                new ClearCacheStatusCreator(_context,id).Create();
                new ResetStatusCreator(_context, id).Create();
                new CancelReservationStatusCreator(_context, id).Create();
                new UnitCreator(_context, id).Create();
                new PowerCreator(_context, id).Create();
                new CapacityCreator(_context, id).Create();
                new ConnectorTypeCreator(_context, id).Create();
                new ContextCreator(_context, id).Create();
                new OCPPVersionCreator(_context, id).Create();
                new OCPPFeatureCreator(_context, id).Create();
                new FirmwareStatusCreator(_context, id).Create();
                new FormatCreator(_context, id).Create();
                new KeyValueCreator(_context, id).Create();
                new LocationCreator(_context, id).Create();
                new MeasurandCreator(_context, id).Create();
                new OCPPTransportCreator(_context, id).Create();
                new PhaseCreator(_context, id).Create();
                new ReasonCreator(_context, id).Create();
                new RegistrationStatusCreator(_context, id).Create();
                new ReservationStatusCreator(_context, id).Create();
                new TagTransationTypeCreator(_context, id).Create();
                new TagStatusCreator(_context, id).Create();
                new TransactionStatusCreator(_context, id).Create();
                new UnlockStatusCreator(_context, id).Create();
                new UpdateStatusCreator(_context, id).Create();
                new UpdateTypeCreator(_context, id).Create();
                new OCPPMessageCreator(_context, id).Create();
                new OCPPStatusCreator(_context, id).Create();
                new TransactionTypeCreator(_context, id).Create();
                new ConnectorStatusCodeCreator(_context, id).Create();
                new MeterValueTypeCreator(_context, id).Create();
                new ErrorCodeCreator(_context, id).Create();
                new RemoteStartStopEventTypeCreator(_context, id).Create();
                new RemoteStartStopStatusCreator(_context,id).Create();
                new GrantRuleConditionTypeCreator(_context,id).Create();
                new RuleConditionRelationCreator(_context,id).Create();
                new BillingStatusCreator(_context,id).Create();
            }
            
            _context.SaveChanges();
        }
    }
}
