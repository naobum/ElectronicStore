using ElectronicStore.Domain.Models;

namespace ElectronicStore.Application.Responses;

public static class Mappings
{
    public static UserResponse ToResponse(this User user)
    {
        return new UserResponse(user.Id, user.Name, user.Balance, user.Cart?.ToDto() ?? new CartDto
        {
            Items = Array.Empty<OrderItemDto>()
        });
    }

    public static CartDto ToDto(this Cart? cart)
    {
        return new CartDto
        {
            Items = cart?.GetItems()
                .Select(orderItem => orderItem.ToDto())
                .ToArray()
                ?? Array.Empty<OrderItemDto>()
        };
    }

    public static OrderItemDto ToDto(this OrderItem orderItem)
    {
        return new OrderItemDto
        {
            Amount = orderItem.Amount,
            ProductId = orderItem.Product?.Id ?? 0,
            ProductName = orderItem.Product?.Name ?? "Unknown product",
            TotalPrice = orderItem.Price,
            UnitPrice = orderItem.UnitPrice
        };
    }

    public static PurchaseResponse ToResponse(this Purchase purchase)
    {
        return new PurchaseResponse
        {
            UserId = purchase.User?.Id ?? 0,
            DateTime = purchase.DateTime,
            TotalPrice = purchase.TotalPrice,
            Items = (purchase.Items ?? Array.Empty<OrderItem>())
                .Select(item => item.ToDto())
                .ToArray()
        };
    }

    public static BrandResponse ToResponse(this Brand brand)
    {
        return new BrandResponse
        {
            Id = brand.Id,
            Name = brand.Name,
            Description = brand.Description
        };
    }

    public static ProductResponse ToResponse(this Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            BrandId = product.BrandId,
            Name = product.Name,
            Amount = product.Amount,
            Price = product.Price,
            Rating = product.Rating,
            Description = product.Description
        };
    }
}
