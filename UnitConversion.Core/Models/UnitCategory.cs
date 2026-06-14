namespace UnitConversion.Core.Models;

public sealed class CategoryDefinition
{
    public string Name { get; set; } = string.Empty;

    public string? BaseUnit { get; set; }

    public List<UnitDefinition> Units { get; set; } = [];
}