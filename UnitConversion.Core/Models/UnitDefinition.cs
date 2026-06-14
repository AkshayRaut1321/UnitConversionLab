namespace UnitConversion.Core.Models;

public sealed class UnitDefinition
{
    public string Name { get; set; } = string.Empty;

    public decimal? Factor { get; set; }
    
    public List<string> Aliases { get; set; } = [];
}