using ElectronicStore.Domain.Contracts.Repositories;
using ElectronicStore.Domain.Models;
using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Users;

public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateUserCommand, ErrorOr<Created>>
{
    public async Task<ErrorOr<Created>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(request.Name);

        await userRepository.CreateUser(user, cancellationToken);

        var cart = new Cart();
        var result = user.SetCart(cart);

        if (result.IsError)
        {
            return result;
        }

        await unitOfWork.SaveChanges(cancellationToken);

        return Result.Created;
    }
}
