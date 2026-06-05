using System.ComponentModel.DataAnnotations;

namespace ElectronicStore.Api.Requests.User;

public record TopUpBalanceRequest
{
    [Required]
    public decimal Money { get; init; }
}
