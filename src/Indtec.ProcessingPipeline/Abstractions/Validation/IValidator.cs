namespace Indtec.ProcessingPipeline.Abstractions.Validation;

public interface IValidator<in T>
{
    Task<ValidationResult> ValidateAsync(
        T target,
        ValidationContext context,
        CancellationToken cancellationToken = default);
}
