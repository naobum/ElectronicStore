using ElectronicStore.Application.Commands.Brands;

namespace ElectronicStore.Api.Requests.Brands;

public static class Mappings
{
    public static CreateBrandCommand ToCommand(this CreateBrandRequest request)
    {
        return new CreateBrandCommand(request.Name, request.Description);
    }
}
