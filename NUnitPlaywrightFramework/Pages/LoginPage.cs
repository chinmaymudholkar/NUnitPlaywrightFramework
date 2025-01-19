using Microsoft.Playwright;
using NUnitPlaywrightFramework.Libs;

namespace NUnitPlaywrightFramework.Pages
{
    public class LoginPage
    {
        private readonly IPage page;
        private readonly FrameworkActions frameworkActions;

        public LoginPage(IPage page)
        {
            this.page = page;
            frameworkActions = new FrameworkActions(page);
        }

        private string TxtUsername => "#user-name";
        private string TxtPassword => "#password";
        private string BtnLogin => "#login-button";
        private string LblError => "[data-test=\"error\"]";

        public async Task GotoAsync()
        {
            await page.GotoAsync("https://www.saucedemo.com/");
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
