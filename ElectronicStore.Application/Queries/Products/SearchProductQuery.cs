using ElectronicStore.Application.Responses;
using MediatR;

namespace ElectronicStore.Application.Queries.Products;

public record SearchProductsQuery : IRequest<IReadOnlyCollection<ProductResponse>>
{
    public string? NameMatch { get; init; }

    public long? BrandId { get; init; }

    public decimal? MinimalPrice { get; init; }

    public decimal? MaximumPrice { get; init; }

    public double? MinimalRating { get; init; }
}
