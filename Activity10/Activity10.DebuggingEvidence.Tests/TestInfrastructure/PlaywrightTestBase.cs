using Microsoft.Playwright;

namespace Activity10.DebuggingEvidence.Tests.TestInfrastructure;

public abstract class PlaywrightTestBase : IAsyncLifetime
{
    protected IPlaywright Playwright = null!;
    protected IBrowser Browser = null!;
    protected IBrowserContext Context = null!;
    protected IPage Page = null!;

    protected virtual bool Headless => true;

    public async Task InitializeAsync()
    {
        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        Browser = await Playwright.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions
            {
                Headless = Headless,
                SlowMo = Headless ? 0 : 300
            });

        Context = await Browser.NewContextAsync(
            new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize
                {
                    Width = 1366,
                    Height = 768
                },
                RecordVideoDir = "videos/",
                RecordVideoSize = new RecordVideoSize
                {
                    Width = 1280,
                    Height = 720
                }
            });

        await Context.Tracing.StartAsync(
            new TracingStartOptions
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });

        Page = await Context.NewPageAsync();
    }

    public async Task DisposeAsync()
    {
        Directory.CreateDirectory("traces");

        await Context.Tracing.StopAsync(
            new TracingStopOptions
            {
                Path = $"traces/trace-{DateTime.Now:yyyyMMdd-HHmmss}.zip"
            });

        await Context.CloseAsync();
        await Browser.CloseAsync();

        Playwright.Dispose();
    }

    protected async Task CaptureScreenshotAsync(string testName)
    {
        Directory.CreateDirectory("screenshots");

        await Page.ScreenshotAsync(
            new PageScreenshotOptions
            {
                Path = $"screenshots/{testName}-{DateTime.Now:yyyyMMdd-HHmmss}.png",
                FullPage = true
            });
    }
}