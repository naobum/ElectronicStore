using ElectronicStore.Application.Responses;
using ElectronicStore.Domain.Contracts.Repositories;
using ElectronicStore.Domain.Models;
using MediatR;

namespace ElectronicStore.Application.Queries.Products;

public class SearchProductQueryHandler(IProductRepository productRepository)
    : IRequestHandler<SearchProductsQuery, IReadOnlyCollection<ProductResponse>>
{
    public async Task<IReadOnlyCollection<ProductResponse>> Handle(SearchProductsQuery request, CancellationToken cancellationToken)
    {
        var filter = new ProductFilter(
            request.NameMatch,
            request.BrandId,
            request.MinimalPrice,
            request.MaximumPrice,
            request.MinimalRating);

        var filteredProducts = await productRepository.GetProductsWithFilter(filter, cancellationToken);

        return filteredProducts
            .Select(product => product.ToResponse())
            .ToArray();
    }
}
