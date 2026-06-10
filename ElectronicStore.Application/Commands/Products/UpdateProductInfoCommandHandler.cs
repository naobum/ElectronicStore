using ElectronicStore.Domain.Contracts.Repositories;
using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Products;

public class UpdateProductInfoCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateProductInfoCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(UpdateProductInfoCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductById(request.ProductId, cancellationToken);

        if (product == null)
        {
            return Error.NotFound($"Product {request.ProductId} not found");
        }

        if (request.Price != null)
        {
            var result = product.UpdatePrice(request.Price.Value);

            if (result.IsError)
            {
                return result;
            }
        }

        product.UpdateInfomation(request.Name, request.Description);

        await unitOfWork.SaveChanges(cancellationToken);

        return Result.Updated;
    }
}
