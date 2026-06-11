using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Products;

public record UpdateProductInfoCommand(
    long ProductId,
    decimal? Price,
    long? BrandId,
    string? Name,
    string? Description,
    double? Rating = null,
    int? Amount = null)
    : IRequest<ErrorOr<Updated>>;
