using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Zeiss.Products.Infrastructure.Database;

internal class DbErrorInterceptor(
    ILogger<DbErrorInterceptor> logger
) : DbCommandInterceptor
{
    public override void CommandFailed(DbCommand command, CommandErrorEventData eventData)
    {
        logger.LogError(
            eventData.Exception,
            "Database command faulted: {CommandText}", 
            command.CommandText);
        
        base.CommandFailed(command, eventData);
    }
}