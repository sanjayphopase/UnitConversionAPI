using System.ComponentModel.DataAnnotations;

namespace UnitConversationAPI.Models
{
    public class ConversionResponseDto
    {
        public string Category { get; set; } = string.Empty;

        public decimal InputValue { get; set; }

        public string FromUnit { get; set; } = string.Empty;

        public decimal Result { get; set; }

        public string ToUnit { get; set; } = string.Empty;
    }
}
