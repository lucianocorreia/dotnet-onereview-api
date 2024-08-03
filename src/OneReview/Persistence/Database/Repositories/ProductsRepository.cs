using System.Data;

using Dapper;

using OneReview.Domain;

using Throw;

namespace OneReview.Persistence.Database.Repositories;

public class ProductsRepository(IDbConnectionFactory dbConnectionFactory)
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task CreateAsync(Product product)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync();

        string query = "INSERT INTO Products (id, name, category, subcategory) VALUES (@Id, @Name, @Category, @SubCategory)";

        var result = await connection.ExecuteAsync(query, product);

        result.Throw().IfNegativeOrZero();
    }

    public async Task<Product?> GetByIdAsync(Guid productId)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync();

        string query = "SELECT id, name, category, subcategory FROM products WHERE id = @Id";

        return await connection.QueryFirstAsync<Product>(query, new { Id = productId });
    }

    public async Task<bool> ExistsAsync(Guid productId)
    {
        using IDbConnection connection = await _dbConnectionFactory.CreateConnectionAsync();

        string query = "SELECT COUNT(*) FROM products WHERE id = @Id";

        var result = await connection.ExecuteScalarAsync<int>(query, new { id = productId });

        return result > 0;
    }
}
