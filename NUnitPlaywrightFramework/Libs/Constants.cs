namespace NUnitPlaywrightFramework.Libs
{
    public enum ObjectActions
    {
        Click,
        Type,
        WaitForSelector,
        CheckboxCheck,
        CheckboxUncheck,
        RadioButtonSelect,
        ListSelect
    }

    public enum ObjectProperties
    {
        TEXT,
        TITLE,
        URL,
        CHECKED,
        UNCHECKED,
        SELECTED,
        UNSELECTED,
        HREF
    }

    public enum BrowserType
    {
        Chromium,
        Firefox,
        Webkit
    }

    public class EnvironmentVariables
    {
        public const string UI_BASE_URL = "UI_BASE_URL";
        public const string API_BASE_URL = "API_BASE_URL";
        public const string HEADLESS_MODE = "HEADLESS_MODE";
        public const string CAPTURE_SCREENSHOTS = "CAPTURE_SCREENSHOTS";
        public const string STANDARD_USERNAME = "STANDARD_USERNAME";
        public const string STANDARD_PASSWORD = "STANDARD_PASSWORD";
    }
}
