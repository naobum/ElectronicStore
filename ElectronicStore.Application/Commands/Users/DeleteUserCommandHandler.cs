using ElectronicStore.Domain.Contracts.Repositories;
using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Users;

public class DeleteUserCommandHandler(IUserRepository userRepository) : IRequestHandler<DeleteUserCommand, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        await userRepository.DeleteUser(request.UserId, cancellationToken);

        return Result.Deleted;
    }
}
