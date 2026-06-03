using Microsoft.Playwright;

namespace Activity9.RealisticE2EWorkflowAutomation.Tests.Pages.Components;

public sealed class HeaderMenuComponent
{
    private readonly IPage _page;

    public HeaderMenuComponent(IPage page)
    {
        _page = page;
    }

    public async Task LogoutAsync()
    {
        await _page
            .Locator("header")
            .GetByRole(AriaRole.Button)
            .Last
            .ClickAsync();

        await _page.GetByText("Logout").ClickAsync();

        await _page.WaitForURLAsync("**/login",
            new PageWaitForURLOptions
            {
                Timeout = 15000
            });
    }
}