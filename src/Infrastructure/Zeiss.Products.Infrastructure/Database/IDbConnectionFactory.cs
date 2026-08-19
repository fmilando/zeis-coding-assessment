using System.Data;

namespace Zeiss.Products.Infrastructure.Database;

internal interface IDbConnectionFactory
{
    IDbConnection Create();
}