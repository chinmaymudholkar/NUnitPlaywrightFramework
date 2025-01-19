using NUnitPlaywrightFramework.Libs;

namespace NUnitPlaywrightFramework.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class SampleAPITests:BaseTest
    {
        FrameworkActions frameworkActions;
        string baseUrl;

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            await Setup();
            frameworkActions = new(_page);
            baseUrl = "https://reqres.in";
        }

        [Test]
        public void GetUserDetails()
        {
            // Write your test here
        }
    }
}
