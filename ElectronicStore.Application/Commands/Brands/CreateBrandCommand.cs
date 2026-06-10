using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Brands;

public record CreateBrandCommand(string Name, string? Description) : IRequest<Created>;
