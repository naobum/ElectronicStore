using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Users;

public record CreateUserCommand(string Name) : IRequest<ErrorOr<Created>>;
