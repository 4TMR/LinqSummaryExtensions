using System;
using System.Collections.Generic;
using Bogus;
using Bogus.Extensions;
using Sample.Models;

namespace Sample
{
    public class SampleDataProvider
    {
        private readonly Faker<SomeDto> _generator;

        public SampleDataProvider()
        {
            _generator = new Faker<SomeDto>()
                .StrictMode(true)
                .RuleFor(dto => dto.Int, faker => faker.Random.Int(1, 100))
                .RuleFor(dto => dto.Long, faker => faker.Random.Long(1, 100))
                .RuleFor(dto => dto.Float, faker => faker.Random.Float())
                .RuleFor(dto => dto.Double, faker => faker.Random.Double())
                .RuleFor(dto => dto.Decimal, faker => faker.Finance.Amount())
                .RuleFor(dto => dto.Date, faker => faker.Date.Between(DateTime.UtcNow.AddYears(-1), DateTime.UtcNow))
                .RuleFor(dto => dto.Title, faker => faker.Commerce.Product())
                .RuleFor(dto => dto.Type, faker => faker.PickRandom<SomeType>())
                .RuleFor(dto => dto.SomeFlag, faker => faker.Random.Bool())
                .RuleFor(dto => dto.NullableInt, faker => faker.Random.Int(1, 100).OrNull(faker))
                .RuleFor(dto => dto.NullableLong, faker => faker.Random.Long(1, 100).OrNull(faker))
                .RuleFor(dto => dto.NullableFloat, faker => faker.Random.Float().OrNull(faker))
                .RuleFor(dto => dto.NullableDouble, faker => faker.Random.Double().OrNull(faker))
                .RuleFor(dto => dto.NullableDecimal, faker => faker.Finance.Amount().OrNull(faker))
                .RuleFor(dto => dto.NullableDate,
                    faker => faker.Date.Between(DateTime.UtcNow.AddYears(-1), DateTime.UtcNow).OrNull(faker))
                .RuleFor(dto => dto.NullableTitle, faker => faker.Commerce.Product())
                .RuleFor(dto => dto.NullableType, faker => faker.PickRandom<SomeType>().OrNull(faker))
                .RuleFor(dto => dto.NullableSomeFlag, faker => faker.Random.Bool().OrNull(faker));
        }

        public List<SomeDto> GetData(int count) => _generator.Generate(count);
    }
}