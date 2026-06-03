namespace TrainingApp.UiTests;

public class DatabaseFixture
{
    public DatabaseFixture()
    {
        Console.WriteLine("Database Connected");
    }
}

public class Case3SharedSetup : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;

    public Case3SharedSetup(DatabaseFixture fixture)
    {
        _fixture = fixture;
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
}