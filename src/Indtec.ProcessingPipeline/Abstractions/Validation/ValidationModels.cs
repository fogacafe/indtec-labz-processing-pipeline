using Indtec.ProcessingPipeline.Abstractions.Processing;

namespace Indtec.ProcessingPipeline.Abstractions.Validation;

public enum ValidationSeverity
{
    Hint,
    Warning,
    Error
}

public sealed record ValidationMessage(string Field, string Message, ValidationSeverity Severity);

public sealed record ValidationContext(ProcessingIntent Intent);

public sealed class ValidationResult(IEnumerable<ValidationMessage> messages)
{
    public IReadOnlyCollection<ValidationMessage> Messages { get; } = messages.ToArray();
    public bool HasErrors => Messages.Any(x => x.Severity == ValidationSeverity.Error);
    public bool HasWarnings => Messages.Any(x => x.Severity == ValidationSeverity.Warning);
    public bool HasHints => Messages.Any(x => x.Severity == ValidationSeverity.Hint);
}
