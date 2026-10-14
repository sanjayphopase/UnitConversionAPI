using UnitConversationAPI.Interfaces;
using UnitConversationAPI.Models;

namespace UnitConversationAPI.Strategies
{
    public class LengthConversionStrategy : IConversionStrategy
    {
        public string Category => "Length";

        private readonly Dictionary<string, decimal> _toMeter = new()
        {
            ["meter"] = 1m,
            ["kilometer"] = 1000m,
            ["centimeter"] = 0.01m,
            ["mile"] = 1609.344m
        };

        private readonly Dictionary<string, decimal> _fromMeter = new()
        {
            ["meter"] = 1m,
            ["kilometer"] = 0.001m,
            ["centimeter"] = 100m,
            ["mile"] = 1m / 1609.344m
        };

        public ConversionResponseDto Convert(ConversionRequestDto requestDto)
        {
            if (!_toMeter.TryGetValue(requestDto.FromUnit.ToLowerInvariant(), out var toMeter))
                throw new ArgumentException("Unsupported source length unit.");

            if (!_fromMeter.TryGetValue(requestDto.ToUnit.ToLowerInvariant(), out var fromMeter))
                throw new ArgumentException("Unsupported target length unit.");

            decimal result = Math.Round(requestDto.Value * toMeter * fromMeter,2,MidpointRounding.AwayFromZero);

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
