using AutomationFootshop.Controls;
using OpenQA.Selenium;

namespace AutomationFootshop.PageObjects
{
    public class HomePage
    {
        private IWebDriver driver;

        public LoggedInMenuControl loggedInMenuControl => new LoggedInMenuControl(driver);
        public VisitorMenuControl visitorMenuControl => new VisitorMenuControl(driver);

        public HomePage(IWebDriver browser)
        {
            driver = browser;
        }
    }
}
