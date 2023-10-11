namespace Learn2Blog
{
    public class FileProcessor
    {
        public static void ProcessFiles(CommandLineOptions options)
        {
            if (File.Exists(options.InputPath))
            {
                string ext = Path.GetExtension(options.InputPath);
                string html = "";

                try
                {
                    string text = File.ReadAllText(options.InputPath);
                    string body = "";

                    if (ext == ".txt") body = HtmlProcessor.ProcessText(text);
                    else body = HtmlProcessor.ProcessMarkdown(text);

                    html = HtmlGenerator.GenerateHtmlFromText(Path.GetFileNameWithoutExtension(options.InputPath), body);
                }
                catch (Exception ex)
                {
                    CommandLineUtils.Logger($"Error processing file {options.InputPath}: {ex.Message}");
                    return;
                }

                string outputFileName = Path.Combine(options.OutputPath, Path.GetFileNameWithoutExtension(options.InputPath) + ".html");

                int fileNumber = 1;
                string fileName = Path.GetFileNameWithoutExtension(options.InputPath);
                string newFileName = fileName;

                while (File.Exists(outputFileName))
                {
                    newFileName = $"{fileName}_{fileNumber}";
                    outputFileName = Path.Combine(options.OutputPath, newFileName + ".html");
                    fileNumber++;
                }

                FileUtility.SaveHtmlFile(outputFileName, html);
                CommandLineUtils.Logger($"File converted: {outputFileName}");
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
    }
}