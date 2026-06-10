using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Reviews;

public record CreateReviewCommand(
    long UserId,
    long ProductId,
    int Rating,
    string? Comment)
    : IRequest<ErrorOr<Created>>;