using Microsoft.Playwright;

namespace Activity7.MantisPomAutomation.Tests.Pages;

public class DashboardPage
{
    private readonly IPage _page;

    public DashboardPage(IPage page)
    {
        _page = page;
    }

    public async Task AssertDashboardLoadedAsync()
    {
        await Assertions.Expect(
            _page.GetByRole(AriaRole.Heading, new() { Name = "Unique Visitor" })
        ).ToBeVisibleAsync();

        await Assertions.Expect(
            _page.GetByText("Income Overview")
        ).ToBeVisibleAsync();
    }

    public async Task ClickDashboardAsync()
    {
        await _page.GetByRole(
            AriaRole.Link,
            new() { Name = "Dashboard" }
        ).ClickAsync();
    }

    public async Task ClickWeekTabAsync()
    {
        await _page.GetByRole(
            AriaRole.Button,
            new() { Name = "Week" }
        ).ClickAsync();
    }

    public async Task ClickTuesdayOnChartAsync()
    {
        var tuesdayLabel = _page
            .Locator("svg")
            .GetByText("Tue")
            .First;

        await Assertions.Expect(tuesdayLabel)
            .ToBeVisibleAsync();

        await tuesdayLabel.ClickAsync();
    }
    public async Task AssertTuesdayTooltipAsync()
    {
        await Assertions.Expect(
            _page.GetByText("Page views", new() { Exact = true })
        ).ToBeVisibleAsync();

        await Assertions.Expect(
            _page.GetByText("Sessions", new() { Exact = true })
        ).ToBeVisibleAsync();
    }

public async Task OpenProfileMenuAsync()
{
    var profileButton = _page
        .Locator("header")
        .GetByRole(AriaRole.Button, new() { Name = "open profile" })
        .Filter(new()
        {
            Has = _page.Locator("img[alt='profile user']")
        });

    await profileButton.ClickAsync();

    await Assertions.Expect(
        _page.GetByText("John Doe", new() { Exact = true })
    ).ToBeVisibleAsync();
}
public async Task LogoutAsync()
{
    var logoutButton = _page.GetByRole(
        AriaRole.Button,
        new()
        {
            Name = "Logout",
            Exact = true
        });

    await logoutButton.ClickAsync();
}

    public async Task AssertReturnedToLoginAsync()
    {
        await Assertions.Expect(
            _page.GetByRole(AriaRole.Heading, new() { Name = "Login" })
        ).ToBeVisibleAsync();
    }
}