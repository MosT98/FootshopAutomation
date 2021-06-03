using AutomationFootshop.Utils;
using OpenQA.Selenium;

namespace AutomationFootshop.PageObjects.Products
{
    public class CartSidebar
    {
        private IWebDriver driver;
        private By ProductDetails = By.CssSelector("div.OldProductItem_product_jU7DN");
        private By PromoCodeCheck = By.CssSelector("div.OldDiscount_heading_225Zj div.Checkbox_container_307eG div.Checkbox_checkbox_mWkKj");
        private By PromoCodeInput = By.CssSelector("input[placeholder='Vouchere']");
        private By AddPromoCode = By.ClassName("OldDiscount_submit_khw7E");
        private By DeletePromoCode = By.CssSelector("span.OldVoucherItem_delete_1Lazc");
        private By PromoCodeLabel = By.CssSelector("span.OldVoucherItem_name_10UwT");
        private By PromoCodeErrorLabel = By.CssSelector("#notifications div.Notification_error_lgNgK span.Notification_message_1MYtI");
        private By ProductName = By.CssSelector("strong.OldProductItem_name_2g_xM");
        private By ProductSize = By.CssSelector("div.OldProductItem_product_jU7DN small");
        private By CloseSidebar = By.CssSelector("button.CloseButton_close_21oMi");
        private By ClosePromoCodeError = By.XPath("//span[contains(text(), 'OK')]");


        public CartSidebar(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement LblProductDetails => driver.FindElement(ProductDetails);
        public IWebElement ChbPromoCode => driver.FindElement(PromoCodeCheck);
        public IWebElement TxtPromoCode => driver.FindElement(PromoCodeInput);
        public IWebElement BtnAddPromoCode => driver.FindElement(AddPromoCode);
        public IWebElement BtnDeletePromoCode => driver.FindElement(DeletePromoCode);
        public IWebElement LblPromoCode => driver.FindElement(PromoCodeLabel);
        public IWebElement LblPromoCodeError => driver.FindElement(PromoCodeErrorLabel);
        public IWebElement BtnCloseSidebar => driver.FindElement(CloseSidebar);
        public IWebElement BtnClosePromoCodeError => driver.FindElement(ClosePromoCodeError);

        public string GetProductName()
        {
            WaitHelpers.WaitElementToBeVisible(driver, ProductName);

            return LblProductDetails.FindElement(ProductName).Text.ToUpper();
        }
        public string GetProductSize() 
        {
            WaitHelpers.WaitElementToBeVisible(driver, ProductSize);

            return LblProductDetails.FindElement(ProductSize).Text.ToUpper();
        } 

        public void CheckExistentPromoCode()
        {
            if (BtnDeletePromoCode.Displayed)
            {
                BtnDeletePromoCode.Click();
            }
        }

        public void ApplyPromocode(string code)
        {
            WaitHelpers.WaitElementToBeVisible(driver, PromoCodeCheck);

            ChbPromoCode.Click();

            TxtPromoCode.Clear();
            TxtPromoCode.SendKeys(code);

            BtnAddPromoCode.Click();
        }

        public string GetPromoCodeLabel()
        {
            WaitHelpers.WaitElementToBeVisible(driver, PromoCodeLabel);

            return LblPromoCode.Text;
        }

        public void CloseSidebarCart()
        {
            WaitHelpers.WaitElementToBeVisible(driver, CloseSidebar);

            BtnCloseSidebar.Click();
        }

        public string GetErrorPromoCodeLabel()
        {
            WaitHelpers.WaitElementToBeVisible(driver, PromoCodeErrorLabel);

            return LblPromoCodeError.Text;
        }

        public void ClosePromoCodeErrorMessage()
        {
            WaitHelpers.WaitElementToBeVisible(driver, ClosePromoCodeError);

            BtnClosePromoCodeError.Click();
        }
    }
}
