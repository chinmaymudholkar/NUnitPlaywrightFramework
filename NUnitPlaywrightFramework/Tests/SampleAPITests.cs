using NUnitPlaywrightFramework.Libs;
using System.Net;

namespace NUnitPlaywrightFramework.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class SampleAPITests : APIBase
    {
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Setup();
        }

        [Test]
        public void VerifySingleUserResponseCode()
        {
            var expectedResponseCode = HttpStatusCode.OK;
            string endpoint = "/api/users/2";
            Assert.That(GetAsync(endpoint).Result.ResponseStatusCode, Is.EqualTo(expectedResponseCode));
        }

        [Test]
        public void VerifyEmailForThirdUser()
        {
            string expectedResult = "eve.holt@reqres.in";
            string endpoint = "/api/users";
            Assert.That(GetAsync(endpoint).Result.ResponseBody?["data"]?[3]?["email"]?.ToString(), Is.EqualTo(expectedResult));
        }
    }
}
