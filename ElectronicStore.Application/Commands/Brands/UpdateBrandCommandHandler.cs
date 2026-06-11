using ElectronicStore.Domain.Contracts.Repositories;
using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Brands;

public class UpdateBrandCommandHandler(
    IBrandRepository brandRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateBrandCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await brandRepository.GetById(request.BrandId);

        if (brand is null)
        {
            return Error.NotFound($"Brand {request.BrandId} not found");
        }

        var updated = brand with { Name = request.Name, Description = request.Description };

        await brandRepository.Update(updated);
        await unitOfWork.SaveChanges(cancellationToken);

        return Result.Updated;
    }
}
