using AutomationFootshop.Utils;
using OpenQA.Selenium;

namespace AutomationFootshop.PageObjects
{
    public class LoginPage
    {
        private IWebDriver driver;
        private By Email = By.Id("email");
        private By Password = By.Id("password");
        private By Authenticate = By.CssSelector("button[type=submit]");
        private By LoginFailed = By.CssSelector("p._2Rc24J2H3vTXx7NM9pEMPM");

        public LoginPage(IWebDriver browser)
        {
            driver = browser;
        }

        public IWebElement TxtEmail => driver.FindElement(Email);
        public IWebElement TxtPassword => driver.FindElement(Password);
        public IWebElement BtnAuthenticate => driver.FindElement(Authenticate);
        public IWebElement LblLoginFailed => driver.FindElement(LoginFailed);

        public HomePage Login(string email, string password)
        {
            TxtEmail.SendKeys(email);
            TxtPassword.SendKeys(password);
            BtnAuthenticate.Click();

            return new HomePage(driver);
        }

        public string GetLoginFailedText()
        {
            WaitHelpers.WaitElementToBeVisible(driver, LoginFailed);

            return LblLoginFailed.Text;
        }
    }
}
