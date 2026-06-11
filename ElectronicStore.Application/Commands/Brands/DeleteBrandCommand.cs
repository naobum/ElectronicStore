using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Brands;

public record DeleteBrandCommand(long BrandId) : IRequest<ErrorOr<Deleted>>;
