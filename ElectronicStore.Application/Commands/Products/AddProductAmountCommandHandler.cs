using ElectronicStore.Domain.Contracts.Repositories;
using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Products;

public class AddProductAmountCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<AddProductAmountCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(AddProductAmountCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetProductById(request.ProductId, cancellationToken);

        if (product == null)
        {
            return Error.NotFound($"Product {request.ProductId} not found");
        }

        product.AddAmount(request.Amount);

        await unitOfWork.SaveChanges();

        return Result.Updated;
    }
}
