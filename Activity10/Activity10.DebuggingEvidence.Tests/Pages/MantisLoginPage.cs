using Microsoft.Playwright;

namespace Activity10.DebuggingEvidence.Tests.Pages;

public sealed class MantisLoginPage
{
    private readonly IPage _page;

    public MantisLoginPage(IPage page)
    {
        _page = page;
    }

    public async Task GotoAsync()
    {
        await _page.GotoAsync(
            "http://localhost:3000",
            new PageGotoOptions
            {
                WaitUntil = WaitUntilState.DOMContentLoaded
            });
        
        await _page.GetByText("Already have an account?").ClickAsync();
        
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

    public async Task AssertDashboardLoadedAsync()
    {
        await Assertions.Expect(
            _page.GetByRole(AriaRole.Heading, new() { Name = "Dashboard" })
        ).ToBeVisibleAsync();
    }
}