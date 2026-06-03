using Microsoft.Playwright;
using Xunit;

namespace TrainingApp.UiTests;

public class RegisterFlowTest
{
    [Fact]
    public async Task Should_Register_New_User_And_Show_Login()
    {
        using IPlaywright playwright = await Playwright.CreateAsync();

        await using IBrowser browser = await playwright.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 500
            });

        IPage page = await browser.NewPageAsync();

        await page.GotoAsync("http://localhost:3000/register");

        string uniqueEmail = $"testuser{DateTime.Now:yyyyMMddHHmmss}@demo.com";

        await page.GetByPlaceholder("John").FillAsync("John");
        await page.GetByPlaceholder("Doe").FillAsync("Doe");
        await page.GetByPlaceholder("Demo Inc.").FillAsync("Demo Company");
        await page.GetByPlaceholder("demo@company.com").FillAsync(uniqueEmail);
        await page.GetByPlaceholder("******").FillAsync("Password");

        await page.GetByRole(
            AriaRole.Button,
            new() { Name = "Create Account" }
        ).ClickAsync();

        ILocator loginHeading = page.GetByRole(
            AriaRole.Heading,
            new() { Name = "Login" }
        );

        await loginHeading.WaitForAsync();

        Assert.True(await loginHeading.IsVisibleAsync());
    }
}

