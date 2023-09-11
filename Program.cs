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
                Console.WriteLine("Usage: learn2blog [option] <input>");
                Console.WriteLine("NOTE: <input> can be a .txt file or a directory");
                Console.WriteLine("Options:");
                Console.WriteLine("  -v, -version    Show version information");
                Console.WriteLine("  -h, -help       Show help information");
                return;
            }

            string inputPath = options.InputPath;

            if (File.Exists(inputPath))
            {
                Console.WriteLine($"Converting {inputPath}...");
            }
            else if (Directory.Exists(inputPath))
            {
                Console.WriteLine($"Converting all files in {inputPath}...");
            }
            else
            {
                Console.WriteLine($"Input path {inputPath} does not exist");
            }
            return;
        }

        static string GetAppVersion()
        {
            return typeof(Program).Assembly.GetName().Version.ToString();
        }
    }
}