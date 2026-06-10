using ElectronicStore.Domain.Contracts.Repositories;
using ElectronicStore.Domain.Models;
using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Brands;

public class CreateBrandCommandHandler(
    IBrandRepository brandRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateBrandCommand, Created>
{
    public async Task<Created> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = new Brand(request.Name, request.Description);

        await brandRepository.Create(brand);

        await unitOfWork.SaveChanges(cancellationToken);

        return Result.Created;
    }
}
