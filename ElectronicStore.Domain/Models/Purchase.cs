namespace ElectronicStore.Domain.Models;

public record Purchase(Guid Id, Guid UserId, DateTime DateTime)
{
    public required List<Product> Products { get; init; }
}
