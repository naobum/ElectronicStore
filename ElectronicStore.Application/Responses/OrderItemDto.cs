namespace ElectronicStore.Application.Responses;

public record OrderItemDto
{
    public required long ProductId { get; init; }
    public required string ProductName { get; init; }
    public required int Amount { get; init; }
    public required decimal TotalPrice { get; init; }
    public required decimal UnitPrice { get; init; }
}
