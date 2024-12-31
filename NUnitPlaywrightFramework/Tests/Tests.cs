using Microsoft.Playwright;
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
        
        [SetUp]
        public async Task ClassSetup()
        {
            await Setup();
            frameworkActions = new FrameworkActions(_page);
            getAttributes = new GetAttributes(_page);
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
        [TestCase("", "invalid_pass2", "Epic sadface: Username and password do not match any user in this service")]
        [TestCase("invalid_user3", "", "Epic sadface: Username and password do not match any user in this service")]
        [TestCase("", "", "Epic sadface: Username and password do not match any user in this service")]
        public async Task LoginWithInvalidCredentials(string username, string password, string errorMessage)
        {
            frameworkActions.PerformAction(Actions.Type, "#user-name", username);
            frameworkActions.PerformAction(Actions.Type, "#password", password);
            frameworkActions.PerformAction(Actions.Click, "#login-button", null);

            string actualErrorMessage = await getAttributes.GetTextContentAsync("[data-test=\"error\"]");
            Assert.That(errorMessage, Is.EqualTo(errorMessage));
        }

        [Test]
        public async Task LoginWithValidCredentials()
        {
            //Todo: Move to github secrets
            string username = "standard_user";
            string password = "secret_sauce";

            string expectedHeading = "Products";

            frameworkActions.PerformAction(Actions.Type, "#user-name", username);
            frameworkActions.PerformAction(Actions.Type, "#password", password);
            frameworkActions.PerformAction(Actions.Click, "#login-button", null);
            string actualHeading = await getAttributes.GetTextContentAsync("[data-test=\"title\"]");
            Assert.That(actualHeading, Is.EqualTo(expectedHeading));
        }
    }
}
