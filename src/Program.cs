namespace Learn2Blog
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = CommandLineParser.ParseCommandLineArgs(args);
            if (options != null)
            {
                if (options.ShowVersion)
                {
                    CommandLineUtils.ShowVersion();
                    return;
                }

                if (options.ShowHelp)
                {
                    CommandLineUtils.ShowHelp();
                    return;
                }

                string currentDirectory = Directory.GetCurrentDirectory();
                string inputPath = options.InputPath;
                string outputPath = options.OutputPath != null ? Path.Combine(currentDirectory, options.OutputPath) : Path.Combine(currentDirectory, "til");

                if (File.Exists(inputPath))
                {
                    CommandLineUtils.CreateOutputDirectory(outputPath);
                    HtmlProcessor.ProcessFile(inputPath, outputPath);
                }
                else if (Directory.Exists(inputPath))
                {
                    CommandLineUtils.CreateOutputDirectory(outputPath);
                    foreach (string file in Directory.GetFiles(inputPath, "*.txt"))
                    {
                        HtmlProcessor.ProcessFile(file, outputPath);
                    }
                }
                else
                {
                    CommandLineUtils.Logger($"Input path {inputPath} does not exist");
                }
            }
        }
    }
}