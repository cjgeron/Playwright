using Microsoft.Playwright;
using System.Text.RegularExpressions;
using Xunit;
using static Microsoft.Playwright.Assertions;

namespace Activity5.MuiDialogAutomation.Tests;

public class MantisDialogTests
{
    private const string DialogsUrl =
        "https://mantisdashboard.com/components-overview/dialogs";

    [Fact]
    public async Task Should_Automate_Common_Mui_Dialogs()
    {
        using IPlaywright playwright = await Playwright.CreateAsync();

        await using IBrowser browser = await playwright.Chromium.LaunchAsync(
            new BrowserTypeLaunchOptions
            {
                Headless = false,
                SlowMo = 300
            });

        IPage page = await browser.NewPageAsync();

        await page.GotoAsync(DialogsUrl);

        await CloseGreenNotificationAsync(page);

        await OpenDialogAsync(page, "Open Simple Dialog");
        await CloseDialogAsync(page);

        await OpenDialogAsync(page, "Open Alert Dialog");
        await ClickDialogButtonAsync(page, "Agree");

        await OpenFormDialogAsync(page);

        await OpenDialogAsync(page, "Slide In Dialog");
        await ClickDialogButtonAsync(page, "Agree");

        await OpenDialogAsync(page, "Open Dialog");
        await ClickDialogButtonAsync(page, "Save Changes");

        await OpenFullScreenDialogAsync(page);

        await OpenDialogAsync(page, "Open Max-Width Dialog");
        await ClickDialogButtonAsync(page, "Close");

        await OpenResponsiveDialogAsync(page);

        await OpenDialogAsync(page, "Open Draggable Dialog");
        await ClickDialogButtonAsync(page, "Subscribe");

        await OpenDialogAsync(page, "Scroll=Paper");
        await ClickDialogButtonAsync(page, "Subscribe");

        await OpenDialogAsync(page, "Scroll=Body");
        await ClickDialogButtonAsync(page, "Subscribe");
    }

    private static async Task OpenFormDialogAsync(IPage page)
    {
        await OpenDialogAsync(page, "Open Form Dialog");

        ILocator dialog = page.GetByRole(AriaRole.Dialog).First;

        await dialog.GetByRole(
            AriaRole.Textbox,
            new()
            {
                Name = "Email Address",
                Exact = true
            })
            .FillAsync("student@test.com");

        await ClickDialogButtonAsync(page, "Subscribe");
    }

    private static async Task OpenResponsiveDialogAsync(IPage page)
    {
        await OpenDialogAsync(page, "Open Responsive Dialog");

        ILocator dialog = page.GetByRole(AriaRole.Dialog);

        await dialog.GetByRole(
            AriaRole.Button,
            new()
            {
                Name = "Agree",
                Exact = true
            })
            .ClickAsync();
    }

    private static async Task OpenDialogAsync(IPage page, string buttonName)
    {
        Regex buttonNameRegex = new(
            $"^{Regex.Escape(buttonName)}$",
            RegexOptions.IgnoreCase
        );

        await page.GetByRole(
            AriaRole.Button,
            new()
            {
                NameRegex = buttonNameRegex
            })
            .ClickAsync();

        await Expect(page.GetByRole(AriaRole.Dialog).First)
            .ToBeVisibleAsync();
    }

    private static async Task ClickDialogButtonAsync(IPage page, string buttonName)
    {
        Regex buttonNameRegex = new(
            $"^{Regex.Escape(buttonName)}$",
            RegexOptions.IgnoreCase
        );

        ILocator dialog = page
            .GetByRole(AriaRole.Dialog)
            .Filter(new()
            {
                Has = page.GetByRole(
                    AriaRole.Button,
                    new()
                    {
                        NameRegex = buttonNameRegex
                    })
            })
            .First;

        await dialog.GetByRole(
            AriaRole.Button,
            new()
            {
                NameRegex = buttonNameRegex
            })
            .ClickAsync();

        await Expect(dialog).ToBeHiddenAsync();
    }

    private static async Task CloseDialogAsync(IPage page)
    {
        ILocator dialog = page.GetByRole(AriaRole.Dialog).First;

        await dialog.GetByRole(
            AriaRole.Button,
            new()
            {
                NameRegex = new Regex("close", RegexOptions.IgnoreCase)
            })
            .ClickAsync();

        await Expect(dialog).ToBeHiddenAsync();
    }

    private static async Task OpenFullScreenDialogAsync(IPage page)
    {
        await page.GetByRole(
            AriaRole.Button,
            new()
            {
                NameRegex = new Regex(
                    "^Open Full.*Screen Dialog$",
                    RegexOptions.IgnoreCase)
            })
            .ClickAsync();

        await Expect(page.GetByText("Set Backup Account"))
            .ToBeVisibleAsync();

        // await page.Keyboard.PressAsync("Escape");
        await page
            .GetByLabel("close")
            .First
            .ClickAsync();

        await Expect(page.GetByText("Set Backup Account"))
            .ToBeHiddenAsync();
    }

    private static async Task CloseGreenNotificationAsync(IPage page)
    {
        await page
        .Locator("div")
        .Filter(new() { HasText = "Build faster with ready-to-use prompts" })
        .Locator("svg, button, span")
        .Last
        .ClickAsync();
    }
}