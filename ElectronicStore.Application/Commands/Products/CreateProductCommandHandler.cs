using ElectronicStore.Domain.Contracts.Repositories;
using ElectronicStore.Domain.Models;
using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Products;

public class CreateProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateProductCommand, ErrorOr<Created>>
{
    public async Task<ErrorOr<Created>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var creationResult = Product.Create(
            request.BrandId,
            request.Name,
            request.Price,
            request.Amount,
            request.Description);

        if (creationResult.IsError)
        {
            return creationResult.FirstError;
        }

        if (request.Rating is not null)
        {
            var ratingResult = creationResult.Value.UpdateRating(request.Rating.Value);

            if (ratingResult.IsError)
            {
                return ratingResult.FirstError;
            }
        }

        await productRepository.Create(creationResult.Value, cancellationToken);

        await unitOfWork.SaveChanges(cancellationToken);

        return Result.Created;
    }
}
