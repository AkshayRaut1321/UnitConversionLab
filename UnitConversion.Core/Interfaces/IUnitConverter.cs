namespace UnitConversion.Core.Interfaces;

public interface IUnitConverter
{
    bool CanConvert(string category);

    decimal Convert(decimal value, string category, string fromUnit, string toUnit);
}