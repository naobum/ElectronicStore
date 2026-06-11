using System.ComponentModel.DataAnnotations;

namespace ElectronicStore.Api.Requests.Products;

public record UpdateProductInfoRequest
{
    [Required]
    public long ProductId { get; init; }

    public decimal? Price { get; init; }

    public long? BrandId { get; init; }

    public int? Amount { get; init; }

    public string? Name { get; init; }

    public string? Description { get; init; }

    [Range(0, 5)]
    public double? Rating { get; init; }
}
