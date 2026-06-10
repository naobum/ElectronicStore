using ElectronicStore.Application.Responses;
using ElectronicStore.Domain.Contracts.Repositories;
using MediatR;

namespace ElectronicStore.Application.Queries.Brands;

public class GetBrandsQueryHandler(IBrandRepository brandRepository)
    : IRequestHandler<GetBrandsQuery, IReadOnlyCollection<BrandResponse>>
{
    public async Task<IReadOnlyCollection<BrandResponse>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
    {
        var brands = await brandRepository.GetAll();

        return brands
            .Select(brand => brand.ToResponse())
            .ToArray();
    }
}
