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
        public void Should_BePossibleTo_CheckVisualState()
        {
            var sliderForm = new SliderForm();
            Assume.That(sliderForm.State.WaitForDisplayed(), "Slider Form is not opened");
            var style = sliderForm.Style;
            Assert.DoesNotThrow(() => sliderForm.Dump.Save(), "Should be possible to save dump");
            Assert.That(sliderForm.Dump.Compare() == 0L || sliderForm.Style != style, "Form dump should remain the same, unless the slider has already scrolled");
            sliderForm.ClickNextButton();
            Assert.That(sliderForm.Dump.Compare(), Is.GreaterThan(0), "After clicking on slider next button, the form dump should differ");
            Assert.That(AqualityServices.ConditionalWait.WaitFor(() =>
            {
                sliderForm.ClickNextButton();
                return sliderForm.Dump.Compare() == 0L;
            }), "After some clicking on slider the form dump should be the same as initial");
        }

        [Test]
        public void Should_BePossibleTo_PerformActions()
        {
            var sliderForm = new SliderForm();
            Assert.IsTrue(sliderForm.State.WaitForDisplayed(), "Slider Form is not opened");

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
            Assert.IsTrue(authForm.State.WaitForDisplayed(), "Authentication Form is not opened");

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
