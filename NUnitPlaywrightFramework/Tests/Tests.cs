using NUnit.Framework.Internal;
using NUnitPlaywrightFramework.Libs;

namespace NUnitPlaywrightFramework.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class Tests : BaseTest
    {
        FrameworkActions frameworkActions;

        private string TxtUsername;
        private string TxtPassword;
        private string BtnLogin;
        private string LblError;
        private string LblHeading;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            await Setup();
            frameworkActions = new(_page);
            TxtUsername = "#user-name";
            TxtPassword = "#password";
            BtnLogin = "#login-button";
            LblError = "[data-test=\"error\"]";
            LblHeading = "[data-test=\"title\"]";
        }

        [SetUp]
        public async Task ClassSetup()
        {
            await _page.GotoAsync("https://www.saucedemo.com/");
        }

        [Test]
        public void HomepageHasSwagLabsInTitle()
        {
            string expectedTitle = "Swag Labs";
            frameworkActions.Verify(string.Empty, ObjectProperties.TITLE, expectedTitle);
        }

        [Test]
        [TestCase("invalid_user1", "invalid_pass1", "Epic sadface: Username and password do not match any user in this service")]
        [TestCase("", "invalid_pass2", "Epic sadface: Username is required")]
        [TestCase("invalid_user3", "", "Epic sadface: Password is required")]
        [TestCase("", "", "Epic sadface: Username is required")]
        public void LoginWithInvalidCredentials(string username, string password, string expectedErrorMessage)
        {
            frameworkActions.PerformAction(TxtUsername, ObjectActions.Type, username);
            frameworkActions.PerformAction(TxtPassword, ObjectActions.Type, password);
            frameworkActions.PerformAction(BtnLogin, ObjectActions.Click, string.Empty);
            frameworkActions.Verify(LblError, ObjectProperties.TEXT, expectedErrorMessage);
        }

        [Test]
        public void LoginWithValidCredentials()
        {
            string expectedHeading = "Products";

            frameworkActions.PerformAction(TxtUsername, ObjectActions.Type, GetEnvVariable("USERNAME"));
            frameworkActions.PerformAction(TxtPassword, ObjectActions.Type, GetEnvVariable("PASSWORD"));
            frameworkActions.PerformAction(BtnLogin, ObjectActions.Click, string.Empty);
            frameworkActions.Verify(LblHeading, ObjectProperties.TEXT, expectedHeading);
        }
    }
}
