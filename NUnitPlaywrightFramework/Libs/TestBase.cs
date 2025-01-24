using DotNetEnv;

namespace NUnitPlaywrightFramework.Libs
{
    public class TestBase
    {
        [OneTimeSetUp]
        public void BaseSetup()
        {
            Env.Load();
        }

        public string GetEnvVariable(string variableName)
        {
            string? _val = Environment.GetEnvironmentVariable(variableName);
            return _val ?? string.Empty;
        }
    }
}
