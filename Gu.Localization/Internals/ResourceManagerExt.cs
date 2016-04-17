﻿namespace Gu.Localization
{
    using System.Collections;
    using System.Globalization;
    using System.Linq;
    using System.Resources;
    using System.Threading;

    internal static class ResourceManagerExt
    {
        /// <summary>
        /// Check if the <paramref name="resourceManager"/> has a translation for <paramref name="key"/>
        /// </summary>
        /// <param name="resourceManager">The <see cref="ResourceManager"/></param>
        /// <param name="key">The key</param>
        /// <param name="culture">The <see cref="CultureInfo"/></param>
        /// <param name="createIfNotExits">true to load the resource set, if it has not been loaded yet; otherwise, false.</param>
        /// <returns>True if a translation exists</returns>
        internal static bool HasKey(
            this ResourceManager resourceManager,
            string key,
            CultureInfo culture,
            bool createIfNotExits)
        {
            if (createIfNotExits)
            {
                using (var set = resourceManager.GetResourceSet(culture, true, false))
                {
                    return set.OfType<DictionaryEntry>()
                              .Any(x => Equals(x.Key, key));
                }
            }

            return resourceManager.GetResourceSet(culture, false, false)
                                  .OfType<DictionaryEntry>()
                                  .Any(x => Equals(x.Key, key));
        }

        internal static bool HasCulture(this ResourceManager resourceManager, CultureInfo culture)
        {
            if (resourceManager.GetResourceSet(culture, false, false) != null)
            {
                return true;
            }

            using (var resourceSet = resourceManager.GetResourceSet(culture, true, false))
            {
                return resourceSet != null;
            }
        }
    }
}