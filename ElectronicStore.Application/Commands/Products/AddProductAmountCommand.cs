using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Products;

public record AddProductAmountCommand(long ProductId, int Amount) : IRequest<ErrorOr<Updated>>;
