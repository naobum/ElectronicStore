using System.ComponentModel.DataAnnotations;

namespace ElectronicStore.Api.Requests.Reviews;

public record CreateReviewRequest
{
    [Required]
    public int Rating { get; init; }

    public string? Comment { get; init; }
}
