namespace ElectronicStore.Domain.Models;

public record ProductFilter(string? Name, long? BrandId, decimal? MinPrice, decimal? MaxPrice, double? MinRating);
