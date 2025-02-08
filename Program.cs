var path = "eng";

var _text = "";
foreach (var file in Directory.GetFiles(path))
{
    var text = await Analysis.Read(file)!;
    File.WriteAllText(file.Split('\\')[^1] + "analysis.txt", string.Join("\n", text.Item3.OrderBy(x => x.Value).Select(x => $"{x.Key}: {x.Value}")));
    _text += text.Item1;
}

File.WriteAllText($"{_text.Length}.{Guid.NewGuid()}", _text);