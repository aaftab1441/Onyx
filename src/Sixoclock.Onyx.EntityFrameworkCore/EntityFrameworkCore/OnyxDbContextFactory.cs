using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Sixoclock.Onyx.Configuration;
using Sixoclock.Onyx.Web;

namespace Sixoclock.Onyx.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class OnyxDbContextFactory : IDesignTimeDbContextFactory<OnyxDbContext>
    {
        public OnyxDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<OnyxDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            OnyxDbContextConfigurer.Configure(builder, configuration.GetConnectionString(OnyxConsts.ConnectionStringName));

            return new OnyxDbContext(builder.Options);
        }
    }
}