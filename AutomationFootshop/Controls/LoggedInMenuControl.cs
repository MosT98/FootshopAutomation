using AutomationFootshop.PageObjects;
using AutomationFootshop.PageObjects.Dashboard;
using AutomationFootshop.PageObjects.Products;
using AutomationFootshop.Utils;
using OpenQA.Selenium;

namespace AutomationFootshop.Controls
{
    public class LoggedInMenuControl : MenuControl
    {
        private By CookieConsent = By.CssSelector("#react-leaf > div > button");
        private By Dashboard = By.CssSelector(".Ultranav_icons_3ruqi > a[aria-label='User account']");
        private By Wishlist = By.CssSelector("a.CustomerListsIcon_wishIcon_2nPv9");
        private By WishlistQuantity = By.CssSelector("span.CustomerListsIcon_quantity_2Skde");
        private By Home = By.CssSelector("a[aria-label='Footshop homepage']");
        private By Cart = By.CssSelector("button[aria-label='Open cart']");

        public LoggedInMenuControl(IWebDriver browser) : base(browser)
        {
        }

        public IWebElement BtnCookieConsent => driver.FindElement(CookieConsent);
        public IWebElement BtnDashboard => driver.FindElement(Dashboard);
        public IWebElement BtnWishlist => driver.FindElement(Wishlist);
        public IWebElement LblWishlistQuantity => driver.FindElement(WishlistQuantity);
        public IWebElement BtnHome => driver.FindElement(Home);
        public IWebElement BtnCart => driver.FindElement(Cart);

        public IWebElement GetWishlistQuantity()
        {
            WaitHelpers.WaitElementToBeVisible(driver, WishlistQuantity);

            return LblWishlistQuantity;
        }

        public void AcceptCookies()
        {
            WaitHelpers.WaitElementToBeVisible(driver, CookieConsent);
            BtnCookieConsent.Click();
        }

        public DashboardPage NavigateToDashboardPage()
        {
            WaitHelpers.WaitElementToBeVisible(driver, Dashboard);
            BtnDashboard.Click();

            return new DashboardPage(driver);
        }

        public WishlistPage NavigateToWishlistPage()
        {
            WaitHelpers.WaitElementToBeVisible(driver, Wishlist);
            BtnWishlist.Click();

            return new WishlistPage(driver);
        }

        public CartSidebar NavigateToCartSidebar()
        {
            WaitHelpers.WaitElementToBeVisible(driver, Cart);
            BtnCart.Click();

            return new CartSidebar(driver);
        }
    }
}
