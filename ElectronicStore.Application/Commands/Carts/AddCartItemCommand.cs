using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Carts;

public class AddCartItemCommand : IRequest<ErrorOr<Updated>>
{
    public required long UserId { get; init; }

    public required long ProductId { get; init; }

    public required int Quantity { get; init; }
}
