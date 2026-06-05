using ElectronicStore.Domain.Contracts.Repositories;
using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Carts;

public class AddCartItemCommandHandler(
    ICartRepository cartRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<AddCartItemCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetCartByUserId(request.UserId, cancellationToken);

        if (cart == null)
        {
            return Error.NotFound($"Not found cart for user {request.UserId}");
        }

        var product = await productRepository.GetProductById(request.ProductId, cancellationToken);

        if (product == null)
        {
            return Error.NotFound($"Not found product {request.ProductId}");
        }

        var result = cart.AddItem(product, request.Quantity, product.Price, product.Amount);

        if (!result.IsError)
        {
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return result;
    }
}
