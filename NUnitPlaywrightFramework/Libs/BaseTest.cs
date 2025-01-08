using Microsoft.Playwright;
using dotenv.net;

namespace NUnitPlaywrightFramework.Libs
{
    public class BaseTest
    {
        protected IPlaywright _playwright { get; private set; }
        protected IBrowser _browser { get; private set; }
        protected IBrowserContext _browserContext { get; private set; }
        protected IPage _page { get; private set; }
        protected IDictionary<string, string> envVars { get; private set; }

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            try
            {
                envVars = DotEnv.Read();
                string username = envVars["USERNAME"];
                string password = envVars["PASSWORD"];
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            _playwright = await Playwright.CreateAsync();
        }

        [SetUp]
        public async Task Setup()
        {   
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
            _browserContext = await _browser.NewContextAsync();
            _page = await _browserContext.NewPageAsync();
        }

        [TearDown]
        public async Task Teardown()
        {
            await _page.CloseAsync();
            await _browserContext.CloseAsync();
            await _browser.CloseAsync();
        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            _playwright.Dispose();
        }
    }
}
