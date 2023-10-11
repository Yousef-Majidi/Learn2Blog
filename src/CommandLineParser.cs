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
                string arg = args[i];
                if (TryHandleCommand(args, ref i, arg, options)) continue;
                options.InputPath = args[0];
            }

            // Check if input file is provided
            if (string.IsNullOrEmpty(options.InputPath) && !options.ShowVersion && !options.ShowHelp)
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

        private static bool TryHandleCommand(string[] args, ref int index, string arg, CommandLineOptions options)
        {
            switch (arg)
            {
                case "-v":
                case "--version":
                    options.ShowVersion = true;
                    return true;
                case "-h":
                case "--help":
                    options.ShowHelp = true;
                    return true;
                case "-o":
                case "--output":
                    return HandleOutputCommand(args, ref index, options);
                case "-c":
                case "--config":
                    options.InputPath = args[0];
                    return HandleConfigCommand(args, ref index, options);
                default:
                    return false;
            }
        }

        private static bool HandleOutputCommand(string[] args, ref int index, CommandLineOptions options)
        {
            if (index + 1 < args.Length)
            {
                string outputDir = args[index + 1];
                options.OutputPath = GetOutputPath(options.InputPath, outputDir);
                index++; // Skip the next argument as it is the output file path
                return true;
            }

            CommandLineUtils.Logger("Error: Output flag must be used with an output path specified.", "See the help menu below:");
            CommandLineUtils.ShowHelp();
            return false;
        }

        private static bool HandleConfigCommand(string[] args, ref int index, CommandLineOptions options)
        {
            if (index + 1 < args.Length)
            {
                var config = ParseConfigFile(args[index + 1]);
                if (config != null)
                    options.OutputPath = GetOutputPath(options.InputPath, config.OutputPath);
                return true;
            }

            CommandLineUtils.Logger("Error: Config flag must be used with a config path specified.", "See the help menu below:");
            CommandLineUtils.ShowHelp();
            return false;
        }

        private static CommandLineOptions? ParseConfigFile(string configPath)
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
                options.OutputPath = GetOutputPath(options.InputPath, table["output"].ToString()!);
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