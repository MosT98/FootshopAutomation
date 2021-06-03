using AutomationFootshop.Controls;
using AutomationFootshop.PageObjects;
using AutomationFootshop.PageObjects.Products;
using AutomationFootshop.PageObjects.Products.InputData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomationFootshop.Tests
{
    [TestClass]
    public class ProductsTests
    {
        private IWebDriver driver;
        private ProductsPage productsPage;
        private HomePage homePage;
        private FiltersBO filtersBO = new FiltersBO();
        private IJavaScriptExecutor js;

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

            productsPage = homePage.loggedInMenuControl.NavigateToProductsPage();
        }

        [TestCleanup]
        public void CleanUp()
        {
            driver.Quit();
        }

        [TestMethod]
        public void Should_SuccessfullyAddTwoProductFilters_When_TwoFiltersAreSelected()
        {
            productsPage.AddFilters(filtersBO);

            Assert.AreEqual(2, productsPage.LstActiveFilters.Count);
        }

        [TestMethod]
        public void Should_SuccessfullyAddProductToWishlist_When_WishlistIsEmpty_And_ProductIsSelected()
        {
            productsPage.AddFilters(filtersBO);
            productsPage.SelectProductByName(filtersBO);

            js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView();", homePage.loggedInMenuControl.GetWishlistQuantity());

            Assert.AreEqual("1", homePage.loggedInMenuControl.LblWishlistQuantity.Text);

            var wishlistPage = homePage.loggedInMenuControl.NavigateToWishlistPage();

            //Didn't add this in Cleanup method because this method doesn't do anything for the first test
            wishlistPage.RemoveWishlistProducts(); //needed for the cleanup
        }
    }
}
