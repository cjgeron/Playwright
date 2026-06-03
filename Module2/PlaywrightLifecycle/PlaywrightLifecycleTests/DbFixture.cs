namespace TrainingApp.UiTests;

public class DbFixture : IDisposable
{
    public string ConnectionString { get; private set; }

    public DbFixture()
    {
        // One-time setup: initialize database connection or docker containers
        ConnectionString = "Server=localhost;Database=TestDb;";
        Console.WriteLine("Database setup initialized.");
    }

    public void Dispose()
    {
        // One-time teardown: clean up database resources
        Console.WriteLine("Database cleaned up and disposed.");
    }
}
