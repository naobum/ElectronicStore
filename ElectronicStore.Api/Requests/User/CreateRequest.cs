using System.ComponentModel.DataAnnotations;

namespace ElectronicStore.Api.Requests.User;

public record CreateRequest
{
    [Required]
    public string Name { get; init; } = default!;
}
