using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Products;

public record DeleteProductCommand(long ProductId) : IRequest<ErrorOr<Deleted>>;
