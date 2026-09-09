namespace Indtec.ProcessingPipeline.Abstractions.Building;

public sealed class EntityBuilder<TCommand, TEntity>(IEnumerable<IBuilderStep<TCommand, TEntity>> steps)
    : IEntityBuilder<TCommand, TEntity>
{
    public async Task BuildAsync(TCommand command, TEntity entity, CancellationToken cancellationToken = default)
    {
        foreach (var step in steps)
            await step.ExecuteAsync(command, entity, cancellationToken);
    }
}
