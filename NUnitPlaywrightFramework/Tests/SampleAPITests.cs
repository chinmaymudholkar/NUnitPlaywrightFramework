using NUnitPlaywrightFramework.Libs;
using System.Net;

namespace NUnitPlaywrightFramework.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    internal class SampleAPITests : ApiBase
    {
        protected ApiActions frameworkActions;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            ApiBaseSetup();
            frameworkActions = new();
        }

        [Test]
        public void VerifySingleUserResponseCode()
        {
            //Arrange
            var expectedResponseCode = HttpStatusCode.OK;
            string endpoint = "/api/users/2";

            //Act
            var responseCode = frameworkActions.GetAsync(endpoint).Result.ResponseStatusCode;

            //Assert
            Assert.That(responseCode, Is.EqualTo(expectedResponseCode));
        }

        [Test]
        public void VerifyEmailForThirdUser()
        {
            //Arrange
            string expectedResult = "eve.holt@reqres.in";
            string endpoint = "/api/users";

            //Act
            var response = frameworkActions.GetAsync(endpoint).Result.ResponseBody?["data"]?[3]?["email"]?.ToString();

            //Assert
            Assert.That(response, Is.EqualTo(expectedResult));
        }
    }
}
