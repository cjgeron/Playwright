namespace TrainingApp.UiTests;

[Collection("DatabaseCollection")]
public class Case4CustomerTests
{
    private readonly DbFixture _fixture;

    public Case4CustomerTests(DbFixture fixture)
    {
        _fixture = fixture; // Injected instance is shared with other classes
    }

    [Fact]
    public void Test_Can_Read_Customers()
    {
        var connStr = _fixture.ConnectionString;
        Console.WriteLine($"Test_Can_Read_Customers: {connStr}");
    }
}
