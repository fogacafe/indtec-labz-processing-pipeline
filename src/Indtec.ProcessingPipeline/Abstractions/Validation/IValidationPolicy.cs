using Indtec.ProcessingPipeline.Abstractions.Processing;

namespace Indtec.ProcessingPipeline.Abstractions.Validation;

public interface IValidationPolicy
{
    bool CanProceed(ProcessingIntent intent, ValidationResult validation);
}

public sealed class ValidationPolicy : IValidationPolicy
{
    public bool CanProceed(ProcessingIntent intent, ValidationResult validation)
    {
        if (validation.HasErrors)
            return false;

        if (intent.RequiresRelease() && validation.HasWarnings)
            return false;

        return true;
    }
}
