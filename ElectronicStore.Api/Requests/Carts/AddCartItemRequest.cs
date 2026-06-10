using System.ComponentModel.DataAnnotations;

namespace ElectronicStore.Api.Requests.Carts;

public record AddCartItemRequest
{
    [Required]
    public long ProductId { get; init; }

    [Required]
    public int Amount { get; init; }
}
