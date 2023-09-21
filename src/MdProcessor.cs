using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Learn2Blog.src
{
    public partial class MdProcessor
    {
        /* public static string ConvertMdToHtml(string title, string text)
         {
             int titleStart = text.IndexOf("\r\n\r\n\r\n");
             if (titleStart > 0)
             {
                 title = text[..titleStart].Trim();
                 text = text[(titleStart + 3)..].Trim();
             }

             StringBuilder htmlBuilder = new();

             // Creating start of HTML file
             htmlBuilder.AppendLine("<!DOCTYPE html>");
             htmlBuilder.AppendLine("<html lang=\"en\">");

             // Creating header
             htmlBuilder.AppendLine("<head>");
             htmlBuilder.AppendLine("<meta charset=\"utf-8\">");
             htmlBuilder.AppendLine($"<title>{title}</title>");
             htmlBuilder.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
             htmlBuilder.AppendLine("</head>");
             // End of header

             // Start of body
             htmlBuilder.AppendLine("<body>");

             // Title -- only if title is specified in the input file
             if (titleStart > 0)
             {
                 htmlBuilder.AppendLine($"<h1>{title}</h1>");
             }

             // Split text into paragraphs based on new lines
             string[] paragraphs = NewParagraphRegex().Split(text);

             // Initialize a flag to track whether a paragraph is open
             bool paragraphOpen = false;

             // Iterate through paragraphs and wrap each in <p> tags
             foreach (string paragraph in paragraphs)
             {
                 // Skip empty lines
                 if (string.IsNullOrWhiteSpace(paragraph))
                 {
                     if (paragraphOpen)
                     {
                         htmlBuilder.AppendLine("</p>");
                         paragraphOpen = false;
                     }
                     continue;
                 }

                 // Replace **text** with <strong>text</strong>
                 string paragraphText = Regex.Replace(paragraph, @"\*\*(.*?)\*\*", m => $"<strong>{m.Groups[1].Value}</strong>");

                 // Replace line breaks with spaces to keep the text in the same line
                 paragraphText = paragraphText.Replace("\r\n", " ").Trim();

                 // Open a new <p> tag if not already open
                 if (!paragraphOpen)
                 {
                     htmlBuilder.AppendLine("<p>");
                     paragraphOpen = true;
                 }

                 // Add paragraph text
                 htmlBuilder.AppendLine(paragraphText);
             }

             // Close the last <p> tag if it's still open
             if (paragraphOpen)
             {
                 htmlBuilder.AppendLine("</p>");
             }

             htmlBuilder.AppendLine("</body>");
             // Body end

             htmlBuilder.AppendLine("</html>");
             // HTML end

             return htmlBuilder.ToString();
         }*/

        public static string ConvertMdToHtml(string title, string text)
        {
            int titleStart = text.IndexOf("\r\n\r\n\r\n");
            if (titleStart > 0)
            {
                title = text[..titleStart].Trim();
                text = text[(titleStart + 3)..].Trim();
            }

            StringBuilder htmlBuilder = new();

            // Creating start of HTML file
            htmlBuilder.AppendLine("<!DOCTYPE html>");
            htmlBuilder.AppendLine("<html lang=\"en\">");

            // Creating header
            htmlBuilder.AppendLine("<head>");
            htmlBuilder.AppendLine("<meta charset=\"utf-8\">");
            htmlBuilder.AppendLine($"<title>{title}</title>");
            htmlBuilder.AppendLine("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">");
            htmlBuilder.AppendLine("</head>");
            // End of header

            // Start of body
            htmlBuilder.AppendLine("<body>");

            // Title -- only if title is specified in the input file
            if (titleStart > 0)
            {
                htmlBuilder.AppendLine($"<h1>{title}</h1>");
            }

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
                        htmlBuilder.AppendLine("</p>");
                        paragraphOpen = false;
                    }
                    continue;
                }

                // Replace **text** with <strong>text</strong>
                string lineText = Regex.Replace(line, @"\*\*(.*?)\*\*", m => $"<strong>{m.Groups[1].Value}</strong>");

                // Replace line breaks with spaces to keep the text in the same line
                lineText = lineText.Replace("\r\n", " ").Trim();

                // Open a new <p> tag if not already open
                if (!paragraphOpen)
                {
                    htmlBuilder.AppendLine("<p>");
                    paragraphOpen = true;
                }

                // Add line text
                htmlBuilder.AppendLine(lineText);
            }

            // Close the last <p> tag if it's still open
            if (paragraphOpen)
            {
                htmlBuilder.AppendLine("</p>");
            }

            htmlBuilder.AppendLine("</body>");
            // Body end

            htmlBuilder.AppendLine("</html>");
            // HTML end

            return htmlBuilder.ToString();
        }

        public static void ProcessFile(string inputPath, string outputPath)
        {
            // if the file is not a md file, log and return
            if (Path.GetExtension(inputPath) != ".md")
            {
                CommandLineUtils.Logger($"The specified input file [{inputPath}] is not a md file");
                return;
            }

            try
            {
                string text = File.ReadAllText(inputPath);
                string html = ConvertMdToHtml(Path.GetFileNameWithoutExtension(inputPath), text);

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
