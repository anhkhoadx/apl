using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace SampleWebStore.Localization
{
    public static class SampleWebStoreLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(SampleWebStoreConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(SampleWebStoreLocalizationConfigurer).GetAssembly(),
                        "SampleWebStore.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
