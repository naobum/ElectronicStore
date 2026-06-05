using ElectronicStore.Application.Responses;
using ElectronicStore.Domain.Models;

namespace ElectronicStore.Application.Queries.Users;

public static class Mappings
{
    public static UserResponse ToResponse(this User user)
    {
        return new UserResponse(user.Id, user.Name, user.Balance, user.Cart.ToDto());
    }

    private static CartDto ToDto(this Cart cart)
    {
        return new CartDto
        {
            Items = cart.GetItems()
            .Select(orderItem => orderItem.ToDto())
            .ToArray()
        };
    }

    private static OrderItemDto ToDto(this OrderItem orderItem)
    {
        return new OrderItemDto
        {
            Amount = orderItem.Amount,
            ProductId = orderItem.Product.Id,
            ProductName = orderItem.Product.Name,
            TotalPrice = orderItem.Price,
            UnitPrice = orderItem.UnitPrice
        };
    }
}
