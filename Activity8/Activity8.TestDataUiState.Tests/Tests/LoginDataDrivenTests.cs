using Activity8.TestDataUiState.Tests.Fixtures;
using Activity8.TestDataUiState.Tests.Models;
using Activity8.TestDataUiState.Tests.Pages;
using Activity8.TestDataUiState.Tests.TestData;
using Microsoft.Playwright;

namespace Activity8.TestDataUiState.Tests.Tests;

public class LoginDataDrivenTests : IClassFixture<PlaywrightFixture>
{
    private readonly PlaywrightFixture _fixture;

    public LoginDataDrivenTests(PlaywrightFixture fixture)
    {
        _fixture = fixture;
    }

    [Theory]
    [MemberData(nameof(LoginData.LoginScenarios), MemberType = typeof(LoginData))]
    public async Task Should_Test_Login_Using_Positive_And_Negative_Test_Data(LoginTestData data)
    {
        await using IBrowserContext context = await _fixture.Browser.NewContextAsync();
        IPage page = await context.NewPageAsync();

        LoginPage loginPage = new(page);
        DashboardPage dashboardPage = new(page);

        await loginPage.GoToAsync(PlaywrightFixture.BaseUrl);

        string? dialogMessage = null;

        page.Dialog += async (_, dialog) =>
        {
            dialogMessage = dialog.Message;
            await dialog.AcceptAsync();
        };

        await loginPage.LoginAsync(data.Email, data.Password);

        if (data.ShouldLoginSuccessfully)
        {
            await Assertions.Expect(dashboardPage.DashboardContainer).ToBeVisibleAsync();
        }
        else if (string.IsNullOrWhiteSpace(data.Email) &&
                 string.IsNullOrWhiteSpace(data.Password))
        {
            await Assertions.Expect(loginPage.EmailRequiredMessage).ToBeVisibleAsync();
            await Assertions.Expect(loginPage.PasswordRequiredMessage).ToBeVisibleAsync();
        }
        else
        {
            await Assertions.Expect(page.GetByRole(AriaRole.Button, new() { Name = "Login" }))
                .ToBeVisibleAsync();

            Assert.Equal("Invalid email or password", dialogMessage);
        }
    }
}