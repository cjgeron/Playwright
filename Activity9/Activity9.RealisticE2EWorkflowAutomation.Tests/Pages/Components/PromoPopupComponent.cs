using Microsoft.Playwright;

namespace Activity9.RealisticE2EWorkflowAutomation.Tests.Pages.Components;

public sealed class PromoPopupComponent
{
    private readonly IPage _page;

    public PromoPopupComponent(IPage page)
    {
        _page = page;
    }

    public async Task CloseAsync()
    {
        ILocator promoPopup = _page
            .Locator("div")
            .Filter(new() { HasText = "Build faster with ready-to-use prompts" })
            .First;

        try
        {
            await promoPopup.WaitForAsync(
                new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 3000
                });

            await promoPopup
                .Locator("svg, button, span")
                .Last
                .ClickAsync();

            await Assertions.Expect(promoPopup).ToBeHiddenAsync();
        }
        catch (TimeoutException)
        {
            // Popup did not appear
        }
    }
}