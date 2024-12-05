using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class UpdateTypeCreator
    {
        public List<UpdateType> InitialUpdateTypes => GetInitialUpdateTypes();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<UpdateType> GetInitialUpdateTypes()
        {
            return new List<UpdateType>
            {
                new UpdateType() { Value = "Differential", Comment="Indicates that the current Local Authorization List must be updated with the values in this message.", TenantId = _tenantId },
                new UpdateType() { Value = "Full", Comment="Indicates that the current Local Authorization List must be replaced by the values in this message.", TenantId = _tenantId },
            };
        }

        public UpdateTypeCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateUpdateTypes();
        }

        private void CreateUpdateTypes()
        {
            foreach (var updateType in InitialUpdateTypes)
            {
                AddUpdateTypeIfNotExists(updateType);
            }
        }

        private void AddUpdateTypeIfNotExists(UpdateType updateType)
        {
            if (_context.UpdateTypes.Any(l => l.TenantId == _tenantId && l.Value == updateType.Value))
            {
                return;
            }

            _context.UpdateTypes.Add(updateType);

            _context.SaveChanges();
        }
    }
}
