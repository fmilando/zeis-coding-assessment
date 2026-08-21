using System.Data;

namespace Zeiss.Products.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    Task StartAsync(IsolationLevel level, CancellationToken cancellationToken);
    Task CompleteAsync(CancellationToken cancellationToken);
    Task DiscardAsync(CancellationToken cancellationToken);
}