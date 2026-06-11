using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Products;

public record CreateProductCommand(
    long BrandId,
    string Name,
    decimal Price,
    int Amount,
    string? Description,
    double? Rating = null
) : IRequest<ErrorOr<Created>>;
