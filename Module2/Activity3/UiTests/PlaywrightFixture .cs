using Microsoft.Playwright;

namespace Activity3.UiTests;

public class PlaywrightFixture : IAsyncLifetime
{
    public IPlaywright Pw { get; private set; } = null!;
    public IBrowser Browser { get; private set; } = null!;

    public string BaseUrl { get; } = "http://localhost:3000";

    public async Task InitializeAsync()
    {
        Pw = await Playwright.CreateAsync();

        Browser = await Pw.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 500
            });
    }

    public async Task DisposeAsync()
    {
        await Browser.CloseAsync();
        Pw.Dispose();
    }
}