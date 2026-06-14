using UnitConversion.Core.Models;

namespace UnitConversion.Core.Interfaces;

public interface IUnitRepository
{
    IReadOnlyCollection<string> GetCategories();

    IReadOnlyCollection<string> GetUnits(string category);

    CategoryDefinition? FindCategoryByUnit(string unit);

    UnitDefinition? GetUnit(string category, string unitName);
}