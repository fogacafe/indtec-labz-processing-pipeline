namespace Indtec.ProcessingPipeline.Abstractions.Validation;

public sealed class ValidationPipeline<T>(IEnumerable<IValidationRule<T>> rules) : IValidator<T>
{
    public async Task<ValidationResult> ValidateAsync(
        T target,
        ValidationContext context,
        CancellationToken cancellationToken = default)
    {
        var messages = new List<ValidationMessage>();

        foreach (var rule in rules)
        {
            if (!rule.IsApplicable(target, context))
                continue;

            messages.AddRange(await rule.ValidateAsync(target, context, cancellationToken));
        }

        return new ValidationResult(messages);
    }
}
