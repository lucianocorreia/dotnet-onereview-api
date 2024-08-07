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

    public async Task<Review?> GetByIdAsync(Guid reviewId, Guid productId)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync();

        string query = "SELECT id, productid, rating, text FROM reviews WHERE id = @Id and productid = @ProductId";

        return await connection.QueryFirstAsync<Review>(query, new { Id = reviewId, productId = productId });
    }

    public async Task<bool> ExistsAsync(Guid productId)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync();

        string query = "SELECT COUNT(*) FROM reviews WHERE productid = @ProductId";

        var result = await connection.ExecuteScalarAsync<int>(query, new { productid = productId });

        return result > 0;
    }

}
