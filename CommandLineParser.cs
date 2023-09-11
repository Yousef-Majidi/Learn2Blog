using CommandLine;

namespace Learn2Blog
{
    public class CommandLineParser
    {
        public static Options ParseCommandLineArgs(string[] args)
        {
            Options? options = null;
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(parsedOptions => options = parsedOptions);
            return options;
        }
    }

    public class Options
    {
        [Option('v', "version", HelpText = "Show version information")]
        public bool ShowVersion { get; set; }

        [Option('h', "help", HelpText = "Show help information")]
        public bool ShowHelp { get; set; }

        [Value(0, MetaName = "input", HelpText = "Input .txt file or directory")]
        public required string InputPath { get; set; }
    }
}