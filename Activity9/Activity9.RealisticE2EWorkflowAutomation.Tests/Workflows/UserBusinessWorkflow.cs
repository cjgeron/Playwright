using Activity9.RealisticE2EWorkflowAutomation.Tests.Models;
using Activity9.RealisticE2EWorkflowAutomation.Tests.Pages.Authentication;
using Activity9.RealisticE2EWorkflowAutomation.Tests.Pages.Components;
using Activity9.RealisticE2EWorkflowAutomation.Tests.Pages.Forms;
using Activity9.RealisticE2EWorkflowAutomation.Tests.Pages.Profile;
using Microsoft.Playwright;

namespace Activity9.RealisticE2EWorkflowAutomation.Tests.Workflows;

public sealed class MantisBusinessWorkflow
{
    private readonly IBrowserContext _context;

    public MantisBusinessWorkflow(IBrowserContext context)
    {
        _context = context;
    }

    public async Task ExecuteProfileAndOrderWorkflowAsync(
        LoginUserModel loginUser,
        UserProfileModel profile,
        ShippingAddressModel shipping,
        PaymentDetailsModel payment)
    {
        IPage profilePageTab = await _context.NewPageAsync();

        MantisLoginPage loginPage = new(profilePageTab);
        MantisProfilePage profilePage = new(profilePageTab);
        HeaderMenuComponent headerMenu = new(profilePageTab);

        await loginPage.GotoAsync();
        await loginPage.ClosePromoPopupAsync();
        await loginPage.LoginAsync(loginUser);

        await profilePage.OpenAsync();
        await profilePage.UpdatePersonalInformationAsync(profile);
        await profilePage.VerifyPersonalInformationAsync(profile);

        IPage wizardPageTab = await _context.NewPageAsync();

        MantisFormsWizardPage wizardPage = new(wizardPageTab);

        await wizardPage.GotoAsync();
        await wizardPage.CompleteBasicWizardFlowAsync(shipping, payment);

        await profilePageTab.BringToFrontAsync();

        await headerMenu.LogoutAsync();

        await loginPage.VerifyLoginPageAsync();
    }
}

