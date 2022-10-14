using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
        var mdFile = args.FirstOrDefault();
        if (File.Exists(mdFile) == false)
        {
            Console.WriteLine($"找到{mdFile}md文件");
            return;
        }

        var mdText = File.ReadAllText(mdFile);
        var mdPath = Path.GetDirectoryName(Path.GetFullPath(mdFile));
        var mdEmbedded = Regex.Replace(mdText, @"(?<=!\[.*\]\(\s*)\S+(?=\s*\))", m =>
        {
            if (Uri.TryCreate(m.Value, UriKind.Relative, out _))
            {
                var imgFile = mdPath == null ? m.Value : Path.Combine(mdPath, m.Value);
                if (File.Exists(imgFile) == true)
                {
                    var base64 = Convert.ToBase64String(File.ReadAllBytes(imgFile));
                    return $"data:image/jpeg;base64,{base64}";
                }
            }
            return m.Value;
        });

        var embeddedFile = Path.ChangeExtension(mdFile, ".embedded.md");
        File.WriteAllText(embeddedFile, mdEmbedded);
    }
}