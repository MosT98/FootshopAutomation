using OpenQA.Selenium;

namespace AutomationFootshop.PageObjects.Dashboard
{
    public class DashboardPage
    {
        private IWebDriver driver;
        private By Username = By.CssSelector("a[data-test-id='profile-header-username']");
        private By Message = By.CssSelector("div._2SC5eU2RzcmCToIS8e66z0 > p");
        private By PersonalData = By.CssSelector("a[data-test-id='profile-menu-update-profile']");

        public DashboardPage(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement LblUsername => driver.FindElement(Username);
        public IWebElement LblMessage => driver.FindElement(Message);
        public IWebElement BtnPersonalData => driver.FindElement(PersonalData);

        public PersonalDataPage NavigateToPersonalDataPage()
        {
            BtnPersonalData.Click();

            return new PersonalDataPage(driver);
        }
    }
}
