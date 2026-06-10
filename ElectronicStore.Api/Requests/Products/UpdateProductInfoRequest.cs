using System.ComponentModel.DataAnnotations;

namespace ElectronicStore.Api.Requests.Products;

public record UpdateProductInfoRequest
{
    [Required]
    public long ProductId { get; init; }

    public decimal? Price { get; init; }

    public string? Name { get; init; }

    public string? Description { get; init; }
}
