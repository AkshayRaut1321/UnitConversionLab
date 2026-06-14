using UnitConversion.Core.Interfaces;

namespace UnitConversion.Core.Services;

public sealed class LinearUnitConverter : IUnitConverter
{
    private readonly IUnitRepository _unitRepository;

    public LinearUnitConverter(
        IUnitRepository unitRepository)
    {
        _unitRepository = unitRepository;
    }

    public bool CanConvert(string category)
    {
        return !category.Equals("temperature", StringComparison.OrdinalIgnoreCase);
    }

    public decimal Convert(decimal value, string category, string fromUnit, string toUnit)
    {
        var from = _unitRepository.GetUnit(category, fromUnit);

        var to = _unitRepository.GetUnit(category, toUnit);

        if (from?.Factor is null)
        {
            throw new ArgumentException($"Unknown unit: {fromUnit}");
        }

        if (to?.Factor is null)
        {
            throw new ArgumentException($"Unknown unit: {toUnit}");
        }

        var baseValue = value * from.Factor.Value;

        return baseValue / to.Factor.Value;
    }
}