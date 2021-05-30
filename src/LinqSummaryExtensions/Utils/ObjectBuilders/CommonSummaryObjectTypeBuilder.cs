using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqSummaryExtensions.Utils.ObjectBuilders
{
    /// <summary>
    /// Builder of objects of user-defined types with parameterless ctor.
    /// </summary>
    internal class CommonSummaryObjectTypeBuilder : SummaryObjectBuilder
    {
        /// <inheritdoc />
        public CommonSummaryObjectTypeBuilder(Type targetObjectType) : base(targetObjectType)
        {
        }

        public override object Build<T>(IEnumerable<T> sources) where T : class
        {
            var values = SumProcessor.GetValuesInfo(sources);
            var summaryObject = Activator.CreateInstance<T>();
            
            foreach (var propertyInfo in SumProcessor.Properties)
            {
                var vData = values.SingleOrDefault(v => v.Name == propertyInfo.Name);
                var propertySum = vData is null
                    ? propertyInfo.PropertyType.GetDefaultValue()
                    : vData.Value;
                propertyInfo.SetValue(summaryObject, propertySum);
            }

            return summaryObject;
        }
    }
}