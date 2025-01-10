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
            frameworkActions.PerformAction(ActionTypes.Type, TxtUsername, username);
            frameworkActions.PerformAction(ActionTypes.Type, TxtPassword, password);
            frameworkActions.PerformAction(ActionTypes.Click, BtnLogin, null);

            string actualErrorMessage = await getAttributes.GetTextContentAsync("[data-test=\"error\"]");
            Assert.That(actualErrorMessage, Is.EqualTo(errorMessage));
        }

        [Test]
        public async Task LoginWithValidCredentials()
        {
            string expectedHeading = "Products";

            frameworkActions.PerformAction(ActionTypes.Type, TxtUsername, Environment.GetEnvironmentVariable("USERNAME"));
            frameworkActions.PerformAction(ActionTypes.Type, TxtPassword, Environment.GetEnvironmentVariable("PASSWORD"));
            frameworkActions.PerformAction(ActionTypes.Click, BtnLogin, null);
            string actualHeading = await getAttributes.GetTextContentAsync(LblError);
            Assert.That(actualHeading, Is.EqualTo(expectedHeading));
        }
    }
}
