using System;
using System.Collections.Generic;
using System.Linq;
using LinqSummaryExtensions.Utils;

namespace LinqSummaryExtensions
{
    /// <summary>
    /// MakeSummary extension-methods container.
    /// </summary>
    public static class SummaryExtensions
    {
        /// <summary>
        /// Computes summary object by all computable properties.
        /// </summary>
        /// <param name="source"> sequence of objects to compute </param>
        /// <typeparam name="T"> summary object type </typeparam>
        /// <returns> single summary object </returns>
        /// <exception cref="NotImplementedException"> if called on <see cref="IQueryable{T}"/> </exception>
        public static T MakeSummary<T>(this IEnumerable<T> source)
            where T : class
        {
            if (source is IQueryable<T>)
            {
                throw new NotImplementedException("Only for collections in memory");
            }

            var builder = SummaryObjectBuilderProvider.GetBuilder<T>();

            return (T) builder.Build(source);
        }
    }
}