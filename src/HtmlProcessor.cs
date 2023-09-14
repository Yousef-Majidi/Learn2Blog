using System.Text;
using System.Text.RegularExpressions;

namespace Learn2Blog
{
    public partial class HtmlProcessor
    {
        public static string ConvertTextToHtml(string title, string text)
        {
            // check for a title
            int titleStart = text.IndexOf("\r\n\r\n\r\n");
            if (titleStart > 0)
            {
                title = text[..titleStart].Trim();
                text = text[(titleStart + 3)..].Trim();
            }

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

            // title -- only if title is specified in the input file
            if (titleStart > 0)
            {
                htmlBuilder.AppendLine($"<h1>{title}</h1>");
            }

            // split text into paragraphs based on new lines
            string[] paragraphs = NewParagraphRegex().Split(text).Select(paragraph => paragraph.Trim()).ToArray();

            // iterate through paragraphs and wrap each in <p> tags
            foreach (string paragraph in paragraphs)
            {
                // replace line breaks with spaces to keep the text in the same line
                string paragraphText = paragraph.Replace("\r\n", " ");
                htmlBuilder.AppendLine($"<p>{paragraphText.Trim()}</p>");
            }

            htmlBuilder.AppendLine("</body>");
            // body end

            htmlBuilder.AppendLine("</html>");
            // html end

            return htmlBuilder.ToString();
        }

        public static void ProcessFile(string inputPath, string outputPath)
        {
            // if the file is not a text file, log and return
            if (Path.GetExtension(inputPath) != ".txt")
            {
                CommandLineUtils.Logger($"The specified input file [{inputPath}] is not a text file");
                return;
            }

            try
            {
                string text = File.ReadAllText(inputPath);
                string html = ConvertTextToHtml(Path.GetFileNameWithoutExtension(inputPath), text);

                string outputFileName = Path.Combine(outputPath, Path.GetFileNameWithoutExtension(inputPath) + ".html");
                File.WriteAllText(outputFileName, html);

                CommandLineUtils.Logger($"File converted: {outputFileName}");
            }
            catch (Exception ex)
            {
                CommandLineUtils.Logger($"Error: failed to process file: {ex.Message}");
            }
        }

        [GeneratedRegex("\\n\\s*\\n")]
        private static partial Regex NewParagraphRegex();
    }
}