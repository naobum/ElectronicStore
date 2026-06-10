using System.ComponentModel.DataAnnotations;

namespace ElectronicStore.Api.Requests.Carts;

public class RemoveCartItemRequest
{
    [Required]
    public long ProductId { get; init; }
}
