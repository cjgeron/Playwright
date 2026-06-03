using Activity9.RealisticE2EWorkflowAutomation.Tests.Models;
using Activity9.RealisticE2EWorkflowAutomation.Tests.Pages.Components;
using Microsoft.Playwright;

namespace Activity9.RealisticE2EWorkflowAutomation.Tests.Pages.Profile;

public sealed class MantisProfilePage
{
    private readonly IPage _page;
    private readonly SidebarMenuComponent _sidebarMenu;

    public MantisProfilePage(IPage page)
    {
        _page = page;
        _sidebarMenu = new SidebarMenuComponent(page);
    }

    public async Task OpenAsync()
    {
        await _sidebarMenu.OpenUserProfileAsync();
    }

public async Task UpdatePersonalInformationAsync(UserProfileModel profile)
{
    await _page.GetByLabel("First Name").FillAsync(profile.FirstName);
    await _page.GetByLabel("Last Name").FillAsync(profile.LastName);
    await _page.GetByLabel("Email Address").FillAsync(profile.Email);
    await _page.GetByLabel("Designation").FillAsync(profile.Designation);
    await _page.GetByLabel("Address 01").FillAsync(profile.Address1);
    await _page.GetByLabel("Address 02").FillAsync(profile.Address2);
    await _page.GetByLabel("State").FillAsync(profile.State);

    await _page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();
}

public async Task VerifyPersonalInformationAsync(UserProfileModel profile)
{
    await Assertions.Expect(_page.GetByLabel("First Name")).ToHaveValueAsync(profile.FirstName);
    await Assertions.Expect(_page.GetByLabel("Last Name")).ToHaveValueAsync(profile.LastName);
    await Assertions.Expect(_page.GetByLabel("Email Address")).ToHaveValueAsync(profile.Email);
    await Assertions.Expect(_page.GetByLabel("Designation")).ToHaveValueAsync(profile.Designation);
    await Assertions.Expect(_page.GetByLabel("Address 01")).ToHaveValueAsync(profile.Address1);
    await Assertions.Expect(_page.GetByLabel("Address 02")).ToHaveValueAsync(profile.Address2);
    await Assertions.Expect(_page.GetByLabel("State")).ToHaveValueAsync(profile.State);
}
}