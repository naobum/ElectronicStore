using ElectronicStore.Domain.Contracts.Repositories;
using ElectronicStore.Domain.Models;
using ErrorOr;
using MediatR;

namespace ElectronicStore.Application.Commands.Reviews;

public class CreateReviewCommandHandler(
    IUserRepository userRepository,
    IProductRepository productRepository,
    IReviewRepository reviewRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateReviewCommand, ErrorOr<Created>>
{
    public async Task<ErrorOr<Created>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetUserById(request.UserId, cancellationToken);
        var product = await productRepository.GetProductById(request.ProductId, cancellationToken);

        if (user == null)
        {
            return Error.NotFound($"User {request.UserId} not found");
        }

        if (product == null)
        {
            return Error.NotFound($"Product {request.ProductId} not found");
        }

        var reviewCreationResult = Review.Create(request.UserId, request.ProductId, request.Rating, request.Comment);

        if (reviewCreationResult.IsError)
        {
            return reviewCreationResult.FirstError;
        }

        var newReview = reviewCreationResult.Value;

        var productReviews = await reviewRepository.GetReviewsByProductId(newReview.ProductId, cancellationToken);

        var newRating = (productReviews.Sum(review => review.Rating) + newReview.Rating) / (double)(productReviews.Count + 1);

        var ratingUpdateResult = product.UpdateRating(newRating);

        if (ratingUpdateResult.IsError)
        {
            return ratingUpdateResult.FirstError;
        }

        await unitOfWork.SaveChanges();

        return Result.Created;
    }
}
