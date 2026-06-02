using Microsoft.Playwright;

namespace Activity8.TestDataUiState.Tests.Pages;

public class LoginPage
{
    private readonly IPage _page;

    public LoginPage(IPage page)
    {
        _page = page;
    }

    public async Task GoToAsync(string baseUrl)
    {
        await _page.GotoAsync($"{baseUrl}/login");
    }

    public async Task LoginAsync(string email, string password)
    {
        await _page.GetByPlaceholder("Enter email address").FillAsync(email);
        await _page.GetByPlaceholder("Enter password").FillAsync(password);

        await _page.GetByRole(
            AriaRole.Button,
            new() { Name = "Login" }
        ).ClickAsync();
    }

    public ILocator EmailRequiredMessage =>
        _page.GetByText("Email is required");

    public ILocator PasswordRequiredMessage =>
        _page.GetByText("Password is required");
}