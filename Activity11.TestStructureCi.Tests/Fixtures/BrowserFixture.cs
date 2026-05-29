using Microsoft.Playwright;

namespace Activity11.TestStructureCi.Tests.Fixtures;

public sealed class BrowserFixture : IAsyncLifetime
{
    private IPlaywright? _playwright;

    public IBrowser Browser { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        _playwright = await Playwright.CreateAsync();

        Browser = await _playwright.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions
            {
                Headless = true
            });
    }

    public async Task DisposeAsync()
    {
        await Browser.CloseAsync();
        _playwright?.Dispose();
    }
}