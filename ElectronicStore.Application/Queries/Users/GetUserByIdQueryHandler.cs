using ElectronicStore.Application.Responses;
using ElectronicStore.Domain.Contracts.Repositories;
using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Queries.Users;

public class GetUserByIdQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, ErrorOr<UserResponse>>
{
    public async Task<ErrorOr<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserById(request.UserId, cancellationToken);

        if (user == null)
        {
            return Error.NotFound($"User {request.UserId} not found");
        }

        return user.ToResponse();
    }
}
