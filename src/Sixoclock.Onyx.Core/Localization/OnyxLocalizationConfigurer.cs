using System.Reflection;
using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace Sixoclock.Onyx.Localization
{
    public static class OnyxLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    OnyxConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(OnyxLocalizationConfigurer).GetAssembly(),
                        "Sixoclock.Onyx.Localization.Onyx"
                    )
                )
            );
        }
    }
}