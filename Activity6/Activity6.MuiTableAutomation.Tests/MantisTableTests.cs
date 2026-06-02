using Microsoft.Playwright;
using Xunit;

namespace Activity6.MuiTableAutomation.Tests;

public class MantisTableTests : IClassFixture<PlaywrightFixture>
{
    private readonly PlaywrightFixture _fixture;

    public MantisTableTests(PlaywrightFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Should_Test_Mui_Table_Search_Filter_And_Pagination()
    {
        IPage page = await _fixture.Browser.NewPageAsync();

        try
        {
            MantisLoginPage loginPage = new(page);
            MantisFilteringTablePage tablePage = new(page);

            await loginPage.GotoAsync();
            await loginPage.ClickLoginAsync();

            await tablePage.GotoAsync();
            await tablePage.ExpectPageLoadedAsync();

            ILocator firstRow = page.Locator("tbody tr").First;
            ILocator firstRowCells = firstRow.Locator("td");

            string name = await firstRowCells.Nth(0).InnerTextAsync();
            string email = await firstRowCells.Nth(1).InnerTextAsync();
            string role = await firstRowCells.Nth(2).InnerTextAsync();
            string status = await firstRowCells.Nth(5).InnerTextAsync();

            await tablePage.SearchAllRecordsAsync(name);
            await tablePage.ExpectRowContainsAsync(name);
            await tablePage.ClearGlobalSearchAsync();

            await tablePage.FilterByNameAsync(name);
            await tablePage.ExpectRowContainsAsync(name);
            await tablePage.ClearNameFilterAsync();

            await tablePage.FilterByEmailAsync(email);
            await tablePage.ExpectRowContainsAsync(email);
            await tablePage.ClearEmailFilterAsync();

            await tablePage.FilterByRoleAsync(role);
            await tablePage.ExpectRowContainsAsync(role);
            await tablePage.ClearRoleFilterAsync();

            await tablePage.FilterByStatusAsync(status);
            await tablePage.ExpectRowContainsAsync(status);

            await tablePage.FilterByStatusAsync("All");

            await tablePage.GoToNextPageIfAvailableAsync();
            await tablePage.GoToPreviousPageIfAvailableAsync();
        }
        finally
        {
            await page.CloseAsync();
        }
    }
}