using UnitConversationAPI.Interfaces;
using UnitConversationAPI.Models;

namespace UnitConversationAPI.Services
{
    public class ConversionService : IConversionService
    {
        private readonly IConversionStrategyEngine _strategyEngine;

        public ConversionService(IConversionStrategyEngine strategyEngine)
        {
            _strategyEngine = strategyEngine;
        }
        public async Task<ConversionResponseDto> Convert(ConversionRequestDto requestDto)
        {
            var strategy = _strategyEngine.GetStrategy(requestDto.Category);

            return strategy.Convert(requestDto);
        }
    }
}
