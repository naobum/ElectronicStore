using ErrorOr;

namespace ElectronicStore.Domain.Models;

public class User
{
    public long Id { get; private set; }
    public string Name { get; private set; }
    public decimal Balance { get; private set; }
    public Cart? Cart { get; private set; }

    public User(string name)
    {
        Name = name;
        Balance = 0;
    }

    public ErrorOr<Created> SetCart(Cart cart)
    {
        if (Cart != null)
        {
            return Error.Conflict($"User {Id} already has a cart");
        }

        Cart = cart;

        return Result.Created;
    }

    public ErrorOr<Updated> AddFunds(decimal amount)
    {
        if (amount <= 0)
        {
            return Error.Validation("Amount must be greater than zero.");
        }

        Balance += amount;

        return Result.Updated;
    }

    public ErrorOr<Updated> DecreaseBalance(decimal amount)
    {
        if (amount <= 0)
        {
            return Result.Updated;
        }

        if (amount > Balance)
        {
            return Error.Conflict($"User {Id} does not have enough money");
        }

        Balance -= amount;

        return Result.Updated;
    }
}
