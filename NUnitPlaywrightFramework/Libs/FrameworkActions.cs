using Microsoft.Playwright;
namespace NUnitPlaywrightFramework.Libs
{
    public class FrameworkActions
    {
        private readonly IPage _page;
        private readonly Wrappers wrappers;

        public FrameworkActions(IPage page)
        {
            _page = page;
            wrappers = new();
        }


        public void PerformAction(string selector, ActionTypes action, string value)
        {
            TakeScreenshot($"{selector}-before-{action}").Wait();
            switch (action)
            {
                case ActionTypes.Click:
                    ClickAsync(selector).Wait();
                    break;
                case ActionTypes.Type:
                    TypeAsync(selector, value).Wait();
                    break;
                case ActionTypes.WaitForSelector:
                    WaitForSelectorAsync(selector).Wait();
                    break;
                case ActionTypes.GetTextContent:
                    GetTextContentAsync(selector).Wait();
                    break;
                case ActionTypes.GetAttribute:
                    GetAttributeAsync(selector, value).Wait();
                    break;
                case ActionTypes.GetTitle:
                    GetTitleAsync().Wait();
                    break;
            }
            TakeScreenshot($"{selector}-after-{action}").Wait();
        }

        public void Verify(string selector, ActionTypes property, string expectedValue)
        {
            string actualValue = string.Empty;
            switch (property)
            {
                case ActionTypes.GetTextContent:
                    actualValue = GetTextContentAsync(selector).Result;
                    Assert.That(actualValue, Is.EqualTo(expectedValue));
                    break;
                case ActionTypes.GetAttribute:
                    actualValue = GetAttributeAsync(selector, nameof(property)).Result;
                    Assert.That(actualValue, Is.EqualTo(expectedValue));
                    break;
                case ActionTypes.GetTitle:
                    actualValue = GetTitleAsync().Result;
                    Assert.That(actualValue, Is.EqualTo(expectedValue));
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
            string? _val = await _page.TextContentAsync(selector);
            return _val ?? string.Empty;
        }

        private async Task<string> GetAttributeAsync(string selector, string attribute)
        {
            string? _val = await _page.GetAttributeAsync(selector, attribute);
            return _val ?? string.Empty;
        }

        private async Task<string> GetTitleAsync()
        {
            return await _page.TitleAsync();
        }

        private async Task TakeScreenshot(string file_name)
        {
            string? base_path = string.Empty;
            base_path = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
            if (base_path == null)
            {
                base_path = AppDomain.CurrentDomain.BaseDirectory;
            }
            string screenshot_path = Path.Combine(base_path, "Screenshots", $"{wrappers.GetCurrentDateTime()}-{wrappers.CleanString(file_name)}.png");
            var options = new PageScreenshotOptions { Path = screenshot_path };
            await _page.ScreenshotAsync(options);
        }
    }
}

