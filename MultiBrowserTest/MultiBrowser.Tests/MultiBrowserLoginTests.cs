using Microsoft.Playwright;
using Xunit;

namespace Activity12.MultiBrowser.Tests;

public sealed class MultiBrowserLoginTests
{
    [Theory]
    [InlineData("chromium")]
    [InlineData("firefox")]
    [InlineData("webkit")]
    public async Task Should_Login_Using_Multiple_Browsers(string browserName)
    {
        using var playwright = await Playwright.CreateAsync();

        await using var browser = await LaunchBrowserAsync(playwright, browserName);

        var context = await browser.NewContextAsync();

        var page = await context.NewPageAsync();

        await page.GotoAsync("https://mantisdashboard.com/login");

        await page.GetByPlaceholder("Enter email address")
            .FillAsync("info@codedthemes.com");

        await page.GetByPlaceholder("Enter password")
            .FillAsync("123456");

        await page.GetByRole(AriaRole.Button, new() { Name = "Login" })
            .ClickAsync();

        await Assertions.Expect(
            page.GetByRole(AriaRole.Heading, new() { Name = "Welcome to" })
        ).ToBeVisibleAsync();

        await context.CloseAsync();
    }

    private static Task<IBrowser> LaunchBrowserAsync(
        IPlaywright playwright,
        string browserName)
    {
        var options = new BrowserTypeLaunchOptions
        {
            Headless = false,
            SlowMo = 200
        };

        return browserName switch
        {
            "chromium" => playwright.Chromium.LaunchAsync(options),
            "firefox" => playwright.Firefox.LaunchAsync(options),
            "webkit" => playwright.Webkit.LaunchAsync(options),
            _ => throw new ArgumentException($"Unsupported browser: {browserName}")
        };
    }
}