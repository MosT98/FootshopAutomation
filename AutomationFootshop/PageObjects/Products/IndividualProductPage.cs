using AutomationFootshop.PageObjects.Products.InputData;
using AutomationFootshop.Utils;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace AutomationFootshop.PageObjects.Products
{
    public class IndividualProductPage
    {
        private IWebDriver driver;
        private By ProductName = By.CssSelector("div.ProductProperties_relative_2hkcX > h1");
        private By ProductSizeDropdown = By.CssSelector("#nav_tabEUR div.Dropdown_dropdown_3tKJH .Dropdown_arrowDown_JD82S");
        private By SizeDropdown = By.CssSelector("div.Dropdown_list_GFfGs div.Dropdown_item_3d-b-");
        private By AddToCart = By.CssSelector("button.ProductProperties_addToCart_32JsZ");
        private By Size = By.CssSelector("span.ProductSizesWithAvailability_sizeItemCellActive_39wvn");

        public IndividualProductPage(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement LblProductName => driver.FindElement(ProductName);
        public IWebElement BtnExpandProductSizeDropdown => driver.FindElement(ProductSizeDropdown);
        public IList<IWebElement> LstSizeDropdown => driver.FindElements(SizeDropdown);
        public IWebElement BtnAddToCart => driver.FindElement(AddToCart);

        public void SelectSizeFromDropdown(FiltersBO filtersBO)
        {
            WaitHelpers.WaitElementToBeVisible(driver, ProductSizeDropdown);
            BtnExpandProductSizeDropdown.Click();

            LstSizeDropdown.FirstOrDefault(item => item.FindElement(Size).Text.Contains($"EUR {filtersBO.Size}"))
                .Click();
        }

        public string GetFormattedProductName() => LblProductName.Text.Split('\r')[0];

        public CartSidebar NavigateToCartSidebar()
        {
            BtnAddToCart.Click();

            return new CartSidebar(driver);
        }
    }
}
