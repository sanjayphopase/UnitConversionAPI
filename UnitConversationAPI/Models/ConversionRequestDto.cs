using System.ComponentModel.DataAnnotations;

namespace UnitConversationAPI.Models
{
    public sealed class ConversionRequestDto
    {
        [Required(ErrorMessage ="Category is required")]
        public string Category { get; init; } = string.Empty;


        [Required(ErrorMessage = "Value is required")]
        public decimal Value { get; init; }

        [Required(ErrorMessage = "FromUnit is required")]
        public string FromUnit { get; init; } = string.Empty;

        [Required(ErrorMessage = "ToUnit is required")]
        public string ToUnit { get; init; } = string.Empty;
    }
}
