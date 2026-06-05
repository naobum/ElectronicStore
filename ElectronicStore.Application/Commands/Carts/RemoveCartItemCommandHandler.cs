using ElectronicStore.Domain.Contracts.Repositories;
using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Carts;

public class RemoveCartItemCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<RemoveCartItemCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(RemoveCartItemCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserById(request.UsertId, cancellationToken);

        if (user == null)
        {
            return Error.NotFound($"User {request.UsertId} not found");
        }

        var cart = user.Cart;

        if (cart == null)
        {
            return Error.Failure($"User {request.UsertId} does not have a cart");
        }

        var cartItems = cart.GetItems();
        var cartItem = cartItems.FirstOrDefault(item => item.Product?.Id == request.ProductId);

        if (cartItem == null)
        {
            return Error.NotFound($"Product {request.ProductId} not found in cart");
        }

        cart.RemoveItem(request.ProductId);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Updated;
    }
}
