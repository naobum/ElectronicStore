using ElectronicStore.Domain.Contracts.Repositories;
using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Carts;

public class ClearCartCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<ClearCartCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(ClearCartCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserById(request.UserId, cancellationToken);

        if (user == null)
        {
            return Error.NotFound($"User {request.UserId} not found");
        }

        if (user.Cart == null)
        {
            return Error.Failure($"User {request.UserId} does not have a cart");
        }

        user.Cart.Clear();

        await unitOfWork.SaveChanges(cancellationToken);

        return Result.Updated;
    }
}
