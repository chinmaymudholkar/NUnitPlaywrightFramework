using NUnit.Framework;
using System.Text.RegularExpressions;
using Microsoft.Playwright;
using NUnitPlaywrightFramework.Libs;

namespace NUnitPlaywrightFramework
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class Tests : BaseTest
    {
        FrameworkActions frameworkActions;
        [SetUp]
        public async Task Setup()
        {
            await base.Setup();
            frameworkActions = new FrameworkActions(_page);
            await _page.GotoAsync("https://parabank.parasoft.com/parabank/index.htm");
        }


        [Test]
        public async Task HomepageHasParabankInTitle()
        {
            string ExpectedTitle = "ParaBank | Welcome | Online Banking";
            string ActualTitle = await frameworkActions.GetTitle();
            Assert.That(ActualTitle, Is.EqualTo(ExpectedTitle));
        }
    }
}
