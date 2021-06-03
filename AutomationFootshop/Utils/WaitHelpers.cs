using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace AutomationFootshop.Utils
{
    public static class WaitHelpers
    {
        public static void WaitElementToBeVisible(IWebDriver driver, By by, int time = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
        }

        public static void FluentWaitForElement(IWebDriver driver, IWebElement element, int time = 30)
        {
            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(driver);
            wait.Timeout = TimeSpan.FromSeconds(time);
            wait.PollingInterval = TimeSpan.FromMilliseconds(100);
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            wait.Until(x => element.Displayed && element.Enabled);
        }

        public static void WaitElementToBeInvisible(IWebDriver driver, By by, int time = 30)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(time));

            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(by));
        }
    }
}
