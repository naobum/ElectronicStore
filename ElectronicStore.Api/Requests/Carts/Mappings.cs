using ElectronicStore.Application.Commands.Carts;

namespace ElectronicStore.Api.Requests.Carts;

public static class Mappings
{
    public static AddCartItemCommand ToCommand(this AddCartItemRequest request, long userId)
    {
        return new AddCartItemCommand
        {
            ProductId = request.ProductId,
            Quantity = request.Amount,
            UserId = userId
        };
    }

    public static RemoveCartItemCommand ToCommand(this RemoveCartItemRequest request, long userId)
    {
        return new RemoveCartItemCommand
        {
            ProductId = request.ProductId,
            UsertId = userId
        };
    }
}
