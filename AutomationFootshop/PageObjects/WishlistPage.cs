using AutomationFootshop.PageObjects.Products;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace AutomationFootshop.PageObjects
{
    public class WishlistPage
    {
        private IWebDriver driver;
        private By EmptyWishlist = By.CssSelector("div.EmptyList_content_1V6-Z > h1");
        private By WishlistProducts = By.CssSelector("div.ProductList_items_3ISO3 div.ProductList_item_22P1u");
        private By RemoveButton = By.CssSelector("div.ProductCustomerListsButton_buttonWrapper_1N-1v > button");
        private By ProductName = By.CssSelector("div.Product_info_3GqtM > div.Product_inner_n3lJC > a > h4");
        private By ProductPage = By.CssSelector("a.Product_imageLink_X3VQX");


        public WishlistPage(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement LblEmptyWishlist => driver.FindElement(EmptyWishlist);
        public IList<IWebElement> LstWishlistProducts => driver.FindElements(WishlistProducts);

        public void RemoveWishlistProducts()
        {
            if (LstWishlistProducts.Any())
            {
                foreach (var item in LstWishlistProducts)
                {
                    item.FindElement(RemoveButton).Click();
                }
            }
        }

        public string GetProductName()
        {
            return LstWishlistProducts.FirstOrDefault()
                .FindElement(ProductName)
                .Text;
        }

        public IndividualProductPage NavigateToIndividualProductPage()
        {
           if (LstWishlistProducts.Any())
            {
                LstWishlistProducts.FirstOrDefault()
                    .FindElement(ProductPage)
                    .Click();
            }

            return new IndividualProductPage(driver);
        } 
    }
}
