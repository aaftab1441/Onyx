using Sixoclock.Onyx.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sixoclock.Onyx.Migrations.Seed.OnyxSeed
{
    public class LocationCreator
    {
        public List<Location> InitialLocations => GetInitialLocations();

        private readonly OnyxDbContext _context;
        private readonly int _tenantId;

        private List<Location> GetInitialLocations()
        {
            return new List<Location>
            {
                new Location() { LocationName = "Body", Comment="Measurement inside body of Charge Point (e.g. Temperature)", TenantId = _tenantId },
                new Location() { LocationName = "Cable", Comment="Measurement taken from cable between EV and Charge Point", TenantId = _tenantId },
                new Location() { LocationName = "EV", Comment="Measurement taken by EV", TenantId = _tenantId },
                new Location() { LocationName = "Inlet", Comment="Measurement at network (\"grid\") inlet connection", TenantId = _tenantId },
                new Location() { LocationName = "Outlet", Comment="Measurement at a Connector. Default value", TenantId = _tenantId }
            };
        }

        public LocationCreator(OnyxDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateLocations();
        }

        private void CreateLocations()
        {
            foreach (var location in InitialLocations)
            {
                AddLocationIfNotExists(location);
            }
        }

        private void AddLocationIfNotExists(Location location)
        {
            if (_context.Locations.Any(l => l.TenantId == _tenantId && l.LocationName == location.LocationName))
            {
                return;
            }

            _context.Locations.Add(location);

            _context.SaveChanges();
        }
    }
}
