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
                    // Get all files in the directory and save them to the files array
                    string[] files = Directory.GetFiles(inputPath, "*.txt").Union(Directory.GetFiles(inputPath, "*.md")).ToArray();


                    if (files.Length == 0)
                    {
                        CommandLineUtils.Logger($"No .txt or .md files found in directory {inputPath}");
                        return;
                    }

                    CommandLineUtils.CreateOutputDirectory(outputPath);

                    foreach (string file in files) // Go through the files array and convert ones that end with .md or .txt
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
