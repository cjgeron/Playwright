using Activity9.RealisticE2EWorkflowAutomation.Tests.Fixtures;
using Activity9.RealisticE2EWorkflowAutomation.Tests.Utilities;
using Activity9.RealisticE2EWorkflowAutomation.Tests.Workflows;
using Microsoft.Playwright;
using Xunit;

namespace Activity9.RealisticE2EWorkflowAutomation.Tests.Tests;

public sealed class MantisRealisticE2EWorkflowTests : IClassFixture<PlaywrightFixture>
{
    private readonly PlaywrightFixture _fixture;

    public MantisRealisticE2EWorkflowTests(PlaywrightFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Should_Complete_Profile_Update_And_Order_Workflow_From_UI()
    {
        IBrowserContext context = await _fixture.Browser.NewContextAsync(
            new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize
                {
                    Width = 1920,
                    Height = 1080
                }
            });

        MantisBusinessWorkflow workflow = new(context);

        await workflow.ExecuteProfileAndOrderWorkflowAsync(
            loginUser: TestDataFactory.ValidLoginUser(),
            profile: TestDataFactory.ValidUserProfile(),
            shipping: TestDataFactory.ValidShippingAddress(),
            payment: TestDataFactory.ValidPaymentDetails()
        );

        await context.CloseAsync();
    }
}