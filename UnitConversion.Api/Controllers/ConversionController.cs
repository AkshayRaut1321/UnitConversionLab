using Microsoft.AspNetCore.Mvc;
using UnitConversion.Core.Interfaces;
using UnitConversion.Core.Models;

namespace UnitConversion.Api.Controllers;

[ApiController]
[Route("api/conversions")]
public sealed class ConversionsController : ControllerBase
{
    private readonly IConversionService _conversionService;

    public ConversionsController(
        IConversionService conversionService)
    {
        _conversionService = conversionService;
    }

    [HttpPost]
    public ActionResult<ConvertResponse> Convert([FromBody] ConvertRequest request)
    {
        try
        {
            var result = _conversionService.Convert(request);

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new
            {
                Message = ex.Message
            });
        }
    }
}