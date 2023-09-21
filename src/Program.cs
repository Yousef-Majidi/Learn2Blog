using Learn2Blog.src;

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

                    // Check file extension to decide whether to use MdProcessor or HtmlProcessor
                    string fileExtension = Path.GetExtension(inputPath);
                    if (fileExtension == ".md")
                    {
                        MdProcessor.ProcessFile(inputPath, outputPath);
                    }
                    else if (fileExtension == ".txt")
                    {
                        HtmlProcessor.ProcessFile(inputPath, outputPath);
                    }
                    else
                    {
                        CommandLineUtils.Logger($"Unsupported file format for input path: {inputPath}");
                    }
                }
                else if (Directory.Exists(inputPath))
                {
                    // Get all files in the directory and save them to the files array
                    string[] files = Directory.GetFiles(inputPath, "*.*"); 

                    if (files.Length == 0)
                    {
                        CommandLineUtils.Logger($"No files found in directory {inputPath}");
                        return;
                    }

                    CommandLineUtils.CreateOutputDirectory(outputPath);

                    foreach (string file in files) // Go through the files array and convert ones that end with .md or .txt
                    {
                        // Check file extension to decide whether to use MdProcessor or HtmlProcessor.
                        string fileExtension = Path.GetExtension(file);
                        if (fileExtension == ".md")
                        {
                            MdProcessor.ProcessFile(file, outputPath);
                        }
                        else if (fileExtension == ".txt")
                        {
                            HtmlProcessor.ProcessFile(file, outputPath);
                        }
                        else
                        {
                            CommandLineUtils.Logger($"Unsupported file format for file: {file}");
                        }
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
