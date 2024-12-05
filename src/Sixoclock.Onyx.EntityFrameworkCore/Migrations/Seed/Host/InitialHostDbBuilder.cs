using Sixoclock.Onyx.EntityFrameworkCore;

namespace Sixoclock.Onyx.Migrations.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly OnyxDbContext _context;

        public InitialHostDbBuilder(OnyxDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
