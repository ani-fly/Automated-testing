using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace BankingTests.Pages
{
    public class CustomerSelectionPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public CustomerSelectionPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private IWebElement UserSelect =>
            _wait.Until(d => d.FindElement(By.Id("userSelect")));

        private IWebElement LoginButton =>
            _wait.Until(d => d.FindElement(By.CssSelector("form button[type='submit']")));

        public void SelectCustomer(string customerName)
        {
            new SelectElement(UserSelect).SelectByText(customerName);
        }

        public void ClickLogin() => LoginButton.Click();

        public void LoginAs(string customerName)
        {
            SelectCustomer(customerName);
            ClickLogin();
        }
    }
}