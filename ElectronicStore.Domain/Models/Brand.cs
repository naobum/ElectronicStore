namespace ElectronicStore.Domain.Models;

public record Brand(string Name, string? Description)
{
    public long Id { get; private set; }
}