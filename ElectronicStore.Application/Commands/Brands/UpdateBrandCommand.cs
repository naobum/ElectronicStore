using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Brands;

public record UpdateBrandCommand(long BrandId, string Name, string? Description) : IRequest<ErrorOr<Updated>>;
