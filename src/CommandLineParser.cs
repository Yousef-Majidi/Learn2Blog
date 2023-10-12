using Tommy;

namespace Learn2Blog
{
    public class CommandLineOptions
    {
        public bool ShowVersion { get; set; }
        public bool ShowHelp { get; set; }
        public required string InputPath { get; set; }
        public required string OutputPath { get; set; }
        public string? ConfigPath { get; set; }
    }
    public class CommandLineParser
    {
        public static CommandLineOptions? ParseCommandLineArgs(string[] args)
        {
            if (args.Length == 0)
            {
                CommandLineUtils.Logger("Error: No command line arguments specified. See the help menu below:");
                CommandLineUtils.ShowHelp();
                return null;
            }

            CommandLineOptions options = new() { InputPath = "", OutputPath = "" };

            for (int i = 0; i < args.Length; i++)
            {
                if (i == 0)
                {
                    options.InputPath = args[i];
                    continue;
                }
                switch (args[i])
                {
                    case "-v":
                    case "--version":
                        options.ShowVersion = true;
                        return options;
                    case "-h":
                    case "--help":
                        options.ShowHelp = true;
                        return options;
                    case "-o":
                    case "--output":
                        if (i + 1 < args.Length)
                        {
                            // Low priority: If the output path is not specified through the config file, use the one specified through CLI
                            options.OutputPath = GetOutputPath(options.InputPath, args[i + 1]);
                            i++; // Skip the next argument as it is the output file path
                        }
                        else
                        {
                            CommandLineUtils.Logger("Error: Output flag must be used with an output path specified.", "See the help menu below:");
                            CommandLineUtils.ShowHelp();
                            return null;
                        }
                        break;
                    case "-c":
                    case "--config":
                        if (i + 1 < args.Length)
                        {
                            // If -c flag is present, parse the config file and ignore all other flags
                            var config = ParseConfigFile(args[i + 1]);
                            if (config != null)
                                options.OutputPath = config.OutputPath;
                            return options;
                        }
                        else
                        {
                            CommandLineUtils.Logger("Error: Config flag must be used with a config path specified.", "See the help menu below:");
                            CommandLineUtils.ShowHelp();
                            return null;
                        }
                    default:
                        break;
                }
            }

            // Check if input file is provided
            if (string.IsNullOrEmpty(options.InputPath))
            {
                CommandLineUtils.Logger("Error: Invalid command line arguments", "See the help menu below:");
                CommandLineUtils.ShowHelp();
                return null;
            }

            // check if output is not provided, if not, use the default
            if (string.IsNullOrEmpty(options.OutputPath))
            {
                options.OutputPath = GetOutputPath(options.InputPath, "til");
            }

            return options;
        }
        public static CommandLineOptions? ParseConfigFile(string configPath)
        {
            if (string.IsNullOrEmpty(configPath) || !File.Exists(configPath))
            {
                CommandLineUtils.Logger($"Config file {configPath} does not exist");
                return null;
            }
            using StreamReader reader = File.OpenText(configPath);

            CommandLineOptions options = new() { InputPath = "", OutputPath = "" };
            TomlTable? table = null;

            // Parse the table
            try
            {
                // Read the TOML file normally.
                table = TOML.Parse(reader);
            }
            catch (TomlParseException ex)
            {
                // Handle syntax error in whatever fashion you prefer
                foreach (TomlSyntaxException syntaxEx in ex.SyntaxErrors)
                    CommandLineUtils.Logger($"Error on {syntaxEx.Column}:{syntaxEx.Line}: {syntaxEx.Message}");

                return null;
            }

            // Get the values from the table and assign them to the options object
            if (table["o"].HasValue)
            {
                options.OutputPath = table["o"].ToString()!;
            }
            else if (table["output"].HasValue)
            {
                options.OutputPath = table["output"].ToString()!;
            }
            else
            {
                options.OutputPath = GetOutputPath(options.InputPath, options.OutputPath);
            }
            return options;
        }
        private static string GetOutputPath(string inputPath, string outputPath)
        {
            return Path.Combine(Path.GetDirectoryName(inputPath) ?? "", outputPath);
        }
    }
}