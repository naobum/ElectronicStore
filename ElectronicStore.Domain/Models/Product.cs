using ErrorOr;

namespace ElectronicStore.Domain.Models;

public class Product
{
    private Product()
    {
    }
    private Product(long brandId, string name, decimal price, int amount, string? description)
    {
        BrandId = brandId;
        Name = name;
        Price = price;
        Amount = amount;
        Description = description;
        Rating = 0;
    }

    public long Id { get; }
    public long BrandId { get; }
    public Brand? Brand { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public int Amount { get; private set; }
    public string? Description { get; private set; }
    public double Rating { get; private set; }

    public static ErrorOr<Product> Create(long brandId, string name, decimal price, int amount, string? description)
    {
        if (string.IsNullOrEmpty(name))
        {
            return Error.Validation("Name must not be null or empty");
        }
        if (price <= 0)
        {
            return Error.Validation("Price must be greater than 0");
        }
        if (amount <= 0)
        {
            return Error.Validation("Amount must be greater than 0");
        }

        return new Product(brandId, name, price, amount, description);
    }
    public ErrorOr<Updated> UpdateRating(double newRating)
    {
        if (newRating <= 0.0 || newRating > 5.0)
        {
            return Error.Validation("Rating cannot be lower than 0 or greater than 5");
        }

        Rating = newRating;

        return Result.Updated;
    }

    public ErrorOr<Updated> UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0)
        {
            return Error.Validation("Price must be greater than 0");
        }

        Price = newPrice;

        return Result.Updated;
    }

    public void UpdateInfomation(string? name = null, string? description = null)
    {
        Description = string.IsNullOrEmpty(description) ? Description : description;
        Name = string.IsNullOrEmpty(name) ? Name : name;
    }

    public bool MatchFilter(ProductFilter filter)
    {
        if (filter.BrandId is not null && BrandId != filter.BrandId)
        {
            return false;
        }

        if (!string.IsNullOrEmpty(filter.Name) && !Name.Contains(filter.Name, StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        if (filter.MinRating is not null && Rating < filter.MinRating)
        {
            return false;
        }

        if (filter.MinPrice is not null && Price < filter.MinPrice)
        {
            return false;
        }

        if (filter.MaxPrice is not null && Price > filter.MaxPrice)
        {
            return false;
        }

        return true;
    }

    public ErrorOr<Updated> DecreaseAmount(int amount)
    {
        if (amount <= 0)
        {
            return Result.Updated;
        }

        if (amount > Amount)
        {
            return Error.Conflict($"There are not enough product {Id}");
        }

        Amount -= amount;

        return Result.Updated;
    }

    public ErrorOr<Updated> AddAmount(int amount)
    {
        if (amount <= 0)
        {
            return Error.Validation("Amount must be greater than zero.");
        }

        Amount += amount;

        return Result.Updated;
    }
}