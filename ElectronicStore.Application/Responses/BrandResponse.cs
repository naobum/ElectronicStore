namespace ElectronicStore.Application.Responses;

public record BrandResponse
{
    public required long Id { get; init; }
    public required string Name { get; init; }

    public required string? Description { get; init; }
}
