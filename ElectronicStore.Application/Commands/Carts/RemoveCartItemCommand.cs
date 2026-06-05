using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Carts;

public record RemoveCartItemCommand : IRequest<ErrorOr<Updated>>
{
    public required long ProductId { get; init; }

    public required long UsertId { get; init; }
}
