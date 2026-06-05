using ElectronicStore.Api.Extensions;
using ElectronicStore.Api.Requests.User;
using ElectronicStore.Application.Commands.Users;
using ElectronicStore.Application.Queries.Users;
using ElectronicStore.Application.Responses;
using ElectronicStore.Domain.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicStore.Api.Controllers;

[ApiController]
[Route("/api/v1/users")]
public class UserController : ControllerBase
{
    /// <summary>
    /// Получение всех пользователей системы
    /// </summary>
    /// <response code="200"> Данные успешно возвращены </response>
    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<UserResponse>>> GetAll(
        [FromServices] IRequestHandler<GetAllUsersQuery, IReadOnlyCollection<UserResponse>> handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(new GetAllUsersQuery(), cancellationToken);

        return Ok(result);
    }

    /// <summary>
    /// Получение пользователя по идентификатору
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя </param>
    /// <returns></returns>
    [HttpGet]
    [Route("/{userId}")]
    public async Task<ActionResult<UserResponse>> GetById(
        [FromRoute] long userId,
        [FromServices] IRequestHandler<GetUserByIdQuery, ErrorOr<UserResponse>> handler,
        CancellationToken cancellationToken
    )
    {
        var result = await handler.Handle(new GetUserByIdQuery(userId), cancellationToken);

        return this.ToActionResult(result, ErrorMessages.FailedToGetUser);
    }

    /// <summary>
    /// Создание пользователя
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateRequest request,
        [FromServices] IRequestHandler<CreateUserCommand, ErrorOr<ErrorOr.Created>> handler,
        CancellationToken cancellationToken
    )
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        return this.ToActionResult(result, ErrorMessages.FailedToCreateUser);
    }

    /// <summary>
    /// Обновление информации о пользователе
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя </param>
    /// <returns></returns>
    [HttpPatch]
    [Route("/{userId}")]
    public async Task<IActionResult> TopUpBalance(
        [FromRoute] long userId,
        [FromBody] TopUpBalanceRequest request,
        [FromServices] IRequestHandler<TopUpUserBalanceCommand, ErrorOr<Updated>> handler,
        CancellationToken cancellationToken
    )
    {
        var result = await handler.Handle(request.ToCommand(userId), cancellationToken);

        return this.ToActionResult(result, ErrorMessages.FailedToTopUpUserBalance);
    }

    /// <summary>
    /// Удаление информации о пользователе
    /// </summary>
    /// <param name="userId"> Идентификатор пользователя </param>
    /// <returns></returns>
    [HttpDelete]
    [Route("/{userId}")]
    public async Task<IActionResult> Delete(
        [FromRoute] long userId,
        [FromServices] IRequestHandler<DeleteUserCommand, ErrorOr<Deleted>> handler,
        CancellationToken cancellationToken
    )
    {
        var result = await handler.Handle(new DeleteUserCommand(userId), cancellationToken);

        return this.ToActionResult(result, ErrorMessages.FailedToDeleteUser);
    }
}
