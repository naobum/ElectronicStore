using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Users;


public record DeleteUserCommand(long UserId) : IRequest<ErrorOr<Deleted>>;