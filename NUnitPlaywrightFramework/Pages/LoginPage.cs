using Microsoft.Playwright;
using NUnitPlaywrightFramework.Libs;

namespace NUnitPlaywrightFramework.Pages
{
    public class LoginPage
    {
        private readonly IPage page;
        private readonly UiActions FrameworkActions;

        public LoginPage(IPage page)
        {
            this.page = page;
            FrameworkActions = new UiActions(page);
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
            FrameworkActions.Act(ElementActions.Type, TxtUsername, username);
            FrameworkActions.Act(ElementActions.Type, TxtPassword, password);
            FrameworkActions.Act(ElementActions.Click, BtnLogin, string.Empty);
        }

        public void VerifyErrorMessage(string expectedErrorMessage)
        {
            FrameworkActions.Verify(ElementProperties.TEXT, LblError, expectedErrorMessage);
        }

        public void VerifyTitle(string expectedTitle)
        {
            FrameworkActions.Verify(ElementProperties.TITLE, string.Empty, expectedTitle);
        }
    }
}
