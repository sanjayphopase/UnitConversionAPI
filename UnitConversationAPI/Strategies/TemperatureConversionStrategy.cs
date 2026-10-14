using UnitConversationAPI.Interfaces;
using UnitConversationAPI.Models;

namespace UnitConversationAPI.Strategies
{
    public class TemperatureConversionStrategy:IConversionStrategy
    {
        public string Category => "Temperature";

        private readonly Dictionary<string, Func<decimal, decimal>> _toKelvin = new()
        {
            ["celsius"] = value => value + 273.15m,
            ["fahrenheit"] = value => (value - 32m) * 5m / 9m + 273.15m,
            ["kelvin"] = value => value
        };

        private readonly Dictionary<string, Func<decimal, decimal>> _fromKelvin = new()
        {
            ["celsius"] = value => value - 273.15m,
            ["fahrenheit"] = value => (value - 273.15m) * 9m / 5m + 32m,
            ["kelvin"] = value => value
        };

        public ConversionResponseDto Convert(ConversionRequestDto requestDto)
        {
            if (!_toKelvin.TryGetValue(requestDto.FromUnit.ToLowerInvariant(), out var toKelvin))
                throw new ArgumentException("Unsupported source temperature unit.");

            if (!_fromKelvin.TryGetValue(requestDto.ToUnit.ToLowerInvariant(), out var fromKelvin))
                throw new ArgumentException("Unsupported target temperature unit.");

            decimal kelvin = toKelvin(requestDto.Value);

            if (kelvin < 0)
                throw new ArgumentException("Temperature cannot be below absolute zero.");

            decimal result = fromKelvin(kelvin);

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
