using ElectronicStore.Application.Commands.Users;

namespace ElectronicStore.Api.Requests.User;

public static class Mappings
{
    public static CreateUserCommand ToCommand(this CreateRequest request)
    {
        return new CreateUserCommand(request.Name);
    }

    public static TopUpUserBalanceCommand ToCommand(this TopUpBalanceRequest request, long userId)
    {
        return new TopUpUserBalanceCommand(userId, request.Money);
    }
}