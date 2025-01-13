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

        public void PerformAction(string selector, ObjectActions action, string value)
        {
            TakeScreenshot($"{selector}-before-{action}").Wait();
            switch (action)
            {
                case ObjectActions.Click:
                    ClickAsync(selector).Wait();
                    break;
                case ObjectActions.Type:
                    TypeAsync(selector, value).Wait();
                    break;
                case ObjectActions.CheckboxCheck:
                    CheckAsync(selector).Wait();
                    break;
                case ObjectActions.CheckboxUncheck:
                    UncheckAsync(selector).Wait();
                    break;
                case ObjectActions.RadioButtonSelect:
                    RadioButtonSelectAsync(selector).Wait();
                    break;
                case ObjectActions.ListSelect:
                    ListSelectAsync(selector, value).Wait();
                    break;
                default:
                    WaitForSelectorAsync(selector).Wait();
                    break;
            }
            TakeScreenshot($"{selector}-after-{action}").Wait();
        }

        public void Verify(string selector, ObjectProperties property, string expectedValue)
        {
            string actualValue = GetObjectProperty(selector, property);
            switch (property)
            {
                case ObjectProperties.TEXT:
                    Assert.That(actualValue, Is.EqualTo(expectedValue));
                    break;
                case ObjectProperties.TITLE:
                    Assert.That(actualValue, Is.EqualTo(expectedValue));
                    break;
                case ObjectProperties.URL:
                case ObjectProperties.HREF:
                    Assert.That(actualValue, Is.EqualTo(expectedValue));
                    break;
                case ObjectProperties.CHECKED:
                    Assert.That(actualValue.ToLower(), Is.EqualTo("true"));
                    break;
                case ObjectProperties.UNCHECKED:
                    Assert.That(actualValue.ToLower(), Is.EqualTo("false"));
                    break;
                case ObjectProperties.SELECTED:
                    Assert.That(actualValue.ToLower(), Is.EqualTo("true"));
                    break;
                case ObjectProperties.UNSELECTED:
                    Assert.That(actualValue.ToLower(), Is.EqualTo("false"));
                    break;
            }
        }

        public string GetObjectProperty(string selector, ObjectProperties property)
        {
            string actualValue = string.Empty;
            switch (property)
            {
                case ObjectProperties.TEXT:
                    actualValue = GetTextContentAsync(selector).Result;
                    break;
                case ObjectProperties.TITLE:
                    actualValue = GetTitleAsync().Result;
                    break;
                case ObjectProperties.URL:
                case ObjectProperties.HREF:
                    actualValue = GetAttributeAsync(selector, "href").Result;
                    break;
                case ObjectProperties.CHECKED:
                case ObjectProperties.UNCHECKED:
                    actualValue = GetAttributeAsync(selector, "checked").Result;
                    break;
                case ObjectProperties.SELECTED:
                case ObjectProperties.UNSELECTED:
                    actualValue = GetAttributeAsync(selector, "selected").Result;
                    break;
            }
            return actualValue;
        }


        private async Task ListSelectAsync(string selector, string value)
        {
            await _page.SelectOptionAsync(selector, value);
        }

        private async Task RadioButtonSelectAsync(string selector)
        {
            await _page.ClickAsync(selector);
        }

        private async Task CheckAsync(string selector)
        {
            await _page.SetCheckedAsync(selector, true);
        }

        private async Task UncheckAsync(string selector)
        {
            await _page.SetCheckedAsync(selector, false);
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
            string? base_path = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
            base_path = (base_path == null) ? AppDomain.CurrentDomain.BaseDirectory : base_path;
            string screenshot_path = Path.Combine(base_path, "Screenshots", $"{wrappers.GetCurrentDateTime()}-{wrappers.CleanString(file_name)}.png");
            _ = await _page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshot_path });
        }
    }
}

