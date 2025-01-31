using System.Reflection.PortableExecutable;

public class Analysis
{
    private static char[] chars = "1234567890()[]{}!?@#$%^&*(=-+_~`/\"';: \t\n™’‘”“".ToCharArray();

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
                foreach (var c in chars)
                {
                    line = line?.Replace(c.ToString(), string.Empty);
                }
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