using NUnitPlaywrightFramework.Libs;
using System.Net;

namespace NUnitPlaywrightFramework.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    internal class SampleAPITests
    {
        private ApiActions FrameworkActions;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            FrameworkActions = new();
        }

        [Test]
        public void VerifyEmailForThirdUser()
        {
            //Arrange
            string endpoint = "/api/users";
            string expectedResult = "eve.holt@reqres.in";

            //Act
            var response = FrameworkActions.Get(endpoint).Result.ResponseBody?["data"]?[3]?["email"]?.ToString();

            //Assert
            Assert.That(response, Is.EqualTo(expectedResult));
        }

        [Test]
        public void VerifySingleUserResponseCode()
        {
            //Arrange
            string endpoint = "/api/users/2";
            var expectedResponseCode = 200;

            //Act
            var responseCode = FrameworkActions.Get(endpoint).Result.ResponseStatusCode;

            //Assert
            Assert.That(responseCode, Is.EqualTo(expectedResponseCode));
        }

        [Test]
        public void VerifySingleUserNotFound()
        {
            //Arrange
            string endpoint = "/api/users/23";
            var expectedResponseCode = 404;

            //Act
            var responseCode = FrameworkActions.Get(endpoint).Result.ResponseStatusCode;

            //Assert
            Assert.That(responseCode, Is.EqualTo(expectedResponseCode));
        }


    }
}
