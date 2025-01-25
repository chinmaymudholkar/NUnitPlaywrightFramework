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

    internal static class EnvironmentVariables
    {
        internal const string UI_BASE_URL = "UI_BASE_URL";
        internal const string API_BASE_URL = "API_BASE_URL";
        internal const string HEADLESS_MODE = "HEADLESS_MODE";
        internal const string CAPTURE_SCREENSHOTS = "CAPTURE_SCREENSHOTS";
        internal const string STANDARD_USERNAME = "STANDARD_USERNAME";
        internal const string STANDARD_PASSWORD = "STANDARD_PASSWORD";
    }
}
