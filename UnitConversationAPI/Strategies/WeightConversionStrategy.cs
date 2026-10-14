using UnitConversationAPI.Interfaces;
using UnitConversationAPI.Models;

namespace UnitConversationAPI.Strategies
{
    public class WeightConversionStrategy : IConversionStrategy
    {
        public string Category => "Weight";

        private readonly Dictionary<string, decimal> _toKilogram = new()
        {
            ["kilogram"] = 1m,
            ["gram"] = 0.001m,
            ["pound"] = 0.45359237m,
            ["ounce"] = 0.028349523125m
        };

        private readonly Dictionary<string, decimal> _fromKilogram = new()
        {
            ["kilogram"] = 1m,
            ["gram"] = 1000m,
            ["pound"] = 1m / 0.45359237m,
            ["ounce"] = 1m / 0.028349523125m
        };

        public ConversionResponseDto Convert(ConversionRequestDto requestDto)
        {
            if (requestDto.Value < 0)
                throw new ArgumentException("Weight cannot be negative.");

            if (!_toKilogram.TryGetValue(requestDto.FromUnit.ToLowerInvariant(), out var toKilogram))
                throw new ArgumentException("Unsupported source weight unit.");

            if (!_fromKilogram.TryGetValue(requestDto.ToUnit.ToLowerInvariant(), out var fromKilogram))
                throw new ArgumentException("Unsupported target weight unit.");

            decimal result = requestDto.Value * toKilogram * fromKilogram;

            return new ConversionResponseDto
            {
                Category = Category,
                FromUnit = requestDto.FromUnit,
                ToUnit = requestDto.ToUnit,
                InputValue = requestDto.Value,
                Result = result
            };
        }
    }
}
