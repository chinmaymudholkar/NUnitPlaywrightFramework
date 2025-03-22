using Microsoft.Playwright;

namespace NUnitPlaywrightFramework.Libs
{
    internal class ApiBase : TestBase
    {
        protected IAPIRequestContext apiContext;
        [OneTimeSetUp]
        public async Task ApiBaseSetup()
        {
            await BaseSetup();
            apiContext = await _playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions { BaseURL = GetEnvVariable(EnvironmentVariables.API_BASE_URL).ToString() });
        }
    }
}
