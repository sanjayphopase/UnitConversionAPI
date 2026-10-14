using UnitConversationAPI.Models;

namespace UnitConversationAPI.Interfaces
{
    public interface IConversionStrategy
    {
        string Category { get; }

        ConversionResponseDto Convert(ConversionRequestDto requestDto);
    }
}
