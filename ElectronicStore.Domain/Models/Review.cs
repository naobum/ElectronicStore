using ErrorOr;

namespace ElectronicStore.Domain.Models;

public class Review
{
    private Review(long userId, long productId, int rating, string? comment)
    {
        UserId = userId;
        ProductId = productId;
        Rating = rating;
        Comment = comment;
    }

    public long Id { get; private set; }
    public long UserId { get; private set; }
    public long ProductId { get; private set; }
    public int Rating { get; private set; }
    public string? Comment { get; private set; }

    public User? User { get; set; }
    public Product? Product { get; set; }

    /// <summary>
    /// Создает новый отзыв с валидацией
    /// </summary>
    public static ErrorOr<Review> Create(long userId, long productId, int rating, string? comment)
    {
        if (rating < 1 || rating > 5)
        {
            return Error.Validation("Rating must be between 1 and 5");
        }

        if (!string.IsNullOrEmpty(comment) && comment.Length > 1000)
        {
            return Error.Validation("Comment cannot exceed 1000 characters");
        }

        return new Review(userId, productId, rating, comment);
    }

    /// <summary>
    /// Обновляет оценку отзыва с валидацией
    /// </summary>
    public ErrorOr<Updated> UpdateRating(int newRating)
    {
        if (newRating < 1 || newRating > 5)
        {
            return Error.Validation("Rating must be between 1 and 5");
        }

        Rating = newRating;
        return Result.Updated;
    }

    /// <summary>
    /// Обновляет комментарий отзыва с валидацией
    /// </summary>
    public ErrorOr<Updated> UpdateComment(string? newComment)
    {
        if (!string.IsNullOrEmpty(newComment) && newComment.Length > 1000)
        {
            return Error.Validation("Comment cannot exceed 1000 characters");
        }

        Comment = newComment;
        return Result.Updated;
    }

    /// <summary>
    /// Обновляет оценку и комментарий одновременно
    /// </summary>
    public ErrorOr<Updated> Update(int? newRating = null, string? newComment = null)
    {
        if (newRating.HasValue)
        {
            var ratingError = UpdateRating(newRating.Value);
            if (ratingError.IsError)
            {
                return ratingError;
            }
        }

        if (newComment is not null)
        {
            var commentError = UpdateComment(newComment);
            if (commentError.IsError)
            {
                return commentError;
            }
        }

        return Result.Updated;
    }
}