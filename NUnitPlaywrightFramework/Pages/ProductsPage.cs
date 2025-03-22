using Microsoft.Playwright;
using NUnitPlaywrightFramework.Libs;

namespace NUnitPlaywrightFramework.Pages
{
    public class ProductsPage
    {
        private readonly IPage page;
        private readonly UiActions FrameworkActions;

        public ProductsPage(IPage page)
        {
            this.page = page;
            FrameworkActions = new UiActions(page);
        }

        private string LblHeader => "[data-test=\"title\"]";

        public void VerifyHeading(string expectedHeading)
        {
            FrameworkActions.Verify(ElementProperties.TEXT, LblHeader, expectedHeading);
        }
    }
}
