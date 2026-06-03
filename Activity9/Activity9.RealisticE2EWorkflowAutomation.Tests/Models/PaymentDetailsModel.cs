namespace Activity9.RealisticE2EWorkflowAutomation.Tests.Models;

public sealed class PaymentDetailsModel
{
    public string NameOnCard { get; set; } = "";
    public string CardNumber { get; set; } = "";
    public string ExpiryDate { get; set; } = "";
    public string Cvv { get; set; } = "";
}