using AutomationFootshop.PageObjects;
using AutomationFootshop.Utils;
using OpenQA.Selenium;

namespace AutomationFootshop.Controls
{
    public class VisitorMenuControl : MenuControl
    {
        private By SignIn = By.CssSelector("div.Ultranav_icons_3ruqi > a.UserIcon_userIcon_3xrLc");
        public VisitorMenuControl(IWebDriver browser) : base(browser)
        {
        }

        public IWebElement BtnSignIn => driver.FindElement(SignIn);

        public LoginPage NavigateToLoginPage()
        {
            WaitHelpers.WaitElementToBeVisible(driver, SignIn);
            BtnSignIn.Click();

            return new LoginPage(driver);
        }
    }
}
