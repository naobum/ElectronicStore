using ElectronicStore.Domain.Contracts.Repositories;
using ElectronicStore.Domain.Models;
using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Carts;

public class BuyCommandHandler(
    IUserRepository userRepository,
    IPurchaseRepository purchaseRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<BuyCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(BuyCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserById(request.UserId, cancellationToken);

        if (user == null)
        {
            return Error.NotFound($"User {request.UserId} not found");
        }

        if (user.Cart == null || user.Cart.GetItems().Count == 0)
        {
            return Error.Conflict($"User {request.UserId} does not have a cart or cart is empty");
        }

        var orderItems = user.Cart.GetItems();

        var totalPrice = orderItems.Sum(item => item.Price);

        var decreasingProductAmountResults = orderItems.Select(item => item.Product.DecreaseAmount(item.Amount));

        if (decreasingProductAmountResults.Any(result => result.IsError))
        {
            return decreasingProductAmountResults.First(result => result.IsError).FirstError;
        }

        var purchase = new Purchase
        {
            DateTime = DateTime.UtcNow,
            Items = orderItems.ToArray(),
            TotalPrice = totalPrice,
            User = user
        };

        await purchaseRepository.Create(purchase);

        user.Cart.Clear();

        await unitOfWork.SaveChanges(cancellationToken);

        return Result.Success;
    }
}
