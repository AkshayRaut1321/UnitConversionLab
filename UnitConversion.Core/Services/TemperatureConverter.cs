using UnitConversion.Core.Interfaces;

namespace UnitConversion.Core.Services;

public sealed class TemperatureConverter : IUnitConverter
{
    public bool CanConvert(string category)
    {
        return category.Equals("temperature", StringComparison.OrdinalIgnoreCase);
    }

    public decimal Convert(decimal value, string category, string fromUnit, string toUnit)
    {
        var from = Normalize(fromUnit);

        var to = Normalize(toUnit);

        decimal celsius = from switch
        {
            "celsius" => value,
            "fahrenheit" => (value - 32m) * 5m / 9m,
            "kelvin" => value - 273.15m,
            _ => throw new ArgumentException(
                $"Unsupported unit: {fromUnit}")
        };

        return to switch
        {
            "celsius" => celsius,
            "fahrenheit" => (celsius * 9m / 5m) + 32m,
            "kelvin" => celsius + 273.15m,
            _ => throw new ArgumentException(
                $"Unsupported unit: {toUnit}")
        };
    }

    private static string Normalize(string unit)
    {
        return unit.ToLowerInvariant() switch
        {
            "c" or "°c" => "celsius",
            "f" or "°f" => "fahrenheit",
            "k" => "kelvin",
            _ => unit.ToLowerInvariant()
        };
    }
}