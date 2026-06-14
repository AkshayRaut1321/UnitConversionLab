using UnitConversion.Core.Models;

namespace UnitConversion.Core.Interfaces;

public interface IConversionService
{
    ConvertResponse Convert(ConvertRequest request);
}