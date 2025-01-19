using Microsoft.Playwright;
using NUnitPlaywrightFramework.Libs;

namespace NUnitPlaywrightFramework.Pages
{
    public class ProductsPage
    {
        private readonly IPage page;
        private readonly FrameworkActions frameworkActions;

        public ProductsPage(IPage page)
        {
            this.page = page;
            frameworkActions = new FrameworkActions(page);
        }

        private string LblHeading => "[data-test=\"title\"]";

        public void VerifyHeading(string expectedHeading)
        {
            frameworkActions.Verify(LblHeading, ObjectProperties.TEXT, expectedHeading);
        }
    }
}
