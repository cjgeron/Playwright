using Microsoft.Playwright;
using TrainingApp.UiTests.Fixtures;
using static Microsoft.Playwright.Assertions;

namespace TrainingApp.UiTests;

public class DashboardTests : IClassFixture<PlaywrightFixture>
{
    private readonly PlaywrightFixture _fixture;

    public DashboardTests(PlaywrightFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Should_Login_Handle_Popup_And_Check_Dashboard_Statistics()
    {
        await using IBrowserContext context = await _fixture.Browser.NewContextAsync();
        IPage page = await context.NewPageAsync();

        await page.GotoAsync("http://localhost:3000/login");

        await page.GetByPlaceholder("Enter email address").FillAsync("a@a.com");
        await page.GetByPlaceholder("Enter password").FillAsync("password");

        string? dialogMessage = null;

        page.Dialog += async (_, dialog) =>
        {
            dialogMessage = dialog.Message;
            await dialog.AcceptAsync();
        };

        await page.GetByRole(
            AriaRole.Button,
            new() { Name = "Login" }
        ).ClickAsync();

        Assert.Equal("Login successful", dialogMessage);

        await Expect(page.GetByRole(
            AriaRole.Heading,
            new() { Name = "Dashboard" }
        )).ToBeVisibleAsync();

        await Expect(
            page.GetByRole(AriaRole.Heading, new() { Name = "Total Page Views" })
        ).ToBeVisibleAsync();

        await Expect(page.GetByText("4,42,236")).ToBeVisibleAsync();

        await Expect(
            page.GetByRole(AriaRole.Heading, new() { Name = "Total Users" })
        ).ToBeVisibleAsync();

        await Expect(page.GetByText("78,250")).ToBeVisibleAsync();

        await Expect(
            page.GetByRole(AriaRole.Heading, new() { Name = "Total Order" })
        ).ToBeVisibleAsync();

        await Expect(page.GetByText("18,800")).ToBeVisibleAsync();

        await Expect(
            page.GetByRole(AriaRole.Heading, new() { Name = "Total Sales" })
        ).ToBeVisibleAsync();

        await Expect(page.GetByText("35,078")).ToBeVisibleAsync();
    }
}



