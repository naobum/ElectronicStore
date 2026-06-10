using ElectronicStore.Application.Commands.Reviews;

namespace ElectronicStore.Api.Requests.Reviews;

public static class Mappings
{
    public static CreateReviewCommand ToCommand(this CreateReviewRequest request, long userId, long productId)
    {
        return new CreateReviewCommand(userId, productId, request.Rating, request.Comment);
    }
}
