using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using SampleWebStore.Authorization;

namespace SampleWebStore
{
    [DependsOn(
        typeof(SampleWebStoreCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class SampleWebStoreApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<SampleWebStoreAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(SampleWebStoreApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
