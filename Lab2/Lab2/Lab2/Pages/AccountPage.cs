using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BankingTests.Pages
{
    public class AccountPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public AccountPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        private IWebElement AccountSelect =>
            _wait.Until(d => d.FindElement(By.Id("accountSelect")));
        private IWebElement DepositTab =>
            _wait.Until(d => d.FindElement(By.CssSelector("button[ng-click='deposit()']")));
        private IWebElement WithdrawTab =>
            _wait.Until(d => d.FindElement(By.CssSelector("button[ng-click='withdrawl()']")));
        private IWebElement AmountInput =>
            _wait.Until(d =>
            {
                var el = d.FindElement(By.CssSelector("input[ng-model='amount']"));
                return el.Displayed ? el : null;
            });
        private IWebElement SubmitButton =>
            _wait.Until(d => d.FindElement(By.CssSelector("form button[type='submit']")));
        private IWebElement BalanceLabel =>
            _wait.Until(d => d.FindElement(By.XPath("//div[contains(@class,'center')]/strong[2]")));

        public void SelectAccount(string account) =>
            new SelectElement(AccountSelect).SelectByText(account);

        public int GetBalance() => int.Parse(BalanceLabel.Text.Trim());

        public void Deposit(int amount)
        {
            DepositTab.Click();
            WaitForForm("Amount to be Deposited");
            EnterAmountAndSubmit(amount);
        }

        public void Withdraw(int amount)
        {
            WithdrawTab.Click();
            WaitForForm("Amount to be Withdrawn");
            EnterAmountAndSubmit(amount);
        }

        private void WaitForForm(string labelText)
        {
            _wait.Until(d => d.FindElements(
                    By.XPath($"//form//label[contains(text(),'{labelText}')]"))
                .Any(e => e.Displayed));
        }

        private void EnterAmountAndSubmit(int amount)
        {
            AmountInput.Clear();
            AmountInput.SendKeys(amount.ToString());
            SubmitButton.Click();
        }

        public string GetMessage() =>
            _wait.Until(d =>
            {
                var el = d.FindElement(By.CssSelector("span.error"));
                return string.IsNullOrWhiteSpace(el.Text) ? null : el.Text;
            });

        public string WaitForMessage(string expected)
        {
            try
            {
                _wait.Until(d => d.FindElement(By.CssSelector("span.error")).Text.Trim() == expected);
            }
            catch (WebDriverTimeoutException)
            {
            }
            return _driver.FindElement(By.CssSelector("span.error")).Text.Trim();
        }
    }

}