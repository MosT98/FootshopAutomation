using AutomationFootshop.Controls;
using AutomationFootshop.PageObjects.Dashboard;
using AutomationFootshop.PageObjects.Dashboard.InputData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomationFootshop.Tests
{
    [TestClass]
    public class PersonalDataTests
    {
        private IWebDriver driver;
        private PersonalDataPage personalDataPage;
        private PersonalDataBO personalDataBO = new PersonalDataBO();

        [TestInitialize]
        public void Initialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://www.footshop.ro/ro/");

            var menu = new VisitorMenuControl(driver);

            var loginPage = menu.NavigateToLoginPage();

            var homePage = loginPage.Login("testare@test.com", "ParolaComplicata123");
            homePage.loggedInMenuControl.AcceptCookies();

            var dashboardPage = homePage.loggedInMenuControl.NavigateToDashboardPage();

            personalDataPage = dashboardPage.NavigateToPersonalDataPage();
        }

        [TestCleanup]
        public void CleanUp()
        {
            driver.Quit();
        }

        [TestMethod]
        public void Email_Should_BeTheSame_As_TheLoginOne()
        {
            Assert.AreEqual(personalDataBO.Email, personalDataPage.TxtEmail.GetAttribute("value"));        
        }

        [TestMethod]
        public void SaveDataButton_Should_UpdateAccountData_When_NewDataIsAdded()
        {
            var pdataBO = new PersonalDataBO()
            {
                FirstName = "FirstName",
                LastName = "LastName"
            };

            personalDataPage.UpdateData(pdataBO);

            Assert.AreEqual(pdataBO.FirstName, personalDataPage.TxtFirstName.GetAttribute("value"));
            Assert.AreEqual(pdataBO.LastName, personalDataPage.TxtLastName.GetAttribute("value"));

            // for the cleanup ^^
            personalDataPage.UpdateData(personalDataBO);
        }
    }
}
