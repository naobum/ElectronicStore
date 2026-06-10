using ElectronicStore.Application.Responses;
using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Queries.Brands;

public record GetBrandByIdQuery(long Id) : IRequest<ErrorOr<BrandResponse>>;
