using ElectronicStore.Application.Responses;
using ElectronicStore.Domain.Contracts.Repositories;
using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Queries.Brands;

public class GetBrandByIdQueryHandler(IBrandRepository brandRepository) : IRequestHandler<GetBrandByIdQuery, ErrorOr<BrandResponse>>
{
    public async Task<ErrorOr<BrandResponse>> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        var brand = await brandRepository.GetById(request.Id);

        if (brand == null)
        {
            return Error.NotFound($"Brand {request.Id} not found");
        }

        return brand.ToResponse();
    }
}
