using ElectronicStore.Application.Responses;
using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Queries.Products;

public record GetProductByIdQuery(long Id) : IRequest<ErrorOr<ProductResponse>>;
