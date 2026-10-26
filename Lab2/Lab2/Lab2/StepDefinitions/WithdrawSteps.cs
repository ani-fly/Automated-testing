using BankingTests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;

namespace BankingTests.StepDefinitions
{
    [Binding]
    public class WithdrawSteps
    {
        private readonly LoginPage _loginPage;
        private readonly CustomerSelectionPage _customerSelectionPage;
        private readonly AccountPage _accountPage;
        private int _balanceBefore;

        public WithdrawSteps(IWebDriver driver)
        {
            _loginPage = new LoginPage(driver);
            _customerSelectionPage = new CustomerSelectionPage(driver);
            _accountPage = new AccountPage(driver);
        }

        [Given(@"я відкрив сторінку банку")]
        public void GivenЯВідкривСторінкуБанку() => _loginPage.Open();

        [Given(@"я увійшов як клієнт ""(.*)""")]
        public void GivenЯУвійшовЯкКлієнт(string name)
        {
            _loginPage.ClickCustomerLogin();
            _customerSelectionPage.LoginAs(name);
        }

        [Given(@"я обрав рахунок ""(.*)""")]
        public void GivenЯОбрав(string account) => _accountPage.SelectAccount(account);

        [Given(@"на моєму рахунку є (.*) коштів після поповнення")]
        public void GivenНаМоємуРахункуЄ(int amount)
        {
            _accountPage.Deposit(amount);
            Assert.That(_accountPage.GetMessage(), Is.EqualTo("Deposit Successful"));
        }

        [When(@"я знімаю з рахунку (.*)")]
        public void WhenЯЗнімаю(int amount)
        {
            _balanceBefore = _accountPage.GetBalance();
            _accountPage.Withdraw(amount);
        }

        [Then(@"баланс зменшився на (.*)")]
        public void ThenБалансЗменшився(int amount) =>
            Assert.That(_accountPage.GetBalance(), Is.EqualTo(_balanceBefore - amount));

        [Then(@"я бачу повідомлення ""(.*)""")]
        public void ThenЯБачуПовідомлення(string expected) =>
            Assert.That(_accountPage.WaitForMessage(expected), Is.EqualTo(expected));
    }
}