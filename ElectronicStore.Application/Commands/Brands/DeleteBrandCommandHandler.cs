using ElectronicStore.Domain.Contracts.Repositories;
using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Brands;

public class DeleteBrandCommandHandler(
    IBrandRepository brandRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteBrandCommand, ErrorOr<Deleted>>
{
    public async Task<ErrorOr<Deleted>> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        await brandRepository.Delete(request.BrandId);
        await unitOfWork.SaveChanges(cancellationToken);
        return Result.Deleted;
    }
}
