using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Users;

public record TopUpUserBalanceCommand(long UserId, decimal Money) : IRequest<ErrorOr<Updated>>;
