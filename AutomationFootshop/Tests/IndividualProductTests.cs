using AutomationFootshop.Controls;
using AutomationFootshop.PageObjects;
using AutomationFootshop.PageObjects.Products;
using AutomationFootshop.PageObjects.Products.InputData;
using AutomationFootshop.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomationFootshop.Tests
{
    [TestClass]
    public class IndividualProductTests
    {
        private IWebDriver driver;
        private FiltersBO filtersBO = new FiltersBO();
        private HomePage homePage;
        private IndividualProductPage individualProductPage;
        private CartSidebar cartSidebar;

        [TestInitialize]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.footshop.ro/ro/");

            var menu = new VisitorMenuControl(driver);
            var loginPage = menu.NavigateToLoginPage();

            homePage = loginPage.Login("testare@test.com", "ParolaComplicata123");
            homePage.loggedInMenuControl.AcceptCookies();

            var productsPage = homePage.loggedInMenuControl.NavigateToProductsPage();
            productsPage.AddProductToWishlist(filtersBO);

            var wishlistPage = homePage.loggedInMenuControl.NavigateToWishlistPage();

            individualProductPage = wishlistPage.NavigateToIndividualProductPage();
        }

        [TestCleanup]
        public void Cleanup()
        {
            var wishlistPage = homePage.loggedInMenuControl.NavigateToWishlistPage();
            wishlistPage.RemoveWishlistProducts();

            driver.Quit();
        }

        [TestMethod]
        public void AddToCartButton_Should_AddProductToCart_When_SizeIsAvailable()
        {
            individualProductPage.SelectSizeFromDropdown(filtersBO);

            cartSidebar = individualProductPage.NavigateToCartSidebar();
            cartSidebar.CloseSidebarCart();

            homePage.loggedInMenuControl.BtnHome.Click();
            cartSidebar = homePage.loggedInMenuControl.NavigateToCartSidebar();

            Assert.IsTrue(cartSidebar.GetProductName().Contains(filtersBO.Name));
            Assert.IsTrue(cartSidebar.GetProductSize().Contains($"EUR {filtersBO.Size}"));

            //specific cleanup
            cartSidebar.CloseSidebarCart();
        }

        [TestMethod]
        public void AddToCartButton_Should_OpenAModal_When_SizeIsNotAvailable()
        {
            filtersBO = new FiltersBO
            {
                Size = "37"
            };
            individualProductPage.SelectSizeFromDropdown(filtersBO);

            var restockModal = new RestockModal(driver);

            Assert.AreEqual(filtersBO.Name, restockModal.LblProductName.Text.ToUpper());
            Assert.AreEqual(Messages.RestockMessage, restockModal.LblRestockMessage.Text);

            //specific cleanup
            restockModal.CloseModal();
        }

        [TestMethod]
        public void Should_AddPromocode_When_PromocodeExists()
        {
            individualProductPage.SelectSizeFromDropdown(filtersBO);

            cartSidebar = individualProductPage.NavigateToCartSidebar();
            cartSidebar.CloseSidebarCart();

            homePage.loggedInMenuControl.BtnHome.Click();

            cartSidebar = homePage.loggedInMenuControl.NavigateToCartSidebar();
            cartSidebar.ApplyPromocode(Messages.PromoCode);;

            Assert.AreEqual(Messages.PromoCodeMessage, cartSidebar.GetPromoCodeLabel());

            //specific cleanup
            cartSidebar.CloseSidebarCart();
        }

        [TestMethod]
        public void Should_ShowErrorMessage_When_PromocodeDoesNotExist()
        {
            individualProductPage.SelectSizeFromDropdown(filtersBO);

            cartSidebar = individualProductPage.NavigateToCartSidebar();
            cartSidebar.CloseSidebarCart();

            homePage.loggedInMenuControl.BtnHome.Click();

            cartSidebar = homePage.loggedInMenuControl.NavigateToCartSidebar();
            cartSidebar.ApplyPromocode("abcd");

            Assert.AreEqual(Messages.PromoCodeErrorMessage, cartSidebar.GetErrorPromoCodeLabel());

            //specific cleanup
            cartSidebar.ClosePromoCodeErrorMessage();
            cartSidebar.CloseSidebarCart();
        }
    }
}
