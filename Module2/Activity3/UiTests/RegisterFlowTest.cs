using Microsoft.Playwright;

namespace Activity3.UiTests;

public class RegisterFlowTest : IClassFixture<PlaywrightFixture>, IAsyncLifetime
{
    private readonly PlaywrightFixture _fixture;

    private IBrowserContext _context = null!;
    private IPage _page = null!;

    public RegisterFlowTest(PlaywrightFixture fixture)
    {
        _fixture = fixture;
    }

    public async Task InitializeAsync()
    {
        _context = await _fixture.Browser.NewContextAsync();
        _page = await _context.NewPageAsync();
    }

    [Fact]
    public async Task Should_Register_New_User_And_Show_Login()
    {
        await _page.GotoAsync($"{_fixture.BaseUrl}/register");

        string uniqueEmail = $"testuser{DateTime.Now:yyyyMMddHHmmss}@demo.com";

        await _page.GetByPlaceholder("John").FillAsync("John");
        await _page.GetByPlaceholder("Doe").FillAsync("Doe");
        await _page.GetByPlaceholder("Demo Inc.").FillAsync("Demo Company");
        await _page.GetByPlaceholder("demo@company.com").FillAsync(uniqueEmail);
        await _page.GetByPlaceholder("******").FillAsync("Password");

        await _page.GetByRole(
            AriaRole.Button,
            new() { Name = "Create Account" }
        ).ClickAsync();

        ILocator loginHeading = _page.GetByRole(
            AriaRole.Heading,
            new() { Name = "Login" }
        );

        await Assertions.Expect(loginHeading).ToBeVisibleAsync();
    }

    public async Task DisposeAsync()
    {
        await _context.CloseAsync();
    }
}


