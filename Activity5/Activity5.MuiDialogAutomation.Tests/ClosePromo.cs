using Microsoft.Playwright;
namespace Activity5.MuiDialogAutomation.Tests;


public class ClosePromo
{
    private const string DialogsUrl =
        "https://mantisdashboard.com/components-overview/dialogs";

    [Fact]
    public async Task ClosePromoPopupAsync()
    {
        using IPlaywright playwright = await Playwright.CreateAsync();

        await using IBrowser browser = await playwright.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 300
            });

        IPage page = await browser.NewPageAsync();

        await page.GotoAsync(DialogsUrl);

        await page
        .Locator("div")
        .Filter(new() { HasText = "Build faster with ready-to-use prompts" })
        .Locator("svg, button, span")
        .Last
        .ClickAsync();
    }
}
