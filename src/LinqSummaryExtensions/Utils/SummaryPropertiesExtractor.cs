using System;
using System.Linq;
using System.Reflection;

namespace LinqSummaryExtensions.Utils
{
    /// <summary>
    /// Utility that provides computable properties from type.
    /// </summary>
    internal static class SummaryPropertiesExtractor
    {
        private static readonly Type[] AllowedTypes =
        {
            typeof(int),
            typeof(long),
            typeof(float),
            typeof(double),
            typeof(decimal)
        };

        /// <summary>
        /// Получить поля для суммирования
        /// </summary>
        /// <param name="targetType">тип объекта</param>
        /// <returns>набор полей</returns>
        public static PropertyInfo[] GetSummaryObjectProperties(Type targetType)
        {
            var properties = targetType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (!targetType.IsAnonymousType())
            {
                properties = properties.Where(p => p.GetMethod != null && p.SetMethod != null)
                    .ToArray();
            }

            if (!properties.Any())
            {
                return new PropertyInfo[0];
            }

            return properties
                .Where(p => p.PropertyType.IsValueType)
                .Where(p =>
                {
                    var propertyType = p.PropertyType;
                    var typeToCheck = propertyType.IsGenericType
                        ? Nullable.GetUnderlyingType(propertyType)!
                        : propertyType;
                    return AllowedTypes.Contains(typeToCheck);
                })
                .ToArray();
        }
    }
}