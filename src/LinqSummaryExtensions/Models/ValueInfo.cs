namespace LinqSummaryExtensions.Models
{
    /// <summary>
    /// Meta-info object about value, that should used as constructor parameter
    /// </summary>
    internal class ValueInfo
    {
        /// <summary> Parameter name </summary>
        public string Name { get; }
        /// <summary> Computed value </summary>
        public object? Value { get; }

        /// <summary></summary>
        /// <param name="name"> Parameter name </param>
        /// <param name="value"> Computed value </param>
        public ValueInfo(string name, object? value)
        {
            Name = name;
            Value = value;
        }
    }
}