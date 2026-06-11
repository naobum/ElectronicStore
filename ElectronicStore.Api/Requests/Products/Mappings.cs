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
            request.Description,
            request.Rating);
    }

    public static UpdateProductInfoCommand ToCommand(this UpdateProductInfoRequest request, long productId)
    {
        return new UpdateProductInfoCommand(
            productId,
            request.Price,
            request.BrandId,
            request.Name,
            request.Description,
            request.Rating,
            request.Amount);
    }
}
