using System;
using System.Collections.Generic;
using System.Linq;
using LinqSummaryExtensions;
using Sample.Models;

namespace Sample
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var dataProvider = new SampleDataProvider();
            var sampleData = dataProvider.GetData(1000);

            var plainSummaryObj = MakeSummaryAsIs(sampleData);
            PrintObject(plainSummaryObj);
            Console.WriteLine();
            
            var anonymousSummaryObj = MakeSummaryAsAnonymous(sampleData);
            PrintObject(anonymousSummaryObj);
            Console.WriteLine();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey(true);
        }

        private static object MakeSummaryAsIs(IEnumerable<SomeDto> sources) => sources.MakeSummary();

        private static object MakeSummaryAsAnonymous(IEnumerable<SomeDto> sources)
        {
            return sources.Select(dto => new
                {
                    dto.Int,
                    dto.Long,
                    dto.Float,
                    dto.Double,
                    dto.Decimal,
                    dto.Date,
                    dto.Type,
                    dto.SomeFlag,
                    dto.Title,
                    dto.NullableInt,
                    dto.NullableLong,
                    dto.NullableFloat,
                    dto.NullableDouble,
                    dto.NullableDecimal,
                    dto.NullableDate,
                    dto.NullableType,
                    dto.NullableSomeFlag,
                    dto.NullableTitle
                })
                .MakeSummary();
        }

        private static void PrintObject(object obj)
        {
            var type = obj.GetType();
            Console.WriteLine($"{type.Name} data:");
            foreach (var property in type.GetProperties())
            {
                Console.WriteLine($"\t{property.Name}: {property.GetValue(obj)}");
            }
        }
        
    }
}