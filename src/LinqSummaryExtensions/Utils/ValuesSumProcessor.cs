using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LinqSummaryExtensions.Models;

namespace LinqSummaryExtensions.Utils
{
    /// <summary>
    /// Grabs data about computable properties and how to sum their values from object type.
    /// </summary>
    internal class ValuesSumProcessor
    {
        /// <summary> repository of prepared sum-methods </summary>
        private readonly Dictionary<PropertyInfo, Func<IEnumerable<object>, object?>> _sumMethodsMap;

        /// <summary> Computable properties </summary>
        public PropertyInfo[] Properties { get; }

        /// <summary></summary>
        /// <param name="targetType"> object type </param>
        public ValuesSumProcessor(Type targetType)
        {
            Properties = SummaryPropertiesExtractor.GetSummaryObjectProperties(targetType);
            _sumMethodsMap = GetComputingSumMethods(Properties);
        }

        /// <summary> Factory method </summary>
        /// <typeparam name="T"> object type </typeparam>
        /// <returns> instance of <see cref="ValuesSumProcessor"/> </returns>
        public static ValuesSumProcessor GetValuesSumProcessor<T>() where T : class => new ValuesSumProcessor(typeof(T));
        
        /// <summary> Executes prapared sum-methods to build values info. </summary>
        /// <param name="source"> sequence of objects to compute </param>
        /// <typeparam name="T"> object type </typeparam>
        /// <returns> array of <see cref="ValueInfo"/> </returns>
        public ValueInfo[] GetValuesInfo<T>(IEnumerable<T> source) where T : class
        {
            return _sumMethodsMap.Select(pair =>
            {
                var computedSumValue = pair.Value.DynamicInvoke(source);
                return new ValueInfo(pair.Key.Name, computedSumValue);
            })
            .ToArray();
        }
        
        /// todo rewrite to expressions, if it is possible
        private static Dictionary<PropertyInfo, Func<IEnumerable<object>, object?>> GetComputingSumMethods(PropertyInfo[] propertyInfos)
        {
            var methodsMap = new Dictionary<PropertyInfo, Func<IEnumerable<object>, object?>>();
            foreach (var propertyInfo in propertyInfos)
            {
                var propertyType = propertyInfo.PropertyType;
                var isGeneric = propertyType.IsGenericType;
                var typeToCheck = isGeneric
                    ? Nullable.GetUnderlyingType(propertyType)
                    : propertyType;
                
                Func<IEnumerable<object>, object?> sumMethod;
                
                switch (typeToCheck)
                {
                    case var _ when typeToCheck == typeof(decimal):
                        sumMethod = !isGeneric
                            ? (Func<IEnumerable<object>, object?>)(enumerable => enumerable.Sum(o => (decimal) propertyInfo.GetValue(o)))
                            : enumerable => enumerable.Sum(o => (decimal?) propertyInfo.GetValue(o));
                        break;
                    case var _ when typeToCheck == typeof(double):
                        sumMethod = !isGeneric
                            ? (Func<IEnumerable<object>, object?>)(enumerable => enumerable.Sum(o => (double) propertyInfo.GetValue(o)))
                            : enumerable => enumerable.Sum(o => (double?) propertyInfo.GetValue(o));
                        break;
                    case var _ when typeToCheck == typeof(float):
                        sumMethod = !isGeneric
                            ? (Func<IEnumerable<object>, object?>)(enumerable => enumerable.Sum(o => (float) propertyInfo.GetValue(o)))
                            : enumerable => enumerable.Sum(o => (float?) propertyInfo.GetValue(o));
                        break;
                    case var _ when typeToCheck == typeof(long):
                        sumMethod = !isGeneric
                            ? (Func<IEnumerable<object>, object?>)(enumerable => enumerable.Sum(o => (long) propertyInfo.GetValue(o)))
                            : enumerable => enumerable.Sum(o => (long?) propertyInfo.GetValue(o));
                        break;
                    case var _ when typeToCheck == typeof(int):
                        sumMethod = !isGeneric
                            ? (Func<IEnumerable<object>, object?>)(enumerable => enumerable.Sum(o => (int) propertyInfo.GetValue(o)))
                            : enumerable => enumerable.Sum(o => (int?) propertyInfo.GetValue(o));
                        break;
                    default:
                        throw new NotImplementedException();
                }
                
                methodsMap.Add(propertyInfo, sumMethod);
            }

            return methodsMap;
        }
    }
}