namespace Learn2Blog
{
    public class CommandLineParser
    {
        public static CommandLineOptions? ParseCommandLineArgs(string[] args)
        {
            CommandLineOptions options = new() { InputPath = "" };

            if (args.Contains("-v") || args.Contains("--version"))
            {
                options.ShowVersion = true;
            }
            else if (args.Contains("-h") || args.Contains("--help"))
            {
                options.ShowHelp = true;
            }
            else if (args.Contains("-i") || args.Contains("--input"))
            {
                int inputIndex = Array.IndexOf(args, "-i");
                if (inputIndex == -1)
                {
                    inputIndex = Array.IndexOf(args, "--input");
                }

                if (inputIndex == -1 || inputIndex + 1 >= args.Length)
                {
                    Console.WriteLine("Error: Input path not specified. Use -i or --input to specify the input path");
                    return null;
                }

                options.InputPath = args[inputIndex + 1];
            }
            return options;
        }
    }

    public class CommandLineOptions
    {
        public bool ShowVersion { get; set; }

        public bool ShowHelp { get; set; }

        public required string InputPath { get; set; }
    }
}