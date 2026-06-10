using ElectronicStore.Api.Extensions;
using ElectronicStore.Api.Requests.Products;
using ElectronicStore.Application.Commands.Products;
using ElectronicStore.Application.Queries.Products;
using ElectronicStore.Application.Responses;
using ElectronicStore.Domain.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicStore.Api.Controllers;

[ApiController]
[Route("/api/v1/products")]
public class ProductController : ControllerBase
{
    [HttpGet("{productId:long}")]
    public async Task<IActionResult> GetProductById(
        [FromRoute] long productId,
        [FromServices] IRequestHandler<GetProductByIdQuery, ErrorOr<ProductResponse>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new GetProductByIdQuery(productId), cancellationToken);

        return this.ToActionResult(result, ErrorMessages.FailedToGetProduct);
    }

    [HttpGet]
    public async Task<IActionResult> SearchProducts(
        [FromQuery] string? nameMatch,
        [FromQuery] long? brandId,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] double? minRating,
        [FromServices] IRequestHandler<SearchProductsQuery, IReadOnlyCollection<ProductResponse>> handler,
        CancellationToken cancellationToken)
    {
        var query = new SearchProductsQuery
        {
            NameMatch = nameMatch,
            BrandId = brandId,
            MinimalPrice = minPrice,
            MaximumPrice = maxPrice,
            MinimalRating = minRating
        };

        var result = await handler.Handle(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(
        [FromBody] CreateProductRequest request,
        [FromServices] IRequestHandler<CreateProductCommand, ErrorOr<Created>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        return this.ToActionResult(result, ErrorMessages.FailedToCreateProduct);
    }

    [HttpPost("{productId}/add")]
    public async Task<IActionResult> AddProduct(
        [FromRoute] long productId,
        [FromQuery] int amount,
        [FromServices] IRequestHandler<AddProductAmountCommand, ErrorOr<Updated>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new AddProductAmountCommand(productId, amount), cancellationToken);

        return this.ToActionResult(result, ErrorMessages.FailedToAddProductAmount);
    }

    [HttpPatch("{productId}")]
    public async Task<IActionResult> Update(
        [FromRoute] long productId,
        [FromBody] UpdateProductInfoRequest request,
        [FromServices] IRequestHandler<UpdateProductInfoCommand, ErrorOr<Updated>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        return this.ToActionResult(result, ErrorMessages.FailedToUpdateProduct);
    }
}
