using Activity11.TestStructureCi.Tests.Fixtures;
using Activity11.TestStructureCi.Tests.Pages;
using Activity11.TestStructureCi.Tests.Utils;
using Xunit;

namespace Activity11.TestStructureCi.Tests.Tests;

public sealed class LoginBestPracticeTests : IClassFixture<BrowserFixture>, IAsyncLifetime
{
    private readonly BrowserFixture _browserFixture;
    private PlaywrightTestFixture _testFixture = null!;

    public LoginBestPracticeTests(BrowserFixture browserFixture)
    {
        _browserFixture = browserFixture;
    }

    public async Task InitializeAsync()
    {
        _testFixture = new PlaywrightTestFixture(_browserFixture);
        await _testFixture.InitializeAsync();
    }

    public async Task DisposeAsync()
    {
        await _testFixture.DisposeAsync();
    }

    [Fact]
    public async Task Should_Login_And_Display_Dashboard()
    {
        await ArtifactHelper.ExecuteWithArtifactsAsync(
            nameof(Should_Login_And_Display_Dashboard),
            _testFixture.Page,
            _testFixture.Context,
            async () =>
            {
                var loginPage = new LoginPage(_testFixture.Page);
                var dashboardPage = new DashboardPage(_testFixture.Page);

                await loginPage.GotoAsync();
                await loginPage.LoginAsync(
                    TestSettings.ValidEmail,
                    TestSettings.ValidPassword);

                await dashboardPage.AssertLoadedAsync();
            });
    }
}