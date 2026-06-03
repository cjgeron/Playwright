namespace TrainingApp.UiTests;

public class Case5AsyncLifetime : IAsyncLifetime
{
    public async Task InitializeAsync()
    {
        Console.WriteLine("Async Setup");

        await Task.Delay(1000);
    }

    [Fact]
    public async Task Test1()
    {
        Console.WriteLine("Test 1");
        await Task.Delay(500);
    }

    [Fact]
    public async Task Test2()
    {
        Console.WriteLine("Test 2");
        await Task.Delay(500);
    }    

    public async Task DisposeAsync()
    {
        Console.WriteLine("Async Cleanup");

        await Task.Delay(1000);
    }
}