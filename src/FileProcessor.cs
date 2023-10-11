namespace Learn2Blog
{
    public class FileProcessor
    {
        public static void ProcessFiles(CommandLineOptions options)
        {
            if (File.Exists(options.InputPath)) ProcessFile(options.InputPath, options.OutputPath);
            else if (Directory.Exists(options.InputPath)) ProcessFilesInDirectory(options.InputPath, options.OutputPath);
            else CommandLineUtils.Logger($"Input path {options.InputPath} does not exist");
        }

        private static void ProcessFile(string inputPath, string outputPath)
        {
            string ext = Path.GetExtension(inputPath);
            string html = "";

            try
            {
                string text = File.ReadAllText(inputPath);
                string body = "";

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
            FileUtility.SaveHtmlFile(outputFileName, html);

            CommandLineUtils.Logger($"File converted: {outputFileName}");
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
    }
}