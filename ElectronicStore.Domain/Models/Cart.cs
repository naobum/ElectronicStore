using ErrorOr;

namespace ElectronicStore.Domain.Models;

public class Cart
{

    private readonly List<OrderItem> _items;

    public IReadOnlyCollection<OrderItem> Items => _items;
    public long Id { get; private set; }

    public long UserId { get; private set; }
    public User? User { get; private set; }

    public Cart()
    {
        _items = [];
    }

    public void SetUser(User user)
    {
        User = user;
    }

    public IReadOnlyCollection<OrderItem> GetItems()
    {
        return _items.AsReadOnly();
    }

    public ErrorOr<Updated> AddItem(Product product, int quantity, decimal price, int availableStock)
    {
        if (quantity <= 0)
        {
            return Error.Validation("Quantity must be greater than 0.");
        }

        if (availableStock < quantity)
        {
            return Error.Conflict("Not enough products in stock.");
        }

        var existingItem = _items.FirstOrDefault(item => item.Product?.Id == product.Id);

        if (existingItem != null)
        {
            var result = existingItem.IncreaseAmount(quantity, availableStock);

            if (result.IsError)
            {
                return result;
            }
        }
        else
        {
            var item = new OrderItem(product, quantity, price);
            _items.Add(item);
        }

        return Result.Updated;
    }

    public void RemoveItem(long productId, int quantity = 0)
    {
        var cartItem = _items.FirstOrDefault(item => item.Product?.Id == productId);

        if (cartItem == null)
        {
            return;
        }

        var shouldRemove = cartItem.DecreaseAmount(quantity);

        if (shouldRemove)
        {
            _items.Remove(cartItem);
        }
    }

    public void Clear()
    {
        _items.Clear();
    }
}
