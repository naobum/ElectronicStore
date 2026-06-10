using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Carts;

public record BuyCommand(long UserId) : IRequest<ErrorOr<Success>>;
