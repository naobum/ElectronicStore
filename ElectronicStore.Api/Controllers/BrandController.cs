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

    [HttpGet("{brandId: long}")]
    public async Task<ActionResult<BrandResponse>> GetBrandById(
        [FromRoute] long brandId,
        [FromServices] IRequestHandler<GetBrandByIdQuery, ErrorOr<BrandResponse>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new GetBrandByIdQuery(brandId), cancellationToken);

        return this.ToActionResult(result, ErrorMessages.FailedToGetBrand);
    }
}
