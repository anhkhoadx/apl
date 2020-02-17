using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace SampleWebStore.EntityFrameworkCore
{
    public static class SampleWebStoreDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<SampleWebStoreDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<SampleWebStoreDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
