using AutomationFootshop.Controls;
using AutomationFootshop.PageObjects;
using AutomationFootshop.PageObjects.Products.InputData;
using AutomationFootshop.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace AutomationFootshop.Tests
{
    [TestClass]
    public class WishlistTests
    {
        private IWebDriver driver;
        private FiltersBO filtersBO = new FiltersBO();
        private WishlistPage wishlistPage;
        private HomePage homePage;

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
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
        }

        [TestMethod]
        public void Should_ShowMessage_When_WishlistIsEmpty()
        {
            wishlistPage = homePage.loggedInMenuControl.NavigateToWishlistPage();

            Assert.AreEqual(Messages.EmptyWishlistMessage, wishlistPage.LblEmptyWishlist.Text);
        }

        [TestMethod]
        public void Should_ShowProduct_When_ProductWasPreviouslyAdded()
        {
            var productsPage = homePage.loggedInMenuControl.NavigateToProductsPage();
            productsPage.AddProductToWishlist(filtersBO);

            wishlistPage = homePage.loggedInMenuControl.NavigateToWishlistPage();

            Assert.AreEqual(wishlistPage.GetProductName().ToUpper(), filtersBO.Name);

            //Didn't add this in Cleanup method because this method doesn't do anything for the first test
            wishlistPage.RemoveWishlistProducts(); //needed for the cleanup
        }
    }
}
