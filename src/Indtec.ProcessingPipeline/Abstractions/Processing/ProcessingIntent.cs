namespace Indtec.ProcessingPipeline.Abstractions.Processing;

public enum ProcessingIntent
{
    Save,
    Release,
    SaveAndRelease
}

public static class ProcessingIntentExtensions
{
    public static bool RequiresSave(this ProcessingIntent intent) =>
        intent is ProcessingIntent.Save or ProcessingIntent.SaveAndRelease;

    public static bool RequiresRelease(this ProcessingIntent intent) =>
        intent is ProcessingIntent.Release or ProcessingIntent.SaveAndRelease;
}
