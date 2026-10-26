using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace BankingTests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private const string Url =
            "https://www.globalsqa.com/angularJs-protractor/BankingProject/#/login";

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private IWebElement CustomerLoginButton =>
            _wait.Until(d => d.FindElement(By.CssSelector("button[ng-click='customer()']")));

        public void Open() => _driver.Navigate().GoToUrl(Url);

        public void ClickCustomerLogin() => CustomerLoginButton.Click();
    }
}