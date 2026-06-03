namespace TrainingApp.UiTests;

[CollectionDefinition("DatabaseCollection")]
public class DatabaseCollection : ICollectionFixture<DbFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be anchor to apply [CollectionDefinition] and ICollectionFixture<>.
}
