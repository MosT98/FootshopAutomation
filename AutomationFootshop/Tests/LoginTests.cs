using AutomationFootshop.Controls;
using AutomationFootshop.PageObjects;
using AutomationFootshop.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomationFootshop.Tests
{
    [TestClass]
    public class LoginTests
    {
        private IWebDriver driver;
        private LoginPage loginPage;

        [TestInitialize]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.footshop.ro/ro/");

            var menu = new VisitorMenuControl(driver);

            loginPage = menu.NavigateToLoginPage();
        }

        [TestCleanup]
        public void CleanUp()
        {
            driver.Quit();
        }

        [TestMethod]
        public void Login_Should_Fail_When_CredentialsAreInvalid()
        {
            loginPage.Login("andreihalauca@yahoo.com", "Don'tMindMeJustTesting123");

            Assert.AreEqual(Messages.LoginAuthenticationError, loginPage.GetLoginFailedText());
        }

        [TestMethod]
        public void Login_Should_Work_When_CredentialsAreValid()
        {
            var homePage = loginPage.Login("testare@test.com", "ParolaComplicata123");
            var dashboardPage = homePage.loggedInMenuControl.NavigateToDashboardPage();

            Assert.AreEqual(Messages.FullNameAccount, dashboardPage.LblUsername.Text);
            Assert.AreEqual(Messages.DashboardWelcomeMessage, dashboardPage.LblMessage.Text);
        }
    }
}
