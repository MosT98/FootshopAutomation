using AutomationFootshop.PageObjects.Products;
using AutomationFootshop.Utils;
using OpenQA.Selenium;

namespace AutomationFootshop.Controls
{
    public class MenuControl
    {
        private By Men = By.XPath("//a[contains(text(), 'Pentru el')]");

        protected IWebDriver driver;

        public MenuControl(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement BtnMenuMen => driver.FindElement(Men);

        public ProductsPage NavigateToProductsPage()
        {
            WaitHelpers.WaitElementToBeVisible(driver, Men);
            BtnMenuMen.Click();

            return new ProductsPage(driver);
        }
    }
}
