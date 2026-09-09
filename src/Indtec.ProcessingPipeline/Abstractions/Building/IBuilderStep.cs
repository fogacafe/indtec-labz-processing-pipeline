namespace Indtec.ProcessingPipeline.Abstractions.Building;

public interface IBuilderStep<in TCommand, in TEntity>
{
    Task ExecuteAsync(TCommand command, TEntity entity, CancellationToken cancellationToken = default);
}
