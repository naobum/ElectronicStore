using ElectronicStore.Application.Responses;
using MediatR;

namespace ElectronicStore.Application.Queries.Brands;

public record GetBrandsQuery() : IRequest<IReadOnlyCollection<BrandResponse>>;
