using ElectronicStore.Application.Responses;
using ElectronicStore.Domain.Contracts.Repositories;
using MediatR;

namespace ElectronicStore.Application.Queries.Users;

public class GetAllUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetAllUsersQuery, IReadOnlyCollection<UserResponse>>
{
    public async Task<IReadOnlyCollection<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetUsers(cancellationToken);

        return users
            .Select(user => user.ToResponse())
            .ToArray();
    }
}
