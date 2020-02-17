using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SampleWebStore.Configuration;
using SampleWebStore.Web;

namespace SampleWebStore.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class SampleWebStoreDbContextFactory : IDesignTimeDbContextFactory<SampleWebStoreDbContext>
    {
        public SampleWebStoreDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SampleWebStoreDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            SampleWebStoreDbContextConfigurer.Configure(builder, configuration.GetConnectionString(SampleWebStoreConsts.ConnectionStringName));

            return new SampleWebStoreDbContext(builder.Options);
        }
    }
}
