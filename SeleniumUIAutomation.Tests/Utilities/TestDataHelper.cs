using System.Text.Json;

namespace SeleniumUIAutomation.Tests.Utilities
{
    public static class TestDataHelper
    {
        public static T LoadTestData<T>(string fileName) where T : new()
        {
            string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)!.Parent!.Parent!.Parent!.FullName;
            string filePath = Path.Combine(projectDirectory, "TestData", fileName);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Test data file not found: {filePath}");
            }

            string jsonContent = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<T>(jsonContent) ?? new T();
        }

        public static string GenerateRandomEmail()
        {
            return $"test_{Guid.NewGuid():N}@example.com";
        }

        public static string GenerateRandomString(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int GenerateRandomNumber(int min = 1, int max = 1000)
        {
            return new Random().Next(min, max);
        }
    }
}
