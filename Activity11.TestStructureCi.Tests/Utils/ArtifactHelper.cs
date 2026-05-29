using Microsoft.Playwright;

namespace Activity11.TestStructureCi.Tests.Utils;

public static class ArtifactHelper
{
    public static async Task ExecuteWithArtifactsAsync(
        string testName,
        IPage page,
        IBrowserContext context,
        Func<Task> testAction)
    {
        try
        {
            await testAction();

            await context.Tracing.StopAsync(
                new TracingStopOptions
                {
                    Path = $"{TestSettings.TracesDirectory}/{testName}-passed.zip"
                });
        }
        catch
        {
            await page.ScreenshotAsync(
                new PageScreenshotOptions
                {
                    Path = $"{TestSettings.ScreenshotsDirectory}/{testName}-failed.png",
                    FullPage = true
                });

            await context.Tracing.StopAsync(
                new TracingStopOptions
                {
                    Path = $"{TestSettings.TracesDirectory}/{testName}-failed.zip"
                });

            throw;
        }
    }
}