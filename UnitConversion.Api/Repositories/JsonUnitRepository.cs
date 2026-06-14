using System.Text.Json;
using UnitConversion.Core.Interfaces;
using UnitConversion.Core.Models;

namespace UnitConversion.Api.Repositories;

public sealed class JsonUnitRepository : IUnitRepository
{
    private readonly UnitConfiguration _configuration;

    public JsonUnitRepository(IWebHostEnvironment environment)
    {
        var filePath = Path.Combine(environment.ContentRootPath, "Config", "units.json");

        var json = File.ReadAllText(filePath);
        
        _configuration = JsonSerializer.Deserialize<UnitConfiguration>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })
            ?? throw new InvalidOperationException("Unable to load unit configuration.");
    }

    public IReadOnlyCollection<string> GetCategories()
    {
        return _configuration.Categories.Select(x => x.Name).ToList();
    }

    public IReadOnlyCollection<string> GetUnits(string category)
    {
        var categoryDefinition = _configuration.Categories
            .FirstOrDefault(x =>
                x.Name.Equals(category,
                    StringComparison.OrdinalIgnoreCase));

        if (categoryDefinition is null)
        {
            return [];
        }

        return categoryDefinition.Units
            .Select(x => x.Name)
            .ToList();
    }

    public CategoryDefinition? FindCategoryByUnit(string unit)
    {
        return _configuration.Categories
            .FirstOrDefault(category =>
                category.Units.Any(u => MatchesUnit(u, unit)));
    }

    public UnitDefinition? GetUnit(string category, string unitName)
    {
        return _configuration.Categories
            .FirstOrDefault(c => c.Name.Equals(category, StringComparison.OrdinalIgnoreCase))
            ?.Units.FirstOrDefault(u => MatchesUnit(u, unitName));
    }

    private bool MatchesUnit(UnitDefinition unit, string searchValue)
    {
        return unit.Name.Equals(searchValue, StringComparison.OrdinalIgnoreCase)
            || unit.Aliases.Any(alias => alias.Equals(searchValue, StringComparison.OrdinalIgnoreCase));
    }
}