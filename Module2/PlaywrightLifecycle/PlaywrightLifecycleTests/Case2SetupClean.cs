namespace TrainingApp.UiTests;

public class Case2SetupClean : IDisposable
{
    public Case2SetupClean()
    {
        Console.WriteLine("Constructor - Setup/Initialization");
    }

    [Fact]
    public void Test1()
    {
        Console.WriteLine("Normal unit tests only");
    }

    [Fact]
    public async Task Test2()
    {
        Console.WriteLine(@"
        Database/API calls,
        Playwright UI tests,
        Selenium old-style tests,
        Modern browser automation
        ");
    }

    public void Dispose()
    {
        Console.WriteLine("Dispose - Cleanup");
    }
}