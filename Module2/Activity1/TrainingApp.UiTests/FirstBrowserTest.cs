using Microsoft.Playwright;
using Xunit;

namespace TrainingApp.UiTests;

public class FirstBrowserTest
{
    [Fact]
    public async Task Should_Open_Training_App_Register_Page()
    {
        using IPlaywright playwright = await Playwright.CreateAsync();

        await using IBrowser browser = await playwright.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 500
            });

        IPage page = await browser.NewPageAsync();

        await page.GotoAsync("http://localhost:3000");

        await ExpectPageToContainText(page, "Create Account");
    }

    private static async Task ExpectPageToContainText(IPage page, string text)
    {
        var locator = page.GetByText(text);
        await locator.WaitForAsync();
        Assert.True(await locator.IsVisibleAsync());
    }
}