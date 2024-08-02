using System.Data;

using Dapper;

using OneReview.Domain;

using Throw;

namespace OneReview.Persistence.Database.Repositories;

public class ReviewsRepository(IDbConnectionFactory dbConnectionFactory)
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task CreateAsync(Review review)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync();

        string query = "INSERT INTO reviews (id, productid, rating, text) VALUES (@Id, @ProductId, @Rating, @Text)";

        var result = await connection.ExecuteAsync(query, review);

        result.Throw().IfNegativeOrZero();
    }

    internal async Task<Review?> GetByIdAsync(Guid reviewId, Guid productId)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync();

        string query = "SELECT id, productid, rating, text FROM reviews WHERE id = @Id and productid = @ProductId";

        return await connection.QueryFirstAsync<Review>(query, new { Id = reviewId, productId = productId });
    }
}
