using NUnit.Framework.Internal;
using NUnitPlaywrightFramework.Libs;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            Assert.That(await getAttributes.GetTitle(), Is.EqualTo(expectedTitle));
        }

        [Test]
        public async Task LoginWithValidCredentials()
        {
            //Todo: Move to github secrets
            string username = "standard_user";
            string password = "secret_sauce";

            string expectedHeading = "Products";

            frameworkActions.PerformAction(Constants.Type, "#user-name", username);
            frameworkActions.PerformAction(Constants.Type, "#password", password);
            frameworkActions.PerformAction(Constants.Click, "#login-button", null);
            string actualHeading = await getAttributes.GetTextContent("[data-test=\"title\"]");
            Assert.That(actualHeading, Is.EqualTo(expectedHeading));

        }
    }
}
