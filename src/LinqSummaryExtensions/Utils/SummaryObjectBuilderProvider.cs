using System;
using System.Collections.Concurrent;
using LinqSummaryExtensions.Utils.ObjectBuilders;

namespace LinqSummaryExtensions.Utils
{
    /// <summary>
    /// Utility providing ready-to-use object-builders.
    /// </summary>
    internal static class SummaryObjectBuilderProvider
    {
        /// <summary> repository of already prepared builders </summary>
        private static readonly ConcurrentDictionary<Type, SummaryObjectBuilder> Cache;

        static SummaryObjectBuilderProvider()
        {
            Cache = new ConcurrentDictionary<Type, SummaryObjectBuilder>();
        }
        
        /// <summary> Provides object builder. </summary>
        /// <typeparam name="T"> summary object type </typeparam>
        /// <returns> instance of <see cref="SummaryObjectBuilder"/> </returns>
        internal static SummaryObjectBuilder GetBuilder<T>() where T : class
        {
            var targetType = typeof(T);

            if (Cache.TryGetValue(targetType, out var existBuilder))
            {
                return existBuilder;
            }

            var typeIsAnonymous = targetType.IsAnonymousType();
            
            var newBuilder = typeIsAnonymous
                ? (SummaryObjectBuilder) new AnonymousSummaryObjectTypeBuilder(targetType)
                : new CommonSummaryObjectTypeBuilder(targetType);

            return Cache.GetOrAdd(targetType, newBuilder);
        }
    }
}