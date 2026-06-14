using Moq;
using UnitConversion.Core.Interfaces;
using UnitConversion.Core.Models;
using UnitConversion.Core.Services;

namespace UnitConversion.Tests.Services;

[TestFixture]
public class ConversionServiceTests
{
    private Mock<IUnitRepository> _repository = null!;
    private Mock<IUnitConverter> _converter = null!;
    private ConversionService _service = null!;

    [SetUp]
    public void Setup()
    {
        _repository = new Mock<IUnitRepository>();

        _converter = new Mock<IUnitConverter>();

        _service = new ConversionService(
            _repository.Object,
            [_converter.Object]);
    }

    [Test]
    public void Convert_ShouldThrow_WhenSourceUnitIsUnknown()
    {
        var request = new ConvertRequest
        {
            Value = 10,
            FromUnit = "abc",
            ToUnit = "meter"
        };

        _repository.Setup(x => x.FindCategoryByUnit("abc"))
                .Returns((CategoryDefinition?)null);

        var ex = Assert.Throws<ArgumentException>(() => _service.Convert(request));

        Assert.That(ex!.Message, Is.EqualTo("Unknown unit: abc"));
    }

    [Test]
    public void Convert_ShouldThrow_WhenTargetUnitIsUnknown()
    {
        var request = new ConvertRequest
        {
            Value = 10,
            FromUnit = "meter",
            ToUnit = "xyz"
        };

        _repository.Setup(x => x.FindCategoryByUnit("meter"))
                .Returns(new CategoryDefinition { Name = "length" });

        _repository.Setup(x => x.FindCategoryByUnit("xyz"))
                .Returns((CategoryDefinition?)null);

        var ex = Assert.Throws<ArgumentException>(() => _service.Convert(request));

        Assert.That(ex!.Message, Is.EqualTo("Unknown unit: xyz"));
    }

    [Test]
    public void Convert_ShouldThrow_WhenUnitsBelongToDifferentCategories()
    {
        var request = new ConvertRequest
        {
            Value = 10,
            FromUnit = "meter",
            ToUnit = "celsius"
        };

        _repository.Setup(x => x.FindCategoryByUnit("meter"))
                .Returns(new CategoryDefinition { Name = "length" });

        _repository.Setup(x => x.FindCategoryByUnit("celsius"))
                .Returns(new CategoryDefinition { Name = "temperature" });

        var ex = Assert.Throws<ArgumentException>(() => _service.Convert(request));

        Assert.That(ex!.Message,
            Is.EqualTo("Units belong to different categories."));
    }

    [Test]
    public void Convert_ShouldThrow_WhenNoConverterSupportsCategory()
    {
        var request = new ConvertRequest
        {
            Value = 10,
            FromUnit = "meter",
            ToUnit = "km"
        };

        var category = new CategoryDefinition
        {
            Name = "length"
        };

        _repository.Setup(x => x.FindCategoryByUnit(It.IsAny<string>()))
                .Returns(category);

        _converter.Setup(x => x.CanConvert("length"))
                .Returns(false);

        var ex = Assert.Throws<ArgumentException>(() => _service.Convert(request));

        Assert.That(ex!.Message,
            Is.EqualTo("Unsupported category: length"));
    }

    [Test]
    public void Convert_ShouldReturnConvertedValue()
    {
        var request = new ConvertRequest
        {
            Value = 1,
            FromUnit = "km",
            ToUnit = "meter"
        };

        var category = new CategoryDefinition
        {
            Name = "length"
        };

        _repository.Setup(x => x.FindCategoryByUnit(It.IsAny<string>()))
                .Returns(category);

        _converter.Setup(x => x.CanConvert("length"))
                .Returns(true);

        _converter.Setup(x => x.Convert(1, "length", "km", "meter"))
                .Returns(1000);

        var result = _service.Convert(request);

        Assert.That(result.ConvertedValue, Is.EqualTo(1000));
        Assert.That(result.Category, Is.EqualTo("length"));
    }
}