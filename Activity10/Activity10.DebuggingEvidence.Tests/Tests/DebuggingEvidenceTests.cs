using Activity10.DebuggingEvidence.Tests.Pages;
using Activity10.DebuggingEvidence.Tests.TestInfrastructure;
using Microsoft.Playwright;

namespace Activity10.DebuggingEvidence.Tests.Tests;

public sealed class DebuggingEvidenceTests : PlaywrightTestBase
{
    [Fact]
    public async Task Should_Login_And_Capture_Debugging_Evidence()
    {
        var loginPage = new MantisLoginPage(Page);

        try
        {
            await loginPage.GotoAsync();

            await loginPage.LoginAsync(
                "a@a.com",
                "password"
            );

            await loginPage.AssertDashboardLoadedAsync();

            await CaptureScreenshotAsync(nameof(Should_Login_And_Capture_Debugging_Evidence));
        }
        catch
        {
            await CaptureScreenshotAsync($"{nameof(Should_Login_And_Capture_Debugging_Evidence)}-FAILED");
            throw;
        }
    }

    [Fact]
    public async Task Should_Handle_Flaky_Loading_Using_Expect_And_Retry_Style_Assertion()
    {
        await Page.GotoAsync(
            "http://localhost:3000",
            new PageGotoOptions
            {
                WaitUntil = WaitUntilState.DOMContentLoaded
            });

        await Page.GetByText("Already have an account?").ClickAsync();

        await Page.GetByPlaceholder("Enter email address").FillAsync("a@a.com");
        await Page.GetByPlaceholder("Enter password").FillAsync("password");

        await Page.GetByRole(
            AriaRole.Button,
            new() { Name = "Login" }
        ).ClickAsync();

        await Assertions.Expect(
            Page.GetByRole(AriaRole.Heading, new() { Name = "Dashboard" })
        ).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions
        {
            Timeout = 10000
        });

        await Assertions.Expect(
            Page.GetByText("Total Order", new() { Exact = true }).First
        ).ToBeVisibleAsync(new LocatorAssertionsToBeVisibleOptions
        {
            Timeout = 10000
        });
    }

    [Fact]
    public async Task Should_Demonstrate_Debug_Pause_When_Running_Headed()
    {
        await Page.GotoAsync("http://localhost:3000");

        await Page.GetByText("Already have an account?").ClickAsync();

        await Page.GetByPlaceholder("Enter email address").FillAsync("a@a.com");

        // Use this only while debugging.
        // Run the test in headed mode to inspect the page manually.
        // await Page.PauseAsync();

        await Page.GetByPlaceholder("Enter password").FillAsync("password");

        await Page.GetByRole(
            AriaRole.Button,
            new() { Name = "Login" }
        ).ClickAsync();

        await Assertions.Expect(
            Page.GetByRole(AriaRole.Heading, new() { Name = "Dashboard" })
        ).ToBeVisibleAsync();
    }
}
