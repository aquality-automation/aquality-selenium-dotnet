using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Integration.TestApp;
using Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Forms;
using Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Modals;
using NUnit.Framework;
using System;

namespace Aquality.Selenium.Tests.Integration.Usecases
{
    internal class ShoppingCartTests : UITest
    {
        private static readonly int ExpectedNumdberOfProducts = 7;
        private static readonly int ExpectedQuantity = 2;
        private static readonly string FirstName = "John";
        private static readonly Gender Gender = Gender.Male;
        private static readonly int ExpectedNumberOfDays = 32;
        private static readonly int DayToSelect = 29;
        private static readonly string State = "California";

        private string Email => $"john+{DateTime.Now.Millisecond}@doe.com";

        [SetUp]
        public void BeforeTest()
        {
            AqualityServices.Browser.GoTo(Constants.UrlAutomationPractice);
            AqualityServices.Browser.Maximize();
        }

        [Test]
        public void Should_BePossibleTo_PerformActions()
        {
            var sliderForm = new SliderForm();
            Assert.IsTrue(sliderForm.IsDisplayed, "Slider Form is not opened");

            sliderForm.ClickNextButton();
            sliderForm.ClickNextButton();
            var productListForm = new ProductListForm();
            Assert.AreEqual(ExpectedNumdberOfProducts, productListForm.GetNumberOfProductsInContainer(), "Number of products is incorrect");

            productListForm.AddRandomProductToCart();
            var proceedToCheckoutModal = new ProceedToCheckoutModal();
            proceedToCheckoutModal.ClickProceedToCheckoutButton();
            var shoppingCardSummaryForm = new ShoppingCardSummaryForm();
            shoppingCardSummaryForm.ClickPlusButton();
            var actualQuantity = shoppingCardSummaryForm.WaitForQuantityAndGetValue(ExpectedQuantity);
            Assert.AreEqual(ExpectedQuantity, actualQuantity, "Quantity is incorrect");

            shoppingCardSummaryForm.ClickProceedToCheckoutButton();
            var authForm = new AuthenticationForm();
            Assert.IsTrue(authForm.IsDisplayed, "Authentication Form is not opened");

            var cartMenuForm = new CartMenuForm();
            cartMenuForm.OpenCartMenu();
            cartMenuForm.ClickCheckoutButton();

            shoppingCardSummaryForm.ClickProceedToCheckoutButton();
            authForm.SetEmail(Email);
            authForm.ClickCreateAccountButton();

            var personalInfoForm = new YourPersonalInfoForm();
            personalInfoForm.SelectGender(Gender);
            personalInfoForm.SetFirstName(FirstName);
            var actualNumberOfDays = personalInfoForm.GetNumberOfDays();
            Assert.AreEqual(ExpectedNumberOfDays, actualNumberOfDays, "Number of days from combobox is incorrect");

            personalInfoForm.SelectState(State);
            personalInfoForm.SelectDay(DayToSelect);
            Assert.IsFalse(personalInfoForm.IsNewsCheckBoxChecked(), "News checkbox state is not correct");

            personalInfoForm.SetNewsCheckBox();
            Assert.IsTrue(personalInfoForm.IsNewsCheckBoxChecked(), "News checkbox state is not correct");
        }
    }
}
