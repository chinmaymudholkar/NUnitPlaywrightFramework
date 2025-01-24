using Microsoft.Playwright;

namespace NUnitPlaywrightFramework.Libs
{
    public class UIBase : TestBase
    {
        protected IPlaywright _playwright { get; private set; }
        protected IBrowser _browser { get; private set; }
        protected IBrowserContext _browserContext { get; private set; }
        protected IPage _page { get; private set; }

        [OneTimeSetUp]
        public async Task UISetup()
        {
            BaseSetup();
            bool _headless_mode = bool.Parse(GetEnvVariable(EnvironmentVariables.HEADLESS_MODE));
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = _headless_mode, SlowMo=500 });
            _browserContext = await _browser.NewContextAsync();
            _page = await _browserContext.NewPageAsync();
        }

        [OneTimeTearDown]
        public async Task Teardown()
        {
            await _page.CloseAsync();
            await _browserContext.CloseAsync();
            await _browser.CloseAsync();
            _playwright.Dispose();
        }
    }
}
