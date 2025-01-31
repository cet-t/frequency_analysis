using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

public class Analysis
{
    private static string pattern = "[^0-9a-z,.]"; // "\\W";
    private static Regex re = new(pattern);

    public static Task<(string, int)>? Read(string path)
    {
        if (NotExists(path))
        {
            Console.WriteLine("Invalid file path.");
            return null;
        }

        return Task.Run(async () =>
        {
            int count = 0;
            string text = string.Empty;
            using var reader = new StreamReader(path);
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                line = line?.Trim().ToLower();
                line = re.Replace(line ?? string.Empty, string.Empty);
                count += line?.Length ?? 0;
                text += line;
            }
            return (text, count);
        });
    }

    private static bool NotExists(string path)
    {
        return string.IsNullOrEmpty(path) || !File.Exists(path);
    }
}