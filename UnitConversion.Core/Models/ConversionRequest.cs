using System.ComponentModel.DataAnnotations;

namespace UnitConversion.Core.Models;

public sealed class ConvertRequest
{
    [Required]
    public decimal Value { get; set; }

    [Required]
    public string FromUnit { get; set; } = string.Empty;

    [Required]
    public string ToUnit { get; set; } = string.Empty;
}