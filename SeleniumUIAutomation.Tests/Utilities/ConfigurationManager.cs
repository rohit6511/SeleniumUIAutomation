using Microsoft.Extensions.Configuration;

namespace SeleniumUIAutomation.Tests.Utilities
{
    public static class ConfigurationManager
    {
        private static IConfiguration? _configuration;
        private static readonly object _lock = new object();

        public static IConfiguration Configuration
        {
            get
            {
                lock (_lock)
                {
                    if (_configuration == null)
                    {
                        var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                            .AddEnvironmentVariables();

                        _configuration = builder.Build();
                    }
                    return _configuration;
                }
            }
        }

        public static string GetSetting(string key)
        {
            return Configuration[key] ?? string.Empty;
        }

        public static string GetSetting(string section, string key)
        {
            return Configuration.GetSection(section)[key] ?? string.Empty;
        }

        public static T GetSection<T>(string sectionName) where T : new()
        {
            var section = new T();
            Configuration.GetSection(sectionName).Bind(section);
            return section;
        }

        public static string BaseUrl => GetSetting("TestSettings:BaseUrl");
        public static string Browser => GetSetting("TestSettings:Browser");
        public static bool Headless => bool.TryParse(GetSetting("TestSettings:Headless"), out var result) && result;
        public static int ImplicitWaitSeconds => int.TryParse(GetSetting("TestSettings:ImplicitWaitSeconds"), out var result) ? result : 10;
        public static int ExplicitWaitSeconds => int.TryParse(GetSetting("TestSettings:ExplicitWaitSeconds"), out var result) ? result : 15;
    }
}
