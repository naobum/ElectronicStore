namespace ElectronicStore.Domain.Models;

public record Cart(Guid Id, Guid UserId)
{
    public User? User { get; set; }
    public List<Product> Products { get; set; } = [];
}
