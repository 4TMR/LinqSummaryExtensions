using System;
using System.Collections.Generic;
using LinqSummaryExtensions.Models;

namespace LinqSummaryExtensions.Utils
{
    /// <summary>
    /// The object builder.
    /// </summary>
    internal abstract class SummaryObjectBuilder
    {
        /// <summary> Bulding object type </summary>
        protected Type TargetObjectType { get; }
        
        /// <summary>
        /// <see cref="TargetObjectType"/> building data
        /// </summary>
        protected ObjectConstructInfo ConstructInfo { get; }
        
        /// <summary>
        /// Prepared instanfe of <see cref="ValuesSumProcessor"/>
        /// </summary>
        protected ValuesSumProcessor SumProcessor { get; }
        
        /// <summary></summary>
        /// <param name="targetObjectType"> object type to build </param>
        /// <exception cref="ArgumentNullException"> if <paramref name="targetObjectType"/> is null </exception>
        protected SummaryObjectBuilder(Type targetObjectType)
        {
            TargetObjectType = targetObjectType ?? throw new ArgumentNullException(nameof(targetObjectType));
            ConstructInfo = ObjectConstructInfoProvider.GetInfo(TargetObjectType);
            SumProcessor = new ValuesSumProcessor(TargetObjectType);
        }
        
        /// <summary> Build summary object. </summary>
        /// <param name="sources"> sequence of objects to build summary object </param>
        /// <typeparam name="T"> source objects type </typeparam>
        /// <returns> filled summary object </returns>
        public abstract object Build<T>(IEnumerable<T> sources) where T : class;
    }
}