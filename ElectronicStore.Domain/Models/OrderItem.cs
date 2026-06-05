using ErrorOr;

namespace ElectronicStore.Domain.Models;

public class OrderItem
{
    public long Id { get; private set; }
    public int Amount { get; private set; }
    public decimal Price { get; private set; }
    public decimal UnitPrice { get; private set; }

    // навигационные свойства
    public Product Product { get; private set; }
    public Cart? Cart { get; private set; }
    public Purchase? Purchase { get; private set; }

    public OrderItem(Product product, int amount, decimal unitPrice)
    {
        Product = product;
        Amount = amount;
        UnitPrice = unitPrice;
        Price = unitPrice * amount;
    }

    public ErrorOr<Updated> IncreaseAmount(int quantity, int maxAvailableStock)
    {
        if (quantity <= 0)
        {
            return Error.Validation("Quantity must be greater than 0.");
        }

        var newAmount = Amount + quantity;

        if (newAmount > maxAvailableStock)
        {
            return Error.Conflict("Not enough products in stock.");
        }

        Amount = newAmount;
        Price = UnitPrice * Amount;

        return Result.Updated;
    }

    public bool DecreaseAmount(int quantity)
    {
        if (quantity <= 0)
        {
            // Полное удаление товара
            return true;
        }

        if (quantity >= Amount)
        {
            // Удалить товар полностью
            return true;
        }

        // Уменьшить количество товара
        Amount -= quantity;
        Price = UnitPrice * Amount;
        return false;
    }
}
