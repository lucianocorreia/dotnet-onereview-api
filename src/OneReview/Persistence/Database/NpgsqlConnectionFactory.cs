using System.Data;

using Npgsql;

namespace OneReview.Persistence.Database;

public class NpgsqlConnectionFactory(string connectionString) : IDbConnectionFactory
{
    private readonly string _connectionString = connectionString;

    public async Task<IDbConnection> CreateConnectionAsync()
    {
        NpgsqlConnection connection = new(_connectionString);

        await connection.OpenAsync();

        return connection;
    }
}
