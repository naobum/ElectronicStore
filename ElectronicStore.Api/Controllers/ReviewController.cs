using ElectronicStore.Api.Extensions;
using ElectronicStore.Api.Requests.Brands;
using ElectronicStore.Api.Requests.Reviews;
using ElectronicStore.Application.Commands.Reviews;
using ElectronicStore.Domain.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicStore.Api.Controllers;

[ApiController]
[Route("/api/v1")]
public class ReviewController : ControllerBase
{
    [HttpPost("/users/{userId}/add-review/{productId}")]
    public async Task<IActionResult> AddReview(
        [FromRoute] long userId,
        [FromRoute] long productId,
        [FromBody] CreateReviewRequest request,
        [FromServices] IRequestHandler<CreateReviewCommand, ErrorOr<Created>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(userId, productId), cancellationToken);

        return this.ToActionResult(result, ErrorMessages.FailedToCreateReview);
    }
}
