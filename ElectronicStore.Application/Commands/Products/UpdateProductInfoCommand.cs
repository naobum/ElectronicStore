using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Products;

public record UpdateProductInfoCommand(
    long ProductId,
    decimal? Price,
    string? Name,
    string? Description)
    : IRequest<ErrorOr<Updated>>;
