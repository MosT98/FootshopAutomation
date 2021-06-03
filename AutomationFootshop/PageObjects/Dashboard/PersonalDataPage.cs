using AutomationFootshop.PageObjects.Dashboard.InputData;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace AutomationFootshop.PageObjects.Dashboard
{
    public class PersonalDataPage
    {
        private IWebDriver driver;
        private Actions actions;
        private By FirstName = By.Id("name.firstName");
        private By LastName = By.Id("name.lastName");
        private By Email = By.Id("email");
        private By Save = By.CssSelector("button[type=submit]");


        public PersonalDataPage(IWebDriver browser)
        {
            driver = browser;
            actions = new Actions(driver);
        }

        public IWebElement TxtFirstName => driver.FindElement(FirstName);
        public IWebElement TxtLastName => driver.FindElement(LastName);
        public IWebElement TxtEmail => driver.FindElement(Email);
        public IWebElement BtnSave => driver.FindElement(Save);

        public void UpdateData(PersonalDataBO personalDataBO)
        {
            actions.DoubleClick(TxtFirstName).Perform();
            TxtFirstName.SendKeys(personalDataBO.FirstName);

            actions.DoubleClick(TxtLastName).Perform();
            TxtLastName.SendKeys(personalDataBO.LastName);

            BtnSave.Click();
        }
    }
}
