// <copyright file="Learn2Blog.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Learn2Blog
{
    public class Learn2Blog
    {
        public static void Run(string[] args)
        {
            CommandLineOptions options = CommandLineParser.ParseCommandLineArgs(args) ?? new CommandLineOptions { InputPath = string.Empty, OutputPath = string.Empty };
            if (options.ShowVersion)
            {
                CommandLineUtils.ShowVersion();
            }
            else if (options.ShowHelp)
            {
                CommandLineUtils.ShowHelp();
            }
            else
            {
                FileProcessor.ProcessFiles(options);
            }
        }
    }
}
