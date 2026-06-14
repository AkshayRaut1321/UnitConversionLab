namespace UnitConversion.Core.Models;

public sealed class ConvertResponse
{
    public decimal OriginalValue { get; set; }

    public decimal ConvertedValue { get; set; }

    public string FromUnit { get; set; } = string.Empty;

    public string ToUnit { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;
}