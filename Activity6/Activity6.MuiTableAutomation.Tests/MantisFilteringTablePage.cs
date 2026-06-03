using System.Text.RegularExpressions;
using Microsoft.Playwright;

namespace Activity6.MuiTableAutomation.Tests;

public class MantisFilteringTablePage
{
    private readonly IPage _page;

    public MantisFilteringTablePage(IPage page)
    {
        _page = page;
    }

    public async Task GotoAsync()
    {
        await _page.GotoAsync("https://mantisdashboard.com/tables/react-table/filtering");
    }

    public async Task ExpectPageLoadedAsync()
    {
        await Assertions.Expect(
            _page.Locator("h2")
        ).ToHaveTextAsync("Filtering");

        await Assertions.Expect(
            _page.GetByPlaceholder("Search records")
        ).ToBeVisibleAsync();

        await Assertions.Expect(
            _page.Locator("tbody tr").First
        ).ToBeVisibleAsync();
    }

    public async Task SearchAllRecordsAsync(string keyword)
    {
        await _page.GetByPlaceholder("Search records").FillAsync(keyword);
    }

    public async Task ClearGlobalSearchAsync()
    {
        await _page.GetByPlaceholder("Search records").ClearAsync();
    }

    public async Task FilterByNameAsync(string name)
    {
        await _page.GetByPlaceholder("Search Name").FillAsync(name);
    }

    public async Task ClearNameFilterAsync()
    {
        await _page.GetByPlaceholder("Search Name").ClearAsync();
    }

    public async Task FilterByEmailAsync(string email)
    {
        await _page.GetByPlaceholder("Search Email").FillAsync(email);
    }

    public async Task ClearEmailFilterAsync()
    {
        await _page.GetByPlaceholder("Search Email").ClearAsync();
    }

    public async Task FilterByRoleAsync(string role)
    {
        await _page.GetByPlaceholder("Search Role").FillAsync(role);
    }

    public async Task ClearRoleFilterAsync()
    {
        await _page.GetByPlaceholder("Search Role").ClearAsync();
    }

    public async Task FilterByStatusAsync(string status)
    {
        await _page.GetByRole(AriaRole.Combobox).ClickAsync();

        await _page.GetByRole(
            AriaRole.Option,
            new() { Name = status }
        ).ClickAsync();
    }

    public async Task ExpectRowContainsAsync(string text)
    {
        await Assertions.Expect(
            _page.Locator("tbody tr")
                .Filter(new()
                {
                    HasTextString = text
                })
                .First
        ).ToBeVisibleAsync();
    }

    public async Task ExpectTextHiddenAsync(string text)
    {
        await Assertions.Expect(
            _page.Locator("tbody tr")
                .Filter(new()
                {
                    HasTextString = text
                })
        ).ToHaveCountAsync(0);
    }

    public async Task GoToNextPageIfAvailableAsync()
    {
        ILocator nextButton = _page.GetByRole(
            AriaRole.Button,
            new()
            {
                NameRegex = new Regex("next", RegexOptions.IgnoreCase)
            }
        );

        if (await nextButton.CountAsync() > 0 && await nextButton.IsEnabledAsync())
        {
            await nextButton.ClickAsync();

            await Assertions.Expect(
                _page.Locator("tbody tr").First
            ).ToBeVisibleAsync();
        }
    }

    public async Task GoToPreviousPageIfAvailableAsync()
    {
        ILocator previousButton = _page.GetByRole(
            AriaRole.Button,
            new()
            {
                NameRegex = new Regex("previous", RegexOptions.IgnoreCase)
            }
        );

        if (await previousButton.CountAsync() > 0 && await previousButton.IsEnabledAsync())
        {
            await previousButton.ClickAsync();

            await Assertions.Expect(
                _page.Locator("tbody tr").First
            ).ToBeVisibleAsync();
        }
    }

    public async Task Should_Value_Within_Range()
    {
        var valueText = await _page
                .GetByRole(AriaRole.Textbox)
                .InputValueAsync();

        int value = int.Parse(valueText);

        Assert.InRange(value, 1, 100);
    }
}