using AutomationFootshop.Utils;
using OpenQA.Selenium;

namespace AutomationFootshop.PageObjects.Products
{
    public class RestockModal
    {
        private IWebDriver driver;
        private By Restock = By.CssSelector("div.RestockAlert_wrapper_2zgyb div.Form_form_2DG-D");
        private By ProductName = By.CssSelector("div.Headline_wrapper_FF3Z0 > h2");
        private By RestockMessage = By.CssSelector("p:first-of-type");
        private By Close = By.CssSelector("span.ModalWindow_closeButton_LOoG1");

        public RestockModal(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement FrmRestock => driver.FindElement(Restock);
        public IWebElement LblProductName => FrmRestock.FindElement(ProductName);
        public IWebElement LblRestockMessage => FrmRestock.FindElement(RestockMessage);
        public IWebElement BtnCloseModal => driver.FindElement(Close);

        public void CloseModal()
        {
            WaitHelpers.WaitElementToBeVisible(driver, Close);

            BtnCloseModal.Click();
        }
    }
}
