using System.Text;
using System.Text.RegularExpressions;

namespace Learn2Blog
{
    public partial class HtmlProcessor
    {
        public static string HtmlBuilder(string title, string body)
        {
            StringBuilder htmlBuilder = new();

            // html
            htmlBuilder.AppendLine("<!DOCTYPE html>");
            htmlBuilder.AppendLine("<html lang=\"en\">");

            // header
            htmlBuilder.AppendLine("<head>");
            htmlBuilder.AppendLine("<meta charset=\"utf-8\">");
            htmlBuilder.AppendLine($"<title>{title}</title>");
            htmlBuilder.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
            htmlBuilder.AppendLine("</head>");
            // header end

            // body
            htmlBuilder.AppendLine("<body>");
            htmlBuilder.AppendLine(body);
            htmlBuilder.AppendLine("</body>");
            // body end

            htmlBuilder.AppendLine("</html>");
            // html end

            return htmlBuilder.ToString();
        }

        public static string ProcessText(string text)
        {
            string title = "";
            StringBuilder stringBuilder = new();

            // check for a title
            int titleStart = text.IndexOf("\r\n\r\n\r\n");
            if (titleStart > 0)
            {
                title = text[..titleStart].Trim();
                text = text[(titleStart + 3)..].Trim();
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

        public static string ProcessMarkdown(string text)
        {
            StringBuilder stringBuilder = new();

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

        public static void ProcessFile(string inputPath, string outputPath)
        {
            string ext = Path.GetExtension(inputPath);
            // if the file is a text or markdown file, then try to convert it
            if (ext == ".txt" || ext == ".md")
            {
                try
                {
                    string text = File.ReadAllText(inputPath);
                    string body = "";
                    string html = "";

                    if (ext == ".txt")
                    {
                        body = ProcessText(text);
                    }
                    else
                    {
                        body = ProcessMarkdown(text);
                    }

                    html = HtmlBuilder(Path.GetFileNameWithoutExtension(inputPath), body);

                    string outputFileName = Path.Combine(outputPath, Path.GetFileNameWithoutExtension(inputPath) + ".html");

                    // if a file with the same name already exists, append a number to the file name
                    if (File.Exists(outputFileName))
                    {
                        int fileNumber = 1;
                        string fileName = Path.GetFileNameWithoutExtension(inputPath);
                        string newFileName = $"{fileName}_{fileNumber}";
                        outputFileName = Path.Combine(outputPath, newFileName + ".html");

                        while (File.Exists(outputFileName))
                        {
                            fileNumber++;
                            newFileName = $"{fileName}_{fileNumber}";
                            outputFileName = Path.Combine(outputPath, newFileName + ".html");
                        }
                    }

                    File.WriteAllText(outputFileName, html);

                    CommandLineUtils.Logger($"File converted: {outputFileName}");
                }
                catch (Exception ex)
                {
                    CommandLineUtils.Logger($"Error: failed to process file: {ex.Message}");
                }

            }
            // if the file is not a text or markdown file, log and return
            else
            {
                CommandLineUtils.Logger($"The specified input file [{inputPath}] is not a text or markdown file");
                return;
            }

        }

        [GeneratedRegex("\\n\\s*\\n")]
        private static partial Regex NewParagraphRegex();

        [GeneratedRegex("\\*\\*(.*?)\\*\\*")]
        private static partial Regex StrongSyntaxRegex();
    }
}