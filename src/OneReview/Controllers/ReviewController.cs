using Microsoft.AspNetCore.Mvc;

using OneReview.Domain;
using OneReview.Services;

namespace OneReview.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewController(ReviewService reviewService) : ControllerBase
{
    private readonly ReviewService _reviewService = reviewService;

    [HttpPost]
    public IActionResult Create(CreateReviewRequest request)
    {
        var review = request.ToDomain();

        // create review in database
        _reviewService.Create(review);

        return CreatedAtAction(
            actionName: nameof(Get),
            routeValues: new
            {
                ReviewId = review.Id,
                ProductId = review.ProductId
            },
            value: ReviewResponse.FromDomain(review));
    }

    [HttpGet("{productId:guid}/reviews/{reviewId:guid}")]
    public IActionResult Get(Guid productId, Guid reviewId)
    {
        var review = _reviewService.Get(reviewId, productId);

        return review is null
            ? Problem(statusCode: StatusCodes.Status404NotFound, title: "Review not found", detail: $"Review with id {reviewId} for product with id {productId} was not found.")
            : Ok(ReviewResponse.FromDomain(review));
    }
}

public record CreateReviewRequest(Guid ProductId, int Rating, string Text)
{
    public Review ToDomain()
    {
        return new Review()
        {
            ProductId = ProductId,
            Rating = Rating,
            Text = Text
        };
    }
}

public record ReviewResponse(Guid Id, Guid ProductId, int Rating, string Text)
{
    public static ReviewResponse FromDomain(Review review) =>
        new(
            Id: review.Id,
            ProductId: review.ProductId,
            Rating: review.Rating,
            Text: review.Text
        );
}
