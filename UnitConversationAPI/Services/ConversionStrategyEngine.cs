using UnitConversationAPI.Interfaces;
using UnitConversationAPI.Models;

namespace UnitConversationAPI.Services
{
    public class ConversionStrategyEngine : IConversionStrategyEngine
    {

        private readonly Dictionary<string, IConversionStrategy> _strategies;

        public ConversionStrategyEngine(
            IEnumerable<IConversionStrategy> strategies)
        {
            _strategies = strategies.ToDictionary(
                strategy => strategy.Category,
                StringComparer.OrdinalIgnoreCase);
        }
        public IConversionStrategy GetStrategy(string category)
        {
            if (_strategies.TryGetValue(category, out var strategy))
            {
                return strategy;
            }

            throw new NotSupportedException(
                $"Conversion category '{category}' is not supported.");
        }
    }
}
