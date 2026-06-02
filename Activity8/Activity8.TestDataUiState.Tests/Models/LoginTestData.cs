namespace Activity8.TestDataUiState.Tests.Models;

public class LoginTestData
{
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
    public bool ShouldLoginSuccessfully { get; set; }
    public string ExpectedMessage { get; set; } = "";
}