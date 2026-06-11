using ElectronicStore.Domain.Contracts.Repositories;
using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Carts;

public class AddCartItemCommandHandler(
    ICartRepository cartRepository,
    IProductRepository productRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<AddCartItemCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetCartByUserId(request.UserId, cancellationToken);

        if (cart == null)
        {
            var user = await userRepository.GetUserById(request.UserId, cancellationToken);

            if (user == null)
            {
                return Error.NotFound($"Not found user {request.UserId}");
            }

            cart = new Domain.Models.Cart();
            var cartCreationResult = user.SetCart(cart);

            if (cartCreationResult.IsError)
            {
                return cartCreationResult.FirstError;
            }

            await cartRepository.CreateCart(cart, cancellationToken);
        }

        var product = await productRepository.GetProductById(request.ProductId, cancellationToken);

        if (product == null)
        {
            return Error.NotFound($"Not found product {request.ProductId}");
        }

        var result = cart.AddItem(product, request.Quantity, product.Price, product.Amount);

        if (!result.IsError)
        {
            await unitOfWork.SaveChanges(cancellationToken);
        }

        return result;
    }
}
