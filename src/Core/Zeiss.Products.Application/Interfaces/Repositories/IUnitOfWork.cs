namespace Zeiss.Products.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    Task StartAsync(CancellationToken cancellationToken);
    Task CompleteAsync(CancellationToken cancellationToken);
    Task DiscardAsync(CancellationToken cancellationToken);
}