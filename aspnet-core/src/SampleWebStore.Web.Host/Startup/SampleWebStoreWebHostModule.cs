using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using SampleWebStore.Configuration;

namespace SampleWebStore.Web.Host.Startup
{
    [DependsOn(
       typeof(SampleWebStoreWebCoreModule))]
    public class SampleWebStoreWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public SampleWebStoreWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(SampleWebStoreWebHostModule).GetAssembly());
        }
    }
}
