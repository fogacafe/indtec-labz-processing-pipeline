namespace Indtec.ProcessingPipeline.Abstractions.Validation;

public interface IValidationRule<in T>
{
    bool IsApplicable(T target, ValidationContext context);

    Task<IEnumerable<ValidationMessage>> ValidateAsync(
        T target,
        ValidationContext context,
        CancellationToken cancellationToken = default);
}
