using Microsoft.Playwright;
using DotNetEnv;

namespace NUnitPlaywrightFramework.Libs
{
    public class UIBase
    {
        protected IPlaywright _playwright { get; private set; }
        protected IBrowser _browser { get; private set; }
        protected IBrowserContext _browserContext { get; private set; }
        protected IPage _page { get; private set; }

        [OneTimeSetUp]
        public async Task Setup()
        {
            Env.Load();
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = bool.Parse(GetEnvVariable("HEADLESS")), SlowMo=500 });
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

        public string GetEnvVariable(string variableName)
        {
            string? _val = Environment.GetEnvironmentVariable(variableName);
            return _val ?? string.Empty;
        }
    }
}
