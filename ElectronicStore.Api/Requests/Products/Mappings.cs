using ElectronicStore.Application.Commands.Products;

namespace ElectronicStore.Api.Requests.Products;

public static class Mappings
{
    public static CreateProductCommand ToCommand(this CreateProductRequest request)
    {
        return new CreateProductCommand(
            request.BrandId,
            request.Name,
            request.Price,
            request.Amount,
            request.Description);
    }

    public static UpdateProductInfoCommand ToCommand(this UpdateProductInfoRequest request)
    {
        return new UpdateProductInfoCommand(
            request.ProductId,
            request.Price,
            request.Name,
            request.Description);
    }
}
