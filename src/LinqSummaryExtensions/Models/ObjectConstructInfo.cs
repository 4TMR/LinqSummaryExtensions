using System;
using System.Reflection;

namespace LinqSummaryExtensions.Models
{
    /// <summary>
    /// Meta-info object, that contains data about how to build some object
    /// </summary>
    internal class ObjectConstructInfo
    {
        /// <summary> Object ctor </summary>
        public ConstructorInfo Constructor { get; }
        
        /// <summary> Ctor params </summary>
        public ParameterInfo[] Params { get; }

        /// <summary></summary>
        /// <param name="constructor"> Object ctor </param>
        /// <param name="params"> Ctor params </param>
        /// <exception cref="ArgumentException"> if <paramref name="params"/> is null </exception>
        public ObjectConstructInfo(ConstructorInfo constructor, ParameterInfo[] @params)
        {
            Constructor = constructor;
            Params = @params ?? throw new ArgumentException(nameof(@params));
        }

        /// <summary></summary>
        /// <param name="constructor"> Object ctor </param>
        public ObjectConstructInfo(ConstructorInfo constructor) : this(constructor, constructor.GetParameters())
        {
        }
    }
}