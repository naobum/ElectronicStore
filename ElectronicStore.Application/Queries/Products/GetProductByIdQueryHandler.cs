using ElectronicStore.Application.Responses;
using ElectronicStore.Domain.Contracts.Repositories;
using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Queries.Products;

public class GetProductByIdQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductByIdQuery, ErrorOr<ProductResponse>>
{
    public async Task<ErrorOr<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductById(request.Id, cancellationToken);

        if (product == null)
        {
            return Error.NotFound($"Product {request.Id} not found");
        }

        return product.ToResponse();
    }
}
