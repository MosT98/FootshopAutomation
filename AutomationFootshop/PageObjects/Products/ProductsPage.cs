using AutomationFootshop.PageObjects.Products.InputData;
using AutomationFootshop.Utils;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace AutomationFootshop.PageObjects.Products
{
    public class ProductsPage
    {
        private IWebDriver driver;
        private IJavaScriptExecutor js;
        private By Measures = By.CssSelector("#filter-size > div:nth-child(1) > div > div > div > a"); //I know it's bad practice, but couldn't find another good selector to use in this situation :(
        private By ShowAllMeasures = By.CssSelector("#filter-size > div:nth-child(1) > div > span"); //same here :(
        private By Brands = By.CssSelector("#filter-manufacturer > div > div.AdvancedFeatureContainer_itemsContainer_Jd_G7 > div > a");
        private By ShowAllBrands = By.CssSelector("#filter-manufacturer > div > span");
        private By ActiveFilters = By.CssSelector("div.GridFilterSelectedTokens_wrapper_3PVJh > div");
        private By Products = By.CssSelector("div.Product_wrapper_HOAyR.Product_light_BLc7O");

        public ProductsPage(IWebDriver browser)
        {
            driver = browser;
            js = (IJavaScriptExecutor)driver;
        }
        public IList<IWebElement> LstMeasures => driver.FindElements(Measures);
        public IWebElement BtnShowAllMeasures => driver.FindElement(ShowAllMeasures);
        public IList<IWebElement> LstBrands => driver.FindElements(Brands);
        public IWebElement BtnShowAllBrands => driver.FindElement(ShowAllBrands);
        public IList<IWebElement> LstActiveFilters => driver.FindElements(ActiveFilters);
        public IList<IWebElement> LstProducts => driver.FindElements(Products);


        public void SelectSize(string size)
        {
            var regex = new Regex($@"^{size}\s\(\d+\)");

            WaitHelpers.WaitElementToBeVisible(driver, ShowAllMeasures);
            BtnShowAllMeasures.Click();

            var selectedSize = LstMeasures.FirstOrDefault(item => regex.IsMatch(item.Text));
            js.ExecuteScript("arguments[0].scrollIntoView();", selectedSize);

            selectedSize.Click();
        }

        public void SelectBrand(string brand)
        {
            var regex = new Regex($@"^{brand}\s\(\d+\)");

            WaitHelpers.WaitElementToBeVisible(driver, ShowAllBrands);
            BtnShowAllBrands.Click();

            //WaitHelpers.FluentWaitForElement(driver, Brands);
            //WaitHelpers.WaitElementToBeInvisible(driver, ShowAllBrands);
            Thread.Sleep(1000); // Without Thread.Sleep() doesn't work
                                // Also tried with Fluent and Explicit Waits...
                                // Getting StaleElementReferenceException
                                // Sometimes test fails even with Thread.Sleep(), because of this exception

            var selectedBrand = LstBrands.FirstOrDefault(item => regex.IsMatch(item.Text));
            js.ExecuteScript("arguments[0].scrollIntoView();", selectedBrand);

            selectedBrand.Click();
        }

        public void AddFilters(FiltersBO filters)
        {
            SelectSize(filters.Size);
            SelectBrand(filters.Brand);
        }

        public void SelectProductByName(FiltersBO filters)
        {
            Thread.Sleep(1000); // The same situation as in SelectBrand method, without Thread.Sleep() doesn't work
                                // Getting StaleElementReferenceException
                                // Sometimes test fails even with Thread.Sleep(), because of this exception

            var selectedProduct = LstProducts.Where(item => item.Text.ToUpper().Contains(filters.Name))
                .FirstOrDefault();

            var wishlistSelectedProduct = selectedProduct.FindElement(By.CssSelector("button[aria-label='Add to customer list']"));
            js.ExecuteScript("arguments[0].scrollIntoView();", wishlistSelectedProduct);

            wishlistSelectedProduct.Click();
        }

        public void AddProductToWishlist(FiltersBO filters)
        {
            AddFilters(filters);
            SelectProductByName(filters);
        }
    }
}
