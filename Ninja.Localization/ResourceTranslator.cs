﻿using System.Collections.Generic;
using Ninja.Localization.Resources;

namespace Ninja.Localization
{
    public static class ResourceTranslator
    {
        /// <summary>
        ///     Translate the resource name to the localized value. Returns the value if no translation is found.
        /// </summary>
        /// <param name="identifier">Identifier of the resource to translate.</param>
        /// <param name="value">Value of the resource to translate.</param>
        /// <returns>Localized value of the resource. Returns the value if no translation is found.</returns>
        public static string Translate(ResourceIdentifier identifier, object value)
        {
            return Strings.ResourceManager.GetString($"{identifier}_{value}",
                LocalizationManager.GetInstance().Culture) ?? value.ToString();
        }

        /// <summary>
        ///     Translate the resource name to the localized value. Returns the value if no translation is found.
        /// </summary>
        /// <param name="identifiers">List of identifiers of the resource to translate.</param>
        /// <param name="value">Value of the resource to translate.</param>
        /// <returns>Localized value of the resource. Returns the value if no translation is found.</returns>
        public static string Translate(IEnumerable<ResourceIdentifier> identifiers, object value)
        {
            foreach (var identifier in identifiers)
            {
                var foundResource = Strings.ResourceManager.GetString($"{identifier}_{value}",
                    LocalizationManager.GetInstance().Culture);

                if (foundResource != null)
                    return foundResource;
            }

            return value.ToString();
        }
    }
}