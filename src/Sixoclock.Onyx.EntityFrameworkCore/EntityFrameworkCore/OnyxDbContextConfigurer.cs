using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Sixoclock.Onyx.EntityFrameworkCore
{
    public static class OnyxDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<OnyxDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<OnyxDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}