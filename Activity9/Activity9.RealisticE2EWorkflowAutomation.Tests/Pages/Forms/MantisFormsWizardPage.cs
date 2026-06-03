using Activity9.RealisticE2EWorkflowAutomation.Tests.Models;
using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace Activity9.RealisticE2EWorkflowAutomation.Tests.Pages.Forms;

public sealed class MantisFormsWizardPage
{
    private readonly IPage _page;
    private readonly ILocator _basicSection;

    public MantisFormsWizardPage(IPage page)
    {
        _page = page;

        _basicSection = _page
            .Locator(".MuiCard-root")
            .Filter(new()
            {
                Has = _page.GetByText("Basic", new() { Exact = true })
            });
    }

    public async Task GotoAsync()
    {
        await _page.GotoAsync(
            "https://mantisdashboard.com/forms/wizard",
            new PageGotoOptions
            {
                WaitUntil = WaitUntilState.DOMContentLoaded
            });

        await Assertions.Expect(
            _page.Locator("h2").GetByText("Forms Wizard", new() { Exact = true })
        ).ToBeVisibleAsync();

        await Assertions.Expect(
            _basicSection.GetByText("Shipping address", new() { Exact = true }).First
        ).ToBeVisibleAsync();
    }

    public async Task FillShippingAddressAsync(ShippingAddressModel shipping)
    {
        await _basicSection.Locator("#firstNameBasic").FillAsync(shipping.FirstName);
        await _basicSection.Locator("#lastNameBasic").FillAsync(shipping.LastName);
        await _basicSection.Locator("#address1Basic").FillAsync(shipping.Address1);
        await _basicSection.Locator("#address2Basic").FillAsync(shipping.Address2);
        await _basicSection.Locator("#cityBasic").FillAsync(shipping.City);
        await _basicSection.Locator("#stateBasic").FillAsync(shipping.State);
        await _basicSection.Locator("#zipBasic").FillAsync(shipping.ZipCode);
        await _basicSection.Locator("#countryBasic").FillAsync(shipping.Country);

        await _basicSection
            .GetByRole(
                AriaRole.Checkbox,
                new() { Name = "Use this address for payment details" })
            .CheckAsync();

        await _basicSection
            .GetByRole(AriaRole.Button, new() { Name = "Next" })
            .ClickAsync();

        await Assertions.Expect(
            _basicSection.GetByText("Payment method", new() { Exact = true })
        ).ToBeVisibleAsync();
    }

    public async Task FillPaymentDetailsAsync(PaymentDetailsModel payment)
    {
        await _basicSection.GetByPlaceholder("Name on card").FillAsync(payment.NameOnCard);
        await _basicSection.GetByPlaceholder("Card number").FillAsync(payment.CardNumber);
        await _basicSection.GetByPlaceholder("Expiry date").FillAsync(payment.ExpiryDate);
        await _basicSection.GetByPlaceholder("CVV").FillAsync(payment.Cvv);

        await _basicSection
            .GetByRole(AriaRole.Button, new() { Name = "Next" })
            .ClickAsync();

        await Assertions.Expect(
            _basicSection.GetByText("Review your order")
        ).ToBeVisibleAsync();
    }

    public async Task PlaceOrderAsync()
    {
        await _basicSection
            .GetByRole(AriaRole.Button, new() { Name = "Place order" })
            .ClickAsync();

        await Assertions.Expect(
            _basicSection.GetByText("Thank you for your order.")
        ).ToBeVisibleAsync();

        await Assertions.Expect(
            _basicSection.GetByText(new Regex(@"Your order number is #\d+"))
        ).ToBeVisibleAsync();

        await Assertions.Expect(
            _basicSection.GetByRole(AriaRole.Button, new() { Name = "Reset" })
        ).ToBeVisibleAsync();
    }

    public async Task CompleteBasicWizardFlowAsync(
        ShippingAddressModel shipping,
        PaymentDetailsModel payment)
    {
        await FillShippingAddressAsync(shipping);
        await FillPaymentDetailsAsync(payment);
        await PlaceOrderAsync();
    }
}