using Activity9.RealisticE2EWorkflowAutomation.Tests.Models;

namespace Activity9.RealisticE2EWorkflowAutomation.Tests.Utilities;

public static class TestDataFactory
{
    public static LoginUserModel ValidLoginUser()
    {
        return new LoginUserModel
        {
            Email = "info@codedthemes.com",
            Password = "123456"
        };
    }

    public static UserProfileModel ValidUserProfile()
    {
        return new UserProfileModel
        {
            FirstName = "Stebin",
            LastName = "Ben",
            Email = "stebin.ben@gmail.com",
            Phone = "9652364852",
            Designation = "Full Stack Developer",
            Address1 = "3801 Chalk Butte Rd, Cut Bank, MT 59427, United States",
            Address2 = "301 Chalk Butte Rd, Cut Bank, NY 96572, New York",
            State = "California"
        };
    }

    public static ShippingAddressModel ValidShippingAddress()
    {
        return new ShippingAddressModel
        {
            FirstName = "Stebin",
            LastName = "Ben",
            Address1 = "3801 Chalk Butte Rd",
            Address2 = "Unit 10",
            City = "Cut Bank",
            State = "California",
            ZipCode = "59427",
            Country = "United States"
        };
    }

    public static PaymentDetailsModel ValidPaymentDetails()
    {
        return new PaymentDetailsModel
        {
            NameOnCard = "Stebin Ben",
            CardNumber = "4111111111111111",
            ExpiryDate = "12/30",
            Cvv = "123"
        };
    }
}