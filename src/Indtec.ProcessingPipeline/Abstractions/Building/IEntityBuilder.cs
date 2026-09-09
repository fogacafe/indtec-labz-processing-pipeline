namespace Indtec.ProcessingPipeline.Abstractions.Building;

public interface IEntityBuilder<in TCommand, TEntity>
{
    Task BuildAsync(TCommand command, TEntity entity, CancellationToken cancellationToken = default);
}
