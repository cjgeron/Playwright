using Activity11.TestStructureCi.Tests.Utils;
using Microsoft.Playwright;

namespace Activity11.TestStructureCi.Tests.Pages;

public sealed class LoginPage
{
    private readonly IPage _page;

    public LoginPage(IPage page)
    {
        _page = page;
    }

    public async Task GotoAsync()
    {
        await _page.GotoAsync(
            TestSettings.BaseUrl,
            new PageGotoOptions
            {
                WaitUntil = WaitUntilState.DOMContentLoaded
            });

        await Assertions.Expect(
            _page.GetByRole(AriaRole.Button, new() { Name = "Login" })
        ).ToBeVisibleAsync();
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
}