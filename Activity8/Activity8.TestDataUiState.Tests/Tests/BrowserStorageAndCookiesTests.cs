using Activity8.TestDataUiState.Tests.Fixtures;
using Microsoft.Playwright;

namespace Activity8.TestDataUiState.Tests.Tests;

public class BrowserStorageAndCookiesTests : IClassFixture<PlaywrightFixture>
{
    private readonly PlaywrightFixture _fixture;

    public BrowserStorageAndCookiesTests(PlaywrightFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Should_Read_And_Validate_Browser_Storage_And_Cookies()
    {
        await using IBrowserContext context = await _fixture.Browser.NewContextAsync();
        IPage page = await context.NewPageAsync();

        await page.GotoAsync($"{PlaywrightFixture.BaseUrl}/login");

        await page.EvaluateAsync("""
            localStorage.setItem('training_user_role', 'admin');
            sessionStorage.setItem('training_session_status', 'active');
        """);

        await context.AddCookiesAsync(new[]
        {
            new Cookie
            {
                Name = "training_cookie",
                Value = "module8",
                Domain = "localhost",
                Path = "/"
            }
        });

        string? localStorageValue = await page.EvaluateAsync<string>(
            "localStorage.getItem('training_user_role')"
        );

        string? sessionStorageValue = await page.EvaluateAsync<string>(
            "sessionStorage.getItem('training_session_status')"
        );

        IReadOnlyList<BrowserContextCookiesResult> cookies = await context.CookiesAsync();

        Assert.Equal("admin", localStorageValue);
        Assert.Equal("active", sessionStorageValue);

        Assert.Contains(cookies, cookie =>
            cookie.Name == "training_cookie" &&
            cookie.Value == "module8");
    }
}