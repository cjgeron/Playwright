using Activity9.RealisticE2EWorkflowAutomation.Tests.Models;
using Activity9.RealisticE2EWorkflowAutomation.Tests.Pages.Components;
using Microsoft.Playwright;

namespace Activity9.RealisticE2EWorkflowAutomation.Tests.Pages.Authentication;

public sealed class MantisLoginPage
{
    private readonly IPage _page;
    private readonly PromoPopupComponent _promoPopup;

    public MantisLoginPage(IPage page)
    {
        _page = page;
        _promoPopup = new PromoPopupComponent(page);
    }

    public async Task GotoAsync()
    {
        await _page.GotoAsync("https://mantisdashboard.com/login",
            new PageGotoOptions
            {
                WaitUntil = WaitUntilState.DOMContentLoaded
            });

        await Assertions.Expect(
            _page.GetByRole(AriaRole.Heading, new() { Name = "Login" })
        ).ToBeVisibleAsync();
    }

    public async Task ClosePromoPopupAsync()
    {
        await _promoPopup.CloseAsync();
    }

    public async Task LoginAsync(LoginUserModel user)
    {
        await _page
            .GetByPlaceholder("Enter email address")
            .FillAsync(user.Email);

        await _page
            .GetByPlaceholder("Enter password")
            .FillAsync(user.Password);

        await _page
            .GetByRole(
                AriaRole.Button,
                new() { Name = "Login" })
            .ClickAsync();

        await _page.WaitForURLAsync(
            "**/dashboard/**",
            new PageWaitForURLOptions
            {
                Timeout = 15000,
                WaitUntil = WaitUntilState.DOMContentLoaded
            });

        await Assertions.Expect(
            _page.GetByRole(
                AriaRole.Heading,
                new() { Name = "Welcome to Mantis" })
        ).ToBeVisibleAsync();
    }

    public async Task VerifyLoginPageAsync()
    {
        await Assertions.Expect(
            _page.GetByRole(AriaRole.Heading, new() { Name = "Login" })
        ).ToBeVisibleAsync();

        await Assertions.Expect(
            _page.GetByRole(AriaRole.Button, new() { Name = "Login" })
        ).ToBeVisibleAsync();
    }
}