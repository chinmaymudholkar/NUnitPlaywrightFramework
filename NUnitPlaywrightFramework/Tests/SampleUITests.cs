using NUnit.Framework;
using NUnit.Framework.Internal;
using NUnitPlaywrightFramework.Libs;
using NUnitPlaywrightFramework.Pages;

namespace NUnitPlaywrightFramework.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class SampleUITests : UIBase
    {
        private LoginPage loginPage;
        private ProductsPage productsPage;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            await Setup();
            loginPage = new LoginPage(_page);
            productsPage = new ProductsPage(_page);
        }

        [SetUp]
        public async Task ClassSetup()
        {
            await loginPage.GotoAsync();
        }

        [Test, Order(1)]
        public void LoginPageHasSwagLabsInTitle()
        {
            string expectedTitle = "Swag Labs";
            loginPage.VerifyTitle(expectedTitle);
        }

        [Test, Order(2)]
        [TestCase("invalid_user1", "invalid_pass1", "Epic sadface: Username and password do not match any user in this service")]
        [TestCase("", "invalid_pass2", "Epic sadface: Username is required")]
        [TestCase("invalid_user3", "", "Epic sadface: Password is required")]
        [TestCase("", "", "Epic sadface: Username is required")]
        public void LoginWithInvalidCredentialsFails(string username, string password, string expectedErrorMessage)
        {
            loginPage.Login(username, password);
            loginPage.VerifyErrorMessage(expectedErrorMessage);
        }

        [Test, Order(3)]
        public void LoginWithValidCredentialsIsSuccessful()
        {
            string expectedHeading = "Products";

            loginPage.Login(GetEnvVariable("USERNAME"), GetEnvVariable("PASSWORD"));
            productsPage.VerifyHeading(expectedHeading);
        }
    }
}
