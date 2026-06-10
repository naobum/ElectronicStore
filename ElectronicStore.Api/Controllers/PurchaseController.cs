using ElectronicStore.Application.Queries.Purchases;
using ElectronicStore.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicStore.Api.Controllers;

[ApiController]
[Route("/api/v1")]
public class PurchaseController : ControllerBase
{

    [HttpGet("user/{userId:long}/purchases")]
    public async Task<ActionResult<IReadOnlyCollection<PurchaseResponse>>> GetPurchasesByUser(
        [FromRoute] long userId,
        [FromServices] IRequestHandler<GetPurchasesByUserIdQuery, IReadOnlyCollection<PurchaseResponse>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new GetPurchasesByUserIdQuery(userId), cancellationToken);

        return Ok(result);
    }

}
