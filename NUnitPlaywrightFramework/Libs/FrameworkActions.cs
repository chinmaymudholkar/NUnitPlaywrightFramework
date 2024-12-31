using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnitPlaywrightFramework.Libs;

namespace NUnitPlaywrightFramework.Libs
{
    public class FrameworkActions
    {
        private readonly IPage _page;

        public FrameworkActions(IPage page)
        {
            _page = page;
        }

        public void PerformAction(string action, string selector, string text)
        {
            switch (action)
            {
                case Constants.Click:
                    ClickAsync(selector).Wait();
                    break;
                case Constants.Type:
                    TypeAsync(selector, text).Wait();
                    break;
                case Constants.WaitForSelector:
                    WaitForSelectorAsync(selector).Wait();
                    break;
                case Constants.GetTextContent:
                    GetTextContentAsync(selector).Wait();
                    break;
                case Constants.GetAttribute:
                    GetAttributeAsync(selector, text).Wait();
                    break;
            }
        }

        private async Task ClickAsync(string selector)
        {
            await _page.ClickAsync(selector);
        }

        private async Task TypeAsync(string selector, string text)
        {
            await _page.FillAsync(selector, text);
        }

        private async Task WaitForSelectorAsync(string selector)
        {
            await _page.WaitForSelectorAsync(selector);
        }

        private async Task<string> GetTextContentAsync(string selector)
        {
            return await _page.TextContentAsync(selector);
        }

        private async Task<string> GetAttributeAsync(string selector, string attribute)
        {
            return await _page.GetAttributeAsync(selector, attribute);
        }

        public async Task<string> GetTitle()
        {
            return await _page.TitleAsync();
        }
    }
}
