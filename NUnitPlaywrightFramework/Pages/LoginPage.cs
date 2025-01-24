using Microsoft.Playwright;
using NUnitPlaywrightFramework.Libs;

namespace NUnitPlaywrightFramework.Pages
{
    public class LoginPage
    {
        private readonly IPage page;
        private readonly UiActions frameworkActions;

        public LoginPage(IPage page)
        {
            this.page = page;
            frameworkActions = new UiActions(page);
        }

        private string TxtUsername => "#user-name";
        private string TxtPassword => "#password";
        private string BtnLogin => "#login-button";
        private string LblError => "[data-test=\"error\"]";

        public async Task GotoAsync()
        {
            await page.GotoAsync(new TestBase().GetEnvVariable(EnvironmentVariables.UI_BASE_URL));
        }

        public void Login(string username, string password)
        {
            frameworkActions.PerformAction(TxtUsername, ObjectActions.Type, username);
            frameworkActions.PerformAction(TxtPassword, ObjectActions.Type, password);
            frameworkActions.PerformAction(BtnLogin, ObjectActions.Click, string.Empty);
        }

        public void VerifyErrorMessage(string expectedErrorMessage)
        {
            frameworkActions.Verify(LblError, ObjectProperties.TEXT, expectedErrorMessage);
        }

        public void VerifyTitle(string expectedTitle)
        {
            frameworkActions.Verify(string.Empty, ObjectProperties.TITLE, expectedTitle);
        }
    }
}
