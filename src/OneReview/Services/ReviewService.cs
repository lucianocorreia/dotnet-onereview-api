using OneReview.Domain;

namespace OneReview.Services;

public class ReviewService
{
    private static readonly List<Review> ReviewRepository = [];

    public void Create(Review review)
    {
        ReviewRepository.Add(review);
    }

    public Review? Get(Guid reviewId, Guid productId)
    {
        return ReviewRepository.Find(r => r.Id == reviewId && r.ProductId == productId);
    }
}
