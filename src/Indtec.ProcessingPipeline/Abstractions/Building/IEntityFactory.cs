namespace Indtec.ProcessingPipeline.Abstractions.Building;

public interface IEntityFactory<in TCommand, TEntity>
{
    Task<TEntity> CreateAsync(TCommand command, CancellationToken cancellationToken = default);
}
