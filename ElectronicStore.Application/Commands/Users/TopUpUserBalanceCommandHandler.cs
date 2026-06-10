using ElectronicStore.Domain.Contracts.Repositories;
using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Users;

public class TopUpUserBalanceCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<TopUpUserBalanceCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(TopUpUserBalanceCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserById(request.UserId, cancellationToken);

        if (user == null)
        {
            return Error.NotFound($"User {request.UserId} not found");
        }

        var result = user.AddFunds(request.Money);

        if (result.IsError)
        {
            return result;
        }

        await unitOfWork.SaveChanges(cancellationToken);

        return Result.Updated;
    }
}
