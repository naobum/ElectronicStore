using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Carts;

public record ClearCartCommand(long UserId) : IRequest<ErrorOr<Updated>>;
