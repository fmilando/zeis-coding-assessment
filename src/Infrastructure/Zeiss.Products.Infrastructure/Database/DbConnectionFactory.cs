using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Zeiss.Products.Infrastructure.Database;

internal sealed class NpgsqlConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    private readonly string _connectionString = configuration.GetConnectionString(DbConstants.ConnectionStringName)!;

    public IDbConnection Create() => new NpgsqlConnection(_connectionString);
}