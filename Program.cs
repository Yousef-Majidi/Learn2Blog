namespace Learn2Blog
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = CommandLineParser.ParseCommandLineArgs(args);

            if (options == null)
            {
                Console.WriteLine("Error parsing command line arguments");
                return;
            }

            if (options.ShowVersion)
            {
                Console.WriteLine($"Learn2Blog v{GetAppVersion()}");
                return;
            }

            if (options.ShowHelp)
            {
                Console.WriteLine("Learn2Blog - Convert .txt files to .html");
                Console.WriteLine("Usage: learn2blog [options] -i <input>");
                Console.WriteLine("Options:");
                Console.WriteLine("  -h, --ShowHelp     Show this help message");
                Console.WriteLine("  -v, --version      Show version information");
                return;
            }

            if (options.InputPath == "")
            {
                Console.WriteLine("No command line arguments specified. Use --help or see the usage below:");
                return;
            }

            string inputPath = options.InputPath;
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "til");
            if (Directory.Exists(outputPath))
            {
                //Console.WriteLine($"Output directory {outputPath} already exists. Deleting old directory...");
                Directory.Delete(outputPath, true);
            }
            Directory.CreateDirectory(outputPath);

            if (File.Exists(inputPath))
            {
                HtmlProcessor.ProcessFile(inputPath, outputPath);
            }
            else if (Directory.Exists(inputPath))
            {
                foreach (string file in Directory.GetFiles(inputPath, "*.txt"))
                {
                    HtmlProcessor.ProcessFile(file, outputPath);
                }
            }
            else
            {
                Console.WriteLine($"Input path {inputPath} does not exist");
            }


        }

        static string GetAppVersion()
        {
            return typeof(Program).Assembly.GetName().Version.ToString();
        }
    }
}