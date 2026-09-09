using Indtec.ProcessingPipeline.Abstractions.Building;
using Indtec.ProcessingPipeline.Abstractions.Validation;

namespace Indtec.ProcessingPipeline.Abstractions.Processing;

public sealed record ProcessingResult<TEntity>(
    TEntity Entity,
    ValidationResult Validation,
    bool Success)
{
    public IReadOnlyCollection<ValidationMessage> Messages => Validation.Messages;
}

public interface IEntityProcessor<in TCommand, TEntity>
{
    Task<ProcessingResult<TEntity>> ProcessAsync(
        TCommand command,
        ProcessingIntent intent,
        CancellationToken cancellationToken = default);
}

public sealed class EntityProcessor<TCommand, TEntity>(
    IEntityFactory<TCommand, TEntity> factory,
    IEntityBuilder<TCommand, TEntity> builder,
    IValidator<TEntity> validator,
    IValidationPolicy validationPolicy)
    : IEntityProcessor<TCommand, TEntity>
{
    public async Task<ProcessingResult<TEntity>> ProcessAsync(
        TCommand command,
        ProcessingIntent intent,
        CancellationToken cancellationToken = default)
    {
        var entity = await factory.CreateAsync(command, cancellationToken);
        await builder.BuildAsync(command, entity, cancellationToken);

        var validation = await validator.ValidateAsync(
            entity,
            new ValidationContext(intent),
            cancellationToken);

        return new ProcessingResult<TEntity>(
            entity,
            validation,
            validationPolicy.CanProceed(intent, validation));
    }
}
