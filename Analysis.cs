using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

public class Analysis
{
    private static string pattern = "[^a-z,.]"; // "\\W";
    private static Regex re = new(pattern);
    private static Dictionary<char, int> frequency = new();

    public static Task<(string, int, Dictionary<char, int>)>? Read(string path)
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
                if (line?.Length > 0)
                    foreach (var c in line)
                    {
                        if (!frequency.TryGetValue(c, out int value))
                        {
                            value = 0;
                            frequency[c] = value;
                        }
                        frequency[c] = ++value;
                    }
                text += line;
            }
            return (text, count, frequency);
        });
    }

    private static bool NotExists(string path)
    {
        return string.IsNullOrEmpty(path) || !File.Exists(path);
    }
}