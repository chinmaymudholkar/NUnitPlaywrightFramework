using DotNetEnv;
using Microsoft.Playwright;

namespace NUnitPlaywrightFramework.Libs
{
    public class TestBase
    {
        protected IPlaywright _playwright { get; private set; }

        [OneTimeSetUp]
        public async Task BaseSetup()
        {
            Env.Load();
            _playwright = await Playwright.CreateAsync();
        }

        public string GetEnvVariable(string variableName)
        {
            string? _val = Environment.GetEnvironmentVariable(variableName);
            return _val ?? string.Empty;
        }
    }
}
