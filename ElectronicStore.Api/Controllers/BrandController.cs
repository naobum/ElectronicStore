using ElectronicStore.Api.Extensions;
using ElectronicStore.Api.Requests.Brands;
using ElectronicStore.Application.Commands.Brands;
using ElectronicStore.Application.Queries.Brands;
using ElectronicStore.Application.Responses;
using ElectronicStore.Domain.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicStore.Api.Controllers;

[ApiController]
[Route("/api/v1/brands")]
public class BrandController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateBrand(
        [FromBody] CreateBrandRequest request,
        [FromServices] IRequestHandler<CreateBrandCommand, Created> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        return Created();
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<BrandResponse>>> GetBrands(
        [FromServices] IRequestHandler<GetBrandsQuery, IReadOnlyCollection<BrandResponse>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new GetBrandsQuery(), cancellationToken);

        return Ok(result);
    }

    [HttpPut("{brandId:long}")]
    public async Task<IActionResult> UpdateBrand(
        [FromRoute] long brandId,
        [FromBody] CreateBrandRequest request,
        [FromServices] IRequestHandler<UpdateBrandCommand, ErrorOr<Updated>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new UpdateBrandCommand(brandId, request.Name, request.Description), cancellationToken);

        return this.ToActionResult(result, ErrorMessages.FailedToUpdateBrand);
    }

    [HttpDelete("{brandId:long}")]
    public async Task<IActionResult> DeleteBrand(
        [FromRoute] long brandId,
        [FromServices] IRequestHandler<DeleteBrandCommand, ErrorOr<Deleted>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new DeleteBrandCommand(brandId), cancellationToken);

        return this.ToActionResult(result, ErrorMessages.FailedToDeleteBrand);
    }

    [HttpGet("{brandId:long}")]
    public async Task<ActionResult<BrandResponse>> GetBrandById(
        [FromRoute] long brandId,
        [FromServices] IRequestHandler<GetBrandByIdQuery, ErrorOr<BrandResponse>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new GetBrandByIdQuery(brandId), cancellationToken);

        return this.ToActionResult(result, ErrorMessages.FailedToGetBrand);
    }
}
