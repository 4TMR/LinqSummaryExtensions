using System;
using System.Linq;
using System.Reflection;
using LinqSummaryExtensions.Models;

namespace LinqSummaryExtensions.Utils
{
    /// <summary>
    /// Utility for obtaining data for building an object.
    /// </summary>
    internal static class ObjectConstructInfoProvider
    {
        /// <summary> Receive data about the construction of an object. </summary>
        /// <param name="targetType"> object type </param>
        /// <exception cref="Exception"> if object type doesn't have any useful ctor </exception>
        public static ObjectConstructInfo GetInfo(Type targetType)
        {
            var isAnonymousType = targetType.IsAnonymousType();
            var ctor = GetAvailableConstructor(targetType, isAnonymousType);
            
            if (ctor is null)
            {
                throw new Exception(
                    "The target type must be anonymous type or a user-defined type with a public parameterless constructor.");
            }

            return new ObjectConstructInfo(ctor);
        }

        private static ConstructorInfo? GetAvailableConstructor(Type type, bool forAnonymous = false)
        {
            var publicConstructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public);

            return forAnonymous
                ? publicConstructors[0]//anonymous-types have only one ctor with all defined fields
                : publicConstructors.FirstOrDefault(c => c.GetParameters().Length == 0);
        }
    }
}