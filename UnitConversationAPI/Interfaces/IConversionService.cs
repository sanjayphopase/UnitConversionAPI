using UnitConversationAPI.Models;

namespace UnitConversationAPI.Interfaces
{
    public interface IConversionService
    {
        Task<ConversionResponseDto> Convert(ConversionRequestDto requestDto);
    }
}
