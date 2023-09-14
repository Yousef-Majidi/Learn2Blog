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

                if (args.Contains("-o") || args.Contains("--output"))
                {
                    // if output flag is used without a second argument
                    if (args.Length < 3)
                    {
                        CommandLineUtils.Logger("Error: Output flag must be used with an output path specified.", "See the help menu below:");
                        CommandLineUtils.ShowHelp();
                        return null;
                    }

                    options.InputPath = args[1];
                    options.OutputPath = args[2];
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
        public string? OutputPath { get; set; }
    }
}