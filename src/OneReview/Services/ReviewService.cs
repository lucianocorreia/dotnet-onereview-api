using OneReview.Domain;
using OneReview.Persistence.Database.Repositories;

namespace OneReview.Services;

public class ReviewService(ReviewsRepository reviewsRepository)
{
    private readonly ReviewsRepository _reviewsRepository = reviewsRepository;

    public async Task Create(Review review)
    {
        await _reviewsRepository.CreateAsync(review);
    }

    public async Task<Review?> Get(Guid reviewId, Guid productId)
    {
        return await _reviewsRepository.GetByIdAsync(reviewId, productId);
    }
}
