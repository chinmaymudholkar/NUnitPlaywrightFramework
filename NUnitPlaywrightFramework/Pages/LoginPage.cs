using Microsoft.Playwright;
using NUnitPlaywrightFramework.Libs;

namespace NUnitPlaywrightFramework.Pages
{
    public class LoginPage
    {
        private readonly IPage _page;
        private readonly FrameworkActions _frameworkActions;

        public LoginPage(IPage page)
        {
            _page = page;
            _frameworkActions = new FrameworkActions(page);
        }

        private string TxtUsername => "#user-name";
        private string TxtPassword => "#password";
        private string BtnLogin => "#login-button";
        private string LblError => "[data-test=\"error\"]";

        public async Task GotoAsync()
        {
            await _page.GotoAsync("https://www.saucedemo.com/");
        }

        public void Login(string username, string password)
        {
            _frameworkActions.PerformAction(TxtUsername, ObjectActions.Type, username);
            _frameworkActions.PerformAction(TxtPassword, ObjectActions.Type, password);
            _frameworkActions.PerformAction(BtnLogin, ObjectActions.Click, string.Empty);
        }

        public void VerifyErrorMessage(string expectedErrorMessage)
        {
            _frameworkActions.Verify(LblError, ObjectProperties.TEXT, expectedErrorMessage);
        }

        public void VerifyTitle(string expectedTitle)
        {
            _frameworkActions.Verify(string.Empty, ObjectProperties.TITLE, expectedTitle);
        }
    }
}
