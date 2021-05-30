using System;

namespace Sample.Models
{
    public class SomeDto
    {
        public int Int { get; set; }
        
        public long Long { get; set; }
        
        public decimal Decimal { get; set; }
        
        public double Double { get; set; }
        
        public float Float { get; set; }
        
        public bool SomeFlag { get; set; }
        
        public DateTime Date { get; set; }
        
        public SomeType Type { get; set; }
        
        public string Title { get; set; } = string.Empty;
        
        public int? NullableInt { get; set; }
        
        public long? NullableLong { get; set; }
        
        public decimal? NullableDecimal { get; set; }
        
        public double? NullableDouble { get; set; }
        
        public float? NullableFloat { get; set; }
        
        public bool? NullableSomeFlag { get; set; }
        
        public DateTime? NullableDate { get; set; }
        
        public SomeType? NullableType { get; set; }
        
        public string? NullableTitle { get; set; }
    }
}