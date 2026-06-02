using Microsoft.Playwright;

namespace Activity7.MantisPomAutomation.Tests.Pages;

public class LoginPage
{
    private readonly IPage _page;

    public LoginPage(IPage page)
    {
        _page = page;
    }

    public async Task OpenAsync()
    {
        await _page.GotoAsync("http://localhost:3000/login");
    }

    public async Task LoginAsync(string email, string password)
    {
        await _page.GetByPlaceholder("Enter email address")
            .FillAsync(email);

        await _page.GetByPlaceholder("Enter password")
            .FillAsync(password);

        await _page.GetByRole(
            AriaRole.Button,
            new() { Name = "Login" }
        ).ClickAsync();
    }

    public async Task AcceptLoginPopupAsync()
    {
        _page.Dialog += async (_, dialog) =>
        {
            await dialog.AcceptAsync();
        };
    }
}