using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomapperProjection
{
    public static class EnumExtensions
    {
        private static IDictionary<Type, ResourceManager> _resourceManagers =
            new ConcurrentDictionary<Type, ResourceManager>();

        public static string ToDisplayString(this Enum value, CultureInfo culture = null)
        {
            string description = value.ToString();

            ResourceManager resourceManager = null;

            if (_resourceManagers.TryGetValue(value.GetType(), out resourceManager) == false)
            {
                EnumDescriptionAttribute[] attributes =
                    (EnumDescriptionAttribute[]) value.GetType()
                        .GetCustomAttributes(typeof(EnumDescriptionAttribute), false);

                if (attributes.Any())
                {
                    Type resourceType = attributes[0].ResourceType;

                    if (resourceType != null)
                    {
                        var resourceManagerProperty = resourceType.GetProperty("ResourceManager");

                        if (resourceManagerProperty != null)
                        {
                            resourceManager = resourceManagerProperty.GetValue(null) as ResourceManager;

                            _resourceManagers.Add(value.GetType(), resourceManager);
                        }
                    }
                }
            }

            if (resourceManager != null)
            {
                return resourceManager.GetString(description, culture ?? Thread.CurrentThread.CurrentUICulture);
            }

            return description;
        }
    }
}