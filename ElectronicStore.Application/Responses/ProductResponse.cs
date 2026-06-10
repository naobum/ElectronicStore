namespace ElectronicStore.Application.Responses;

public record ProductResponse
{
    public required long Id { get; init; }

    public required long BrandId { get; init; }

    public required string Name { get; init; }

    public required decimal Price { get; init; }

    public required int Amount { get; init; }

    public required double Rating { get; init; }

    public required string? Description { get; init; }
}
