using NUnit.Framework;
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace NUnitPlaywrightFramework
{
    public class BaseTest
    {
        protected IPlaywright _playwright { get; private set; }
        protected IBrowser _browser { get; private set; }
        protected IBrowserContext _browserContext { get; private set; }
        protected IPage _page { get; private set; }

        [SetUp]
        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
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
            _playwright.Dispose();
        }
    }
}
