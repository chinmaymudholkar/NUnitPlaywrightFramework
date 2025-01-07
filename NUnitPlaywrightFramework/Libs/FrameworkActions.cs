using Microsoft.Playwright;

namespace NUnitPlaywrightFramework.Libs
{
    public class FrameworkActions
    {
        private readonly IPage _page;

        public FrameworkActions(IPage page) => _page = page;

        public void PerformAction(string action, string selector, string? text)
        {
            switch (action)
            {
                case ActionTypes.Click:
                    ClickAsync(selector).Wait();
                    break;
                case ActionTypes.Type:
                    TypeAsync(selector, text).Wait();
                    break;
                case ActionTypes.WaitForSelector:
                    WaitForSelectorAsync(selector).Wait();
                    break;
                case ActionTypes.GetTextContent:
                    GetTextContentAsync(selector).Wait();
                    break;
                case ActionTypes.GetAttribute:
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
    }

    public class GetAttributes
    {
        private readonly IPage _page;

        public GetAttributes(IPage page) => _page = page;

        public async Task<string> GetAttributeAsync(string selector, string attribute)
        {
            return await _page.GetAttributeAsync(selector, attribute);
        }

        public async Task<string> GetTextContentAsync(string selector)
        {
            return await _page.TextContentAsync(selector);
        }

        public async Task<string> GetTitleAsync()
        {
            return await _page.TitleAsync();
        }
    }
}
