using OneReview.Domain;
using OneReview.Errors;
using OneReview.Persistence.Database.Repositories;

namespace OneReview.Services;

public class ReviewService(ReviewsRepository reviewsRepository, ProductsRepository productsRepository)
{
    private readonly ReviewsRepository _reviewsRepository = reviewsRepository;
    private readonly ProductsRepository _productsRepository = productsRepository;

    public async Task Create(Review review)
    {
        if (!await _productsRepository.ExistsAsync(review.ProductId))
        {
            throw new NotFoundException("Product does not exist");
        }

        await _reviewsRepository.CreateAsync(review);
    }

    public async Task<Review?> Get(Guid reviewId, Guid productId)
    {
        if (!await _productsRepository.ExistsAsync(productId))
        {
            throw new NotFoundException("Product does not exist");
        }

        return await _reviewsRepository.GetByIdAsync(reviewId, productId);
    }
}
