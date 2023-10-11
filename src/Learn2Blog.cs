namespace Learn2Blog
{
    public class Learn2Blog
    {
        public static void Run(string[] args)
        {
            CommandLineOptions options = CommandLineParser.ParseCommandLineArgs(args) ?? new CommandLineOptions { InputPath = "" };
            if (options.ShowVersion) CommandLineUtils.ShowVersion();
            else if (options.ShowHelp) CommandLineUtils.ShowHelp();
            else FileProcessor.ProcessFiles(options);
        }
    }
}