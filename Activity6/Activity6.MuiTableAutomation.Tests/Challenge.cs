using System.Text.RegularExpressions;
using Microsoft.Playwright;

namespace Activity6.MuiTableAutomation.Tests;


public class Challenge : IClassFixture<PlaywrightFixture>
{
    private readonly PlaywrightFixture _fixture;

    public Challenge(PlaywrightFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task ClosePromoPopupAsync()
    {
        IPage page = await _fixture.Browser.NewPageAsync();

        MantisLoginPage loginPage = new(page);

        await loginPage.GotoAsync();

        await page
        .Locator("div")
        .Filter(new() { HasText = "Build faster with ready-to-use prompts" })
        .Locator("svg, button, span")
        .Last
        .ClickAsync();
    }
}
