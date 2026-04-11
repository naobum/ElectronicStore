namespace ElectronicStore.Domain.Models;

public record Review(Guid Id, int UserId, int ProductId, int Rate, string? Comment)
{
    public User? User { get; init; }

    public Product? Product { get; init; }

}