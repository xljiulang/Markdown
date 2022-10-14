﻿using CommonMark;
using System;
using System.IO;
using System.Linq;
using System.Text;

class Program
{
    private static void Main(string[] args)
    {
        const string templateFile = "Md2Html.html";
        if (File.Exists(templateFile) == false)
        {
            Console.WriteLine($"找到文件{templateFile}");
        }

        var mdFile = args.FirstOrDefault();
        if (File.Exists(mdFile) == false)
        {
            Console.WriteLine($"找到{mdFile}md文件");
            return;
        }

        using var reader = new StreamReader(mdFile);
        var stringBuilder = new StringBuilder();
        using var writer = new StringWriter(stringBuilder);
        CommonMarkConverter.Convert(reader, writer);
        var content = stringBuilder.ToString();

        var template = File.ReadAllText(templateFile);
        var title = Path.GetFileNameWithoutExtension(mdFile);
        var html = template.Replace("@title", title).Replace("@content", content);
        var htmlFile = Path.ChangeExtension(mdFile, ".html");
        File.WriteAllText(htmlFile, html, Encoding.UTF8);
    }
}