using System.ComponentModel.DataAnnotations;

namespace ElectronicStore.Api.Requests.Brands;

public record CreateBrandRequest
{
    [Required]
    public string Name { get; init; } = default!;

    [MaxLength(500)]
    public string? Description { get; init; }
}
