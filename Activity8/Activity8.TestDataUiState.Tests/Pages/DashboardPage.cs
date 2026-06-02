using Microsoft.Playwright;

namespace Activity8.TestDataUiState.Tests.Pages;

public class DashboardPage
{
    private readonly IPage _page;

    public DashboardPage(IPage page)
    {
        _page = page;
    }

    public ILocator DashboardContainer =>
        _page.GetByRole(
            AriaRole.Heading,
            new() { Name = "Total Order" }
        );

    public ILocator ProfileButton =>
        _page.GetByRole(
            AriaRole.Button,
            new() { Name = "Profile" }
        );

    public async Task LogoutAsync()
    {
        await ProfileButton.ClickAsync();

        await _page.GetByRole(
            AriaRole.Menuitem,
            new() { Name = "Logout" }
        ).ClickAsync();
    }
}