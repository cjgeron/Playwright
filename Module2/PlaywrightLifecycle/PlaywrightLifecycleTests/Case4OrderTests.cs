namespace TrainingApp.UiTests;

[Collection("DatabaseCollection")]
public class Case4OrderTests
{
    private readonly DbFixture _fixture;

    public Case4OrderTests(DbFixture fixture)
    {
        _fixture = fixture; // Exactly the same instance as CustomerTests
    }

    [Fact]
    public void Test_Can_Create_Order()
    {
        var connStr = _fixture.ConnectionString;
        Console.WriteLine($"Test_Can_Create_Order: {connStr}");
    }
}
