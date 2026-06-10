using ElectronicStore.Api.Extensions;
using ElectronicStore.Api.Requests.Carts;
using ElectronicStore.Application.Commands.Carts;
using ElectronicStore.Domain.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicStore.Api.Controllers;

[Route("/api/v1/users/{userId}/cart")]
public class CartController : ControllerBase
{
    [HttpPost("clear")]
    public async Task<IActionResult> ClearCart(
        [FromRoute] long userId,
        [FromServices] IRequestHandler<ClearCartCommand, ErrorOr<Updated>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new ClearCartCommand(userId), cancellationToken);

        return this.ToActionResult(result, ErrorMessages.FailedToClearCart);
    }

    [HttpPost("remove-item")]
    public async Task<IActionResult> RemoveCartItem(
        [FromRoute] long userId,
        [FromBody] RemoveCartItemRequest request,
        [FromServices] IRequestHandler<RemoveCartItemCommand, ErrorOr<Updated>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(userId), cancellationToken);

        return this.ToActionResult(result, ErrorMessages.FailedToRemoveCartItem);
    }

    [HttpPost("add-item")]
    public async Task<IActionResult> AddCartItem(
        [FromRoute] long userId,
        [FromBody] AddCartItemRequest request,
        [FromServices] IRequestHandler<AddCartItemCommand, ErrorOr<Updated>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(userId), cancellationToken);

        return this.ToActionResult(result, ErrorMessages.FailedToAddCartItem);
    }

    [HttpPost("buy")]
    public async Task<IActionResult> Buy(
        [FromRoute] long userId,
        [FromServices] IRequestHandler<BuyCommand, ErrorOr<Success>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new BuyCommand(userId), cancellationToken);

        return this.ToActionResult(result, ErrorMessages.FailedToBuy);
    }
}
