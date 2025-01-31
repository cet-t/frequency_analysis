var path = "eng";

var _text = "";
foreach (var file in Directory.GetFiles(path))
{
    var text = await Analysis.Read(file)!;
    _text += text.Item1;
    // File.WriteAllText(text.Item2 + ".." + Guid.NewGuid().ToString() + ".txt", text.Item1);
}

File.WriteAllText($"{_text.Length}.{Guid.NewGuid()}", _text);