using Microsoft.Playwright;

namespace Activity6.MuiTableAutomation.Tests;

public class MantisLoginPage
{
    private readonly IPage _page;

    public MantisLoginPage(IPage page)
    {
        _page = page;
    }

    public async Task GotoAsync()
    {
        await _page.GotoAsync("https://mantisdashboard.com/login");
    }

    public async Task ClickLoginAsync()
    {
        await _page.GetByRole(
            AriaRole.Button,
            new() { Name = "Login" }
        ).ClickAsync();
    }
}