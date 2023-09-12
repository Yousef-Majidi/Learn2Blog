namespace Learn2Blog
{
    public class CommandLineParser
    {
        public static CommandLineOptions? ParseCommandLineArgs(string[] args)
        {
            CommandLineOptions options = new() { InputPath = "" };

            if (args.Length == 0)
            {
                CommandLineUtils.Logger("Error: No command line arguments specified. See the help menu below:");
                CommandLineUtils.ShowHelp();
                return null;
            }

            if (args[0].Contains('-'))
            {
                if (args.Contains("-v") || args.Contains("--version"))
                {
                    options.ShowVersion = true;
                    return options;
                }

                if (args.Contains("-h") || args.Contains("--help"))
                {
                    options.ShowHelp = true;
                    return options;
                }

                CommandLineUtils.Logger("Error: Invalid command line arguments.", "See the help menu below:");
                CommandLineUtils.ShowHelp();
            }

            if (args.Length == 1)
            {
                options.InputPath = args[0];
                return options;
            }
            else
            {
                CommandLineUtils.Logger("Error: Invalid command line arguments", "See the help menu below:");
                CommandLineUtils.ShowHelp();
                return null;
            }
        }
    }

    public class CommandLineOptions
    {
        public bool ShowVersion { get; set; }
        public bool ShowHelp { get; set; }
        public required string InputPath { get; set; }
    }
}