using ElectronicStore.Application.Responses;
using MediatR;

namespace ElectronicStore.Application.Queries.Users;

public record GetAllUsersQuery() : IRequest<IReadOnlyCollection<UserResponse>>;
