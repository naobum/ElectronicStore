using ElectronicStore.Application.Responses;
using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Queries.Users;

public record GetUserByIdQuery(long UserId) : IRequest<ErrorOr<UserResponse>>;
