using UnitConversion.Core.Interfaces;
using UnitConversion.Core.Models;

namespace UnitConversion.Core.Services;

public sealed class ConversionService : IConversionService
{
    private readonly IUnitRepository _unitRepository;
    private readonly IEnumerable<IUnitConverter> _converters;

    public ConversionService(IUnitRepository unitRepository, IEnumerable<IUnitConverter> converters)
    {
        _unitRepository = unitRepository;
        _converters = converters;
    }

    public ConvertResponse Convert(ConvertRequest request)
    {
        var sourceCategory = _unitRepository.FindCategoryByUnit(request.FromUnit);

        if (sourceCategory is null)
        {
            throw new ArgumentException($"Unknown unit: {request.FromUnit}");
        }

        var targetCategory = _unitRepository.FindCategoryByUnit(request.ToUnit);

        if (targetCategory is null)
        {
            throw new ArgumentException($"Unknown unit: {request.ToUnit}");
        }

        if (!sourceCategory.Name.Equals(targetCategory.Name, StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("Units belong to different categories.");
        }

        var converter = _converters.FirstOrDefault(c => c.CanConvert(sourceCategory.Name));

        if (converter is null)
        {
            throw new ArgumentException($"Unsupported category: {sourceCategory.Name}");
        }

        var convertedValue = converter.Convert(request.Value, sourceCategory.Name, request.FromUnit, request.ToUnit);

        return new ConvertResponse
        {
            OriginalValue = request.Value,
            ConvertedValue = convertedValue,
            FromUnit = request.FromUnit,
            ToUnit = request.ToUnit,
            Category = sourceCategory.Name
        };
    }

}