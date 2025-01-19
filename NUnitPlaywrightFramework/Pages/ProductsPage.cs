using Microsoft.Playwright;
using NUnitPlaywrightFramework.Libs;

namespace NUnitPlaywrightFramework.Pages
{
    public class ProductsPage
    {
        private readonly IPage _page;
        private readonly FrameworkActions _frameworkActions;

        public ProductsPage(IPage page)
        {
            _page = page;
            _frameworkActions = new FrameworkActions(page);
        }

        private string LblHeading => "[data-test=\"title\"]";

        public void VerifyHeading(string expectedHeading)
        {
            _frameworkActions.Verify(LblHeading, ObjectProperties.TEXT, expectedHeading);
        }
    }
}
