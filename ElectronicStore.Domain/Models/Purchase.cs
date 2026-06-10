namespace ElectronicStore.Domain.Models;

public class Purchase
{
    public long Id { get; init; }

    public required User? User { get; init; }

    public required DateTime DateTime { get; init; }

    public required IReadOnlyCollection<OrderItem> Items { get; init; }

    public required decimal TotalPrice { get; init; }
}
