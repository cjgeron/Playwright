using Activity8.TestDataUiState.Tests.Fixtures;
using Activity8.TestDataUiState.Tests.Pages;
using Microsoft.Playwright;

namespace Activity8.TestDataUiState.Tests.Tests;

public class AuthenticationStateReuseTests : IClassFixture<PlaywrightFixture>
{
    private readonly PlaywrightFixture _fixture;

    private const string AuthStatePath = "auth-state.json";

    public AuthenticationStateReuseTests(PlaywrightFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Step_1_Should_Login_And_Save_Authentication_State()
    {
        if (File.Exists(AuthStatePath))
        {
            File.Delete(AuthStatePath);
        }

        await using IBrowserContext context =
            await _fixture.Browser.NewContextAsync();

        IPage page = await context.NewPageAsync();

        LoginPage loginPage = new(page);
        DashboardPage dashboardPage = new(page);

        await loginPage.GoToAsync(PlaywrightFixture.BaseUrl);

        await loginPage.LoginAsync(
            "a@a.com",
            "password"
        );

        await page.WaitForURLAsync("**/dashboard/default");

        await Assertions.Expect(dashboardPage.DashboardContainer)
            .ToBeVisibleAsync();

        await context.StorageStateAsync(new()
        {
            Path = AuthStatePath
        });

        Assert.True(File.Exists(AuthStatePath));
    }

    [Fact]
    public async Task Step_2_Should_Reuse_Authentication_State_Without_Login()
    {
        Assert.True(
            File.Exists(AuthStatePath),
            "Run Step_1 first to generate auth-state.json."
        );

        await using IBrowserContext context =
            await _fixture.Browser.NewContextAsync(new()
            {
                StorageStatePath = AuthStatePath
            });

        IPage page = await context.NewPageAsync();

        await page.GotoAsync(
            $"{PlaywrightFixture.BaseUrl}/dashboard/default"
        );

        DashboardPage dashboardPage = new(page);

        await Assertions.Expect(dashboardPage.DashboardContainer)
            .ToBeVisibleAsync();
    }
}