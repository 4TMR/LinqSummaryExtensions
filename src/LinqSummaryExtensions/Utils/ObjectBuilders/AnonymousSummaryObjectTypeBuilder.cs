using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LinqSummaryExtensions.Models;

namespace LinqSummaryExtensions.Utils.ObjectBuilders
{
    /// <summary>
    /// Builder of objects of anonymous type.
    /// </summary>
    internal class AnonymousSummaryObjectTypeBuilder : SummaryObjectBuilder
    {
        /// <inheritdoc />
        public AnonymousSummaryObjectTypeBuilder(Type targetObjectType) : base(targetObjectType)
        {
        }

        public override object Build<T>(IEnumerable<T> sources) where T : class
        {
            var values = SumProcessor.GetValuesInfo(sources);
            var preparedCtorParameters = PrepareCtorParameters(ConstructInfo.Params, values);
            return Activator.CreateInstance(TargetObjectType, preparedCtorParameters);
        }
        
        /// <summary> Preparing arguments to pass into ctor. </summary>
        /// <param name="parameters"> defined constructor parameters </param>
        /// <param name="values"> values for parameters </param>
        /// <returns> ready arguments set </returns>
        private static object?[] PrepareCtorParameters(ParameterInfo[] parameters, ValueInfo[] values)
        {
            return parameters.OrderBy(ctorParam => ctorParam.Position)
                .Select(ctorParam =>
                {
                    var vData = values.SingleOrDefault(vd =>
                        string.Equals(vd.Name, ctorParam.Name, StringComparison.CurrentCultureIgnoreCase));
                    return vData is null
                        ? ctorParam.ParameterType.GetDefaultValue()
                        : vData.Value;
                })
                .ToArray();
        }
    }
}