using Indtec.ProcessingPipeline.Abstractions.Processing;
using Indtec.ProcessingPipeline.Abstractions.Validation;

namespace Indtec.ProcessingPipeline.Tests;

public sealed class ValidationPolicyTests
{
    private readonly ValidationPolicy _policy = new();

    [Fact]
    public void Warning_allows_save()
    {
        var validation = Result(ValidationSeverity.Warning);
        Assert.True(_policy.CanProceed(ProcessingIntent.Save, validation));
    }

    [Theory]
    [InlineData(ProcessingIntent.Release)]
    [InlineData(ProcessingIntent.SaveAndRelease)]
    public void Warning_blocks_operations_that_require_release(ProcessingIntent intent)
    {
        var validation = Result(ValidationSeverity.Warning);
        Assert.False(_policy.CanProceed(intent, validation));
    }

    [Theory]
    [InlineData(ProcessingIntent.Save)]
    [InlineData(ProcessingIntent.Release)]
    [InlineData(ProcessingIntent.SaveAndRelease)]
    public void Error_blocks_every_intent(ProcessingIntent intent)
    {
        var validation = Result(ValidationSeverity.Error);
        Assert.False(_policy.CanProceed(intent, validation));
    }

    [Theory]
    [InlineData(ProcessingIntent.Save)]
    [InlineData(ProcessingIntent.Release)]
    [InlineData(ProcessingIntent.SaveAndRelease)]
    public void Hint_never_blocks(ProcessingIntent intent)
    {
        var validation = Result(ValidationSeverity.Hint);
        Assert.True(_policy.CanProceed(intent, validation));
    }

    private static ValidationResult Result(ValidationSeverity severity) =>
        new([new ValidationMessage("field", "message", severity)]);
}
