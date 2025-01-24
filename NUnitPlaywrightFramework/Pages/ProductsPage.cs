using Microsoft.Playwright;
using NUnitPlaywrightFramework.Libs;

namespace NUnitPlaywrightFramework.Pages
{
    public class ProductsPage
    {
        private readonly IPage page;
        private readonly UiActions frameworkActions;

        public ProductsPage(IPage page)
        {
            this.page = page;
            frameworkActions = new UiActions(page);
        }

        private string LblHeader => "[data-test=\"title\"]";

        public void VerifyHeading(string expectedHeading)
        {
            frameworkActions.Verify(LblHeader, ObjectProperties.TEXT, expectedHeading);
        }
    }
}
