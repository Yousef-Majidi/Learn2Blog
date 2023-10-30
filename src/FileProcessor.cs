// <copyright file="FileProcessor.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Learn2Blog
{
    using System.Text;
    using System.Text.RegularExpressions;

    public partial class FileProcessor
    {
        public static void ProcessFiles(CommandLineOptions options)
        {
            if (File.Exists(options.InputPath))
            {
                string ext = Path.GetExtension(options.InputPath);
                if (ext == ".txt" || ext == ".md")
                {
                    CommandLineUtils.CreateOutputDirectory(options.OutputPath); // Create the output directorya
                    ProcessFile(options.InputPath, options.OutputPath);
                }
                else
                {
                    CommandLineUtils.Logger("The specified input file is not a .txt or .md file");
                }
            }
            else if (Directory.Exists(options.InputPath))
            {
                ProcessFilesInDirectory(options.InputPath, options.OutputPath);
            }
            else
            {
                CommandLineUtils.Logger($"Input path {options.InputPath} does not exist");
            }
        }

        private static void ProcessFile(string inputPath, string outputPath)
        {
            string ext = Path.GetExtension(inputPath);
            string html;
            try
            {
                string text = File.ReadAllText(inputPath);
                string body = string.Empty;

                if (ext == ".txt")
                {
                    body = ProcessText(text);
                }
                else
                {
                    body = ProcessMarkdown(text);
                }

                html = HtmlGenerator.GenerateHtmlFromText(Path.GetFileNameWithoutExtension(inputPath), body);
            }
            catch (Exception ex)
            {
                CommandLineUtils.Logger($"Error processing file {inputPath}: {ex.Message}");
                return;
            }

            string outputFileName = GetUniqueOutputFileName(inputPath, outputPath);
            SaveHtmlFile(outputFileName, html);

            CommandLineUtils.Logger($"File converted: {outputFileName}");
        }

        private static void ProcessFilesInDirectory(string inputDirectory, string outputDirectory)
        {
            string[] files = Directory.GetFiles(inputDirectory, "*.txt").Union(Directory.GetFiles(inputDirectory, "*.md")).ToArray();

            if (files.Length == 0)
            {
                CommandLineUtils.Logger($"No .txt or .md files found in directory {inputDirectory}");
                return;
            }

            CommandLineUtils.CreateOutputDirectory(outputDirectory);

            foreach (string file in files)
            {
                ProcessFile(file, outputDirectory);
            }
        }

        private static string GetUniqueOutputFileName(string inputPath, string outputPath)
        {
            string fileName = Path.GetFileNameWithoutExtension(inputPath);
            string outputFileName = Path.Combine(outputPath, fileName + ".html");

            int fileNumber = 1;
            while (File.Exists(outputFileName))
            {
                outputFileName = Path.Combine(outputPath, $"{fileName}_{fileNumber}.html");
                fileNumber++;
            }

            return outputFileName;
        }

        private static string ProcessText(string text)
        {
            string title = string.Empty;
            StringBuilder stringBuilder = new ();

            // check for a title
            int titleStart = text.IndexOf("\r\n\r\n\r\n");
            if (titleStart > 0)
            {
                title = text[..titleStart].Trim();
                text = text[(titleStart + 3) ..].Trim();
            }

            // title -- only if title is specified in the input file
            if (titleStart > 0)
            {
                stringBuilder.AppendLine($"<h1>{title}</h1>");
            }

            // split text into paragraphs based on new lines
            string[] paragraphs = NewParagraphRegex().Split(text).Select(paragraph => paragraph.Trim()).ToArray();

            // iterate through paragraphs and wrap each in <p> tags
            foreach (string paragraph in paragraphs)
            {
                // replace line breaks with spaces to keep the text in the same line
                string paragraphText = paragraph.Replace("\r\n", " ").Trim();
                stringBuilder.AppendLine($"<p>{paragraphText}</p>");
            }

            return stringBuilder.ToString();
        }

        private static string ProcessMarkdown(string text)
        {
            StringBuilder stringBuilder = new ();

            // Split text into lines
            string[] lines = text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            // Initialize a flag to track whether a paragraph is open
            bool paragraphOpen = false;

            // Iterate through lines
            foreach (string line in lines)
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (paragraphOpen)
                    {
                        stringBuilder.AppendLine("</p>");
                        paragraphOpen = false;
                    }

                    continue;
                }

                // Replace **text** with <strong>text</strong>
                string lineText = StrongSyntaxRegex().Replace(line, m => $"<strong>{m.Groups[1].Value}</strong>");

                // Replace line breaks with spaces to keep the text in the same line
                lineText = lineText.Replace("\r\n", " ").Trim();

                // Open a new <p> tag if not already open
                if (!paragraphOpen)
                {
                    stringBuilder.AppendLine("<p>");
                    paragraphOpen = true;
                }

                // Add line text
                stringBuilder.AppendLine(lineText);
            }

            // Close the last <p> tag if it's still open
            if (paragraphOpen)
            {
                stringBuilder.AppendLine("</p>");
            }

            return stringBuilder.ToString();
        }

        private static void SaveHtmlFile(string outputPath, string html)
        {
            File.WriteAllText(outputPath, html);
        }

        [GeneratedRegex("\\n\\s*\\n")]
        private static partial Regex NewParagraphRegex();

        [GeneratedRegex("\\*\\*(.*?)\\*\\*")]
        private static partial Regex StrongSyntaxRegex();
    }
}
