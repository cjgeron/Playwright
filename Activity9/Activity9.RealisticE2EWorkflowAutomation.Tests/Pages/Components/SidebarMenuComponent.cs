using Microsoft.Playwright;

namespace Activity9.RealisticE2EWorkflowAutomation.Tests.Pages.Components;

public sealed class SidebarMenuComponent
{
    private readonly IPage _page;

    public SidebarMenuComponent(IPage page)
    {
        _page = page;
    }

    public async Task OpenUserProfileAsync()
    {
        await _page
            .Locator("nav")
            .GetByText("Profile", new() { Exact = true })
            .ClickAsync();

        await _page
            .Locator("nav")
            .GetByText("User Profile", new() { Exact = true })
            .ClickAsync();

        await Assertions.Expect(
            _page
                .Locator(".MuiCardHeader-root")
                .GetByText("Personal Information", new() { Exact = true })
        ).ToBeVisibleAsync();
    }

    public async Task OpenFormsWizardAsync()
    {
        await _page
            .Locator("nav")
            .GetByText("Forms Wizard", new() { Exact = true })
            .ClickAsync();

        await Assertions.Expect(
            _page.GetByRole(
                AriaRole.Heading,
                new() { Name = "Forms Wizard" })
        ).ToBeVisibleAsync();
    }
}