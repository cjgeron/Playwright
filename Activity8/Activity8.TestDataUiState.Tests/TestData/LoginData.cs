using Activity8.TestDataUiState.Tests.Models;

namespace Activity8.TestDataUiState.Tests.TestData;

public static class LoginData
{
    public static IEnumerable<object[]> LoginScenarios()
    {
        yield return new object[]
        {
            new LoginTestData
            {
                Email = "a@a.com",
                Password = "password",
                ShouldLoginSuccessfully = true,
                ExpectedMessage = "Dashboard"
            }
        };

        yield return new object[]
        {
            new LoginTestData
            {
                Email = "wrong@mantisdashboard.com",
                Password = "wrong",
                ShouldLoginSuccessfully = false,
                ExpectedMessage = "Invalid"
            }
        };

        yield return new object[]
        {
            new LoginTestData
            {
                Email = "",
                Password = "",
                ShouldLoginSuccessfully = false,
                ExpectedMessage = "required"
            }
        };
    }
}