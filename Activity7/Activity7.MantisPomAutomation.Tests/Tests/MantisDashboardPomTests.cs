using Activity7.MantisPomAutomation.Tests.Fixtures;
using Activity7.MantisPomAutomation.Tests.Pages;
using Microsoft.Playwright;

namespace Activity7.MantisPomAutomation.Tests.Tests;

public class MantisDashboardPomTests : IClassFixture<PlaywrightFixture>
{
    private readonly PlaywrightFixture _fixture;

    public MantisDashboardPomTests(PlaywrightFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Should_Login_Test_Dashboard_Chart_Profile_And_Logout_Using_Pom()
    {
        IPage page = await _fixture.Browser.NewPageAsync();

        LoginPage loginPage = new(page);
        DashboardPage dashboardPage = new(page);

        await loginPage.OpenAsync();

        await loginPage.AcceptLoginPopupAsync();

        await loginPage.LoginAsync(
            email: "a@a.com",
            password: "password"
        );

        await dashboardPage.AssertDashboardLoadedAsync();

        await dashboardPage.ClickDashboardAsync();

        await dashboardPage.ClickWeekTabAsync();

        await dashboardPage.ClickTuesdayOnChartAsync();

        await dashboardPage.AssertTuesdayTooltipAsync();

        await dashboardPage.OpenProfileMenuAsync();

        await dashboardPage.LogoutAsync();

        await dashboardPage.AssertReturnedToLoginAsync();

        await page.CloseAsync();
    }
}