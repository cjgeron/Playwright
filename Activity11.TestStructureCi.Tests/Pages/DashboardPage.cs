using Microsoft.Playwright;

namespace Activity11.TestStructureCi.Tests.Pages;

public sealed class DashboardPage
{
    private readonly IPage _page;

    public DashboardPage(IPage page)
    {
        _page = page;
    }

    public async Task AssertLoadedAsync()
    {
        await Assertions.Expect(
            _page.GetByRole(AriaRole.Heading, new() { Name = "Welcome to Mantis" })
        ).ToBeVisibleAsync();
    }
}