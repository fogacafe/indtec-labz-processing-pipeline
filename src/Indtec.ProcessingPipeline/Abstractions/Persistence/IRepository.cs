namespace Indtec.ProcessingPipeline.Abstractions.Persistence;

public interface IRepository<TEntity>
{
    Task<TEntity?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task SaveAsync(TEntity entity, CancellationToken cancellationToken = default);
}
