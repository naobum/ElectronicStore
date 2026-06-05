namespace ElectronicStore.Application.Responses;

public record CartDto
{
    public required IReadOnlyCollection<OrderItemDto> Items { get; init; }
}
