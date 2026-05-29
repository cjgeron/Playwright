using Activity11.TestStructureCi.Tests.Utils;
using Microsoft.Playwright;

namespace Activity11.TestStructureCi.Tests.Fixtures;

public sealed class PlaywrightTestFixture : IAsyncLifetime
{
    private readonly BrowserFixture _browserFixture;

    public IBrowserContext Context { get; private set; } = null!;
    public IPage Page { get; private set; } = null!;

    public PlaywrightTestFixture(BrowserFixture browserFixture)
    {
        _browserFixture = browserFixture;
    }

    public async Task InitializeAsync()
    {
        Directory.CreateDirectory(TestSettings.ScreenshotsDirectory);
        Directory.CreateDirectory(TestSettings.VideosDirectory);
        Directory.CreateDirectory(TestSettings.TracesDirectory);

        Context = await _browserFixture.Browser.NewContextAsync(
            new BrowserNewContextOptions
            {
                RecordVideoDir = TestSettings.VideosDirectory,
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
        await Context.CloseAsync();
    }
}