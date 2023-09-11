namespace Learn2Blog
{
    public class HtmlProcessor
    {
        public static string ConvertTextToHtml(string text)
        {
            // TODO: Convert text to HTML

            return "html created";
        }

        public static void ProcessFile(string inputPath, string outputPath)
        {
            try
            {
                string text = File.ReadAllText(inputPath);
                string html = ConvertTextToHtml(text);



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