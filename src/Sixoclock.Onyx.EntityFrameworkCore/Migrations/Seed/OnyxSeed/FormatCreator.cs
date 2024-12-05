using Sixoclock.Onyx.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class FormatCreator
    {
        public List<Format> InitialFormates => GetInitialFormates();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<Format> GetInitialFormates()
        {
            return new List<Format>
            {
                new Format() { FormatType = "Raw", Comment = "Data is to be interpreted as integer/decimal numeric data.", TenantId = _tenantId },
                new Format() { FormatType = "SignedData", Comment = "Data is represented as a signed binary data block, encoded as hex data.", TenantId = _tenantId }
            };
        }

        public FormatCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateFormates();
        }

        private void CreateFormates()
        {
            foreach (var format in InitialFormates)
            {
                AddFormatIfNotExists(format);
            }
        }

        private void AddFormatIfNotExists(Format format)
        {
            if (_context.Formats.Any(l => l.TenantId == _tenantId && l.FormatType == format.FormatType))
            {
                return;
            }

            _context.Formats.Add(format);

            _context.SaveChanges();
        }
    }
}
