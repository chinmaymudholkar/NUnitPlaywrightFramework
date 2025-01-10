using NUnit.Framework.Internal;
using NUnitPlaywrightFramework.Libs;

namespace NUnitPlaywrightFramework.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class Tests : BaseTest
    {
        FrameworkActions frameworkActions;
        GetAttributes getAttributes;

        string TxtUsername;
        string TxtPassword;
        string BtnLogin;
        string LblError;
        string LblHeading;

        [SetUp]
        public async Task ClassSetup()
        {
            await Setup();
            frameworkActions = new FrameworkActions(_page);
            getAttributes = new GetAttributes(_page);
            TxtUsername = "#user-name";
            TxtPassword = "#password";
            BtnLogin = "#login-button";
            LblError = "[data-test=\"error\"]";
            LblHeading = "[data-test=\"title\"]";
            await _page.GotoAsync("https://www.saucedemo.com/");
        }

        [Test]
        public async Task HomepageHasSwagLabsInTitle()
        {
            string expectedTitle = "Swag Labs";
            Assert.That(await getAttributes.GetTitleAsync(), Is.EqualTo(expectedTitle));
        }

        [Test]
        [TestCase("invalid_user1", "invalid_pass1", "Epic sadface: Username and password do not match any user in this service")]
        [TestCase("", "invalid_pass2", "Epic sadface: Username is required")]
        [TestCase("invalid_user3", "", "Epic sadface: Password is required")]
        [TestCase("", "", "Epic sadface: Username is required")]
        public async Task LoginWithInvalidCredentials(string username, string password, string errorMessage)
        {
            frameworkActions.PerformAction(TxtUsername, ActionTypes.Type, username);
            frameworkActions.PerformAction(TxtPassword, ActionTypes.Type, password);
            frameworkActions.PerformAction(BtnLogin, ActionTypes.Click, "");

            string actualErrorMessage = await getAttributes.GetTextContentAsync(LblError);
            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        }

        [Test]
        public async Task LoginWithValidCredentials()
        {
            string expectedHeading = "Products";

            frameworkActions.PerformAction(TxtUsername, ActionTypes.Type, GetEnvVariable("USERNAME"));
            frameworkActions.PerformAction(TxtPassword, ActionTypes.Type, GetEnvVariable("PASSWORD"));
            frameworkActions.PerformAction(BtnLogin, ActionTypes.Click, "");
            string actualHeading = await getAttributes.GetTextContentAsync(LblHeading);
            Assert.That(actualHeading, Is.EqualTo(expectedHeading));
        }
    }
}
