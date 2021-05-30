using System;
using System.Runtime.CompilerServices;

namespace LinqSummaryExtensions
{
    internal static class TypeExtensions
    {
        public static bool IsAnonymousType(this Type type)
        {
            var hasCompilerGeneratedAttribute = Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false);
            var nameContainsAnonymousType = type.Name.Contains("AnonymousType");
            var hasRequiredFlags = !type.IsPublic && type.IsSealed && type.IsGenericType;
            return hasCompilerGeneratedAttribute && nameContainsAnonymousType && hasRequiredFlags;
        }

        public static object? GetDefaultValue(this Type type)
            => type.IsValueType
                ? Activator.CreateInstance(type)
                : null;

    }
}