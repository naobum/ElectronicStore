namespace ElectronicStore.Domain.Models;


public record Product(Guid Id, string Name, int BrandId, float Rating, string? Description)
{
    public Brand? Brand { get; init; }

    public List<Review> Reviews { get; init; } = [];
}
