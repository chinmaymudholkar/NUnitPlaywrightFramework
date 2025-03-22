using Microsoft.Playwright;
namespace NUnitPlaywrightFramework.Libs
{
    public class UiActions
    {
        private readonly IPage _page;
        private int screenshotCounter;

        public UiActions(IPage page)
        {
            _page = page;
            screenshotCounter = 1;
        }

        public void Act(ElementActions action, string selector, string value)
        {
            TakeScreenshot($"{selector}-before-{action}");
            switch (action)
            {
                case ElementActions.Click:
                    ClickAsync(selector).Wait();
                    break;
                case ElementActions.Type:
                    TypeAsync(selector, value).Wait();
                    break;
                case ElementActions.CheckboxCheck:
                    CheckAsync(selector).Wait();
                    break;
                case ElementActions.CheckboxUncheck:
                    UncheckAsync(selector).Wait();
                    break;
                case ElementActions.RadioButtonSelect:
                    RadioButtonSelectAsync(selector).Wait();
                    break;
                case ElementActions.ListSelect:
                    ListSelectAsync(selector, value).Wait();
                    break;
                default:
                    WaitForSelectorAsync(selector).Wait();
                    break;
            }
            TakeScreenshot($"{selector}-after-{action}");
        }

        public void Verify(ElementProperties property, string selector, string expectedValue)
        {
            string actualValue = GetElementProperty(property, selector);
            switch (property)
            {
                case ElementProperties.TEXT:
                    Assert.That(actualValue, Is.EqualTo(expectedValue));
                    break;
                case ElementProperties.TITLE:
                    Assert.That(actualValue, Is.EqualTo(expectedValue));
                    break;
                case ElementProperties.URL:
                case ElementProperties.HREF:
                    Assert.That(actualValue, Is.EqualTo(expectedValue));
                    break;
                case ElementProperties.CHECKED:
                    Assert.That(actualValue.ToLower(), Is.EqualTo("true"));
                    break;
                case ElementProperties.UNCHECKED:
                    Assert.That(actualValue.ToLower(), Is.EqualTo("false"));
                    break;
                case ElementProperties.SELECTED:
                    Assert.That(actualValue.ToLower(), Is.EqualTo("true"));
                    break;
                case ElementProperties.UNSELECTED:
                    Assert.That(actualValue.ToLower(), Is.EqualTo("false"));
                    break;
            }
        }

        public string GetElementProperty(ElementProperties property, string selector)
        {
            string actualValue = string.Empty;
            switch (property)
            {
                case ElementProperties.TEXT:
                    actualValue = GetTextContentAsync(selector).Result;
                    break;
                case ElementProperties.TITLE:
                    actualValue = GetTitleAsync().Result;
                    break;
                case ElementProperties.URL:
                case ElementProperties.HREF:
                    actualValue = GetAttributeAsync(selector, "href").Result;
                    break;
                case ElementProperties.CHECKED:
                case ElementProperties.UNCHECKED:
                    actualValue = GetAttributeAsync(selector, "checked").Result;
                    break;
                case ElementProperties.SELECTED:
                case ElementProperties.UNSELECTED:
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

        private void TakeScreenshot(string file_name)
        {
            
            if (bool.Parse(new UIBase().GetEnvVariable(EnvironmentVariables.CAPTURE_SCREENSHOTS)))
            {
                string? base_path = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.FullName;
                base_path = (base_path == null) ? AppDomain.CurrentDomain.BaseDirectory : base_path;
                string screenshot_path = Path.Combine(base_path, "Screenshots", $"{screenshotCounter.ToString("D4")}-{Wrappers.GetCurrentDateTime()}-{Wrappers.CleanString(file_name)}.png");
                _page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshot_path }).Wait();
                screenshotCounter++;
            }
        }
    }
}

