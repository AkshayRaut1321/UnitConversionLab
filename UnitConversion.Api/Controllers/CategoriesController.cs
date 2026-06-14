using Microsoft.AspNetCore.Mvc;
using UnitConversion.Core.Interfaces;

namespace UnitConversion.Api.Controllers;

[ApiController]
[Route("api/categories")]
public sealed class CategoriesController : ControllerBase
{
    private readonly IUnitRepository _unitRepository;

    public CategoriesController(
        IUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<string>> GetCategories()
    {
        return Ok(_unitRepository.GetCategories());
    }

    [HttpGet("{category}/units")]
    public ActionResult<IEnumerable<string>> GetUnits(
        string category)
    {
        var units =
            _unitRepository.GetUnits(category);

        if (!units.Any())
        {
            return NotFound();
        }

        return Ok(units);
    }
}