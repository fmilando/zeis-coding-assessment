using Microsoft.EntityFrameworkCore;
using Zeiss.Products.Application.Interfaces.Repositories;
using System.Data;

namespace Zeiss.Products.Infrastructure.Database.Repositories;

internal sealed class UnitOfWork(PersistenceDbContext context) : IUnitOfWork
{
    private volatile IDbTransaction? _transaction;
    
    public async Task StartAsync(IsolationLevel level, CancellationToken cancellationToken)
    {
        if (_transaction is not null)
        {
            throw new InvalidOperationException("The transaction already started.");
        }

        var connection = context.Database.GetDbConnection();
        await connection.OpenAsync(cancellationToken);
        _transaction = await connection.BeginTransactionAsync(level, cancellationToken);
    }

    public async Task CompleteAsync(CancellationToken cancellationToken)
    {
        if (_transaction is not null)
        {
            await context.SaveChangesAsync(cancellationToken);
            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
        }
    }

    public Task DiscardAsync(CancellationToken cancellationToken)
    {
        if (_transaction is not null)
        {
            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
        }
        
        return Task.CompletedTask;
    }
}