using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UnitConversationAPI.Interfaces;
using UnitConversationAPI.Models;

namespace UnitConversationAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConversionController : ControllerBase
    {
        private readonly IConversionService _conversionService;
        public ConversionController(IConversionService conversionService)
        {
            _conversionService = conversionService;
        }

        [HttpPost]
        public async Task<ActionResult<ConversionResponseDto>> Convert(
        [FromBody] ConversionRequestDto request)
        {
            var response = await _conversionService.Convert(request);

            return Ok(response);
        }
    }
}
