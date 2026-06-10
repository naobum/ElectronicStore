namespace ElectronicStore.Application.Responses;

public record PurchaseResponse
{
    public required long UserId { get; init; }

    public required DateTime DateTime { get; init; }

    public required decimal TotalPrice { get; init; }

    public required IReadOnlyCollection<OrderItemDto> Items { get; init; }
}
