using System.Text;

namespace Learn2Blog
{
    public class HtmlProcessor
    {
        public static string ConvertTextToHtml(string title, string text)
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

            // split text into paragraphs based on new lines
            string[] paragraphs = text.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

            // iterate through paragraphs and wrap each in <p> tags
            foreach (string paragraph in paragraphs)
            {
                htmlBuilder.AppendLine($"<p>{paragraph.Trim()}</p>");
            }

            htmlBuilder.AppendLine("</body>");
            // body end

            htmlBuilder.AppendLine("</html>");
            // html end

            return htmlBuilder.ToString();
        }

        public static void ProcessFile(string inputPath, string outputPath)
        {
            try
            {
                string text = File.ReadAllText(inputPath);
                string html = ConvertTextToHtml(Path.GetFileNameWithoutExtension(inputPath), text);

                string outputFileName = Path.Combine(outputPath, Path.GetFileNameWithoutExtension(inputPath) + ".html");
                File.WriteAllText(outputFileName, html);

                Console.WriteLine($"File converted: {outputFileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file: {ex.Message}");
            }
        }
    }
}