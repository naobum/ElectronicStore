using System.ComponentModel.DataAnnotations;

namespace ElectronicStore.Api.Requests.Products;

public record CreateProductRequest
{
    [Required]
    public long BrandId { get; init; }

    [Required]
    public string Name { get; init; } = default!;

    [Required]
    public int Amount { get; init; }

    [Required]
    public decimal Price { get; init; }

    [Range(0, 5)]
    public double? Rating { get; init; }

    public string? Description { get; init; }
}
