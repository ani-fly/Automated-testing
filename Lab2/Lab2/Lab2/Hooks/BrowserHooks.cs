using Reqnroll;
using Reqnroll.BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BankingTests.Hooks
{
    [Binding]
    public class BrowserHooks
    {
        private readonly IObjectContainer _container;

        public BrowserHooks(IObjectContainer container) => _container = container;

        [BeforeScenario]
        public void StartBrowser()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            IWebDriver driver = new ChromeDriver(options);
            _container.RegisterInstanceAs(driver);
        }

        [AfterScenario]
        public void CloseBrowser()
        {
            var driver = _container.Resolve<IWebDriver>();
            driver.Quit();
            driver.Dispose();
        }
    }
}