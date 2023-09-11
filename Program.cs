using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommandLine;

namespace Learn2Blog
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Parser.Default.ParseArguments<Options>(args)
                .WithParsedAsync(RunWithOptions);
        }

        static async Task RunWithOptions(Options options)
        {
            if (options.ShowVersion)
            {
                Console.WriteLine($"Learn2Blog v{GetAppVersion()}");
                return;
            }

            if (options.ShowHelp)
            {
                Console.WriteLine("Learn2Blog - Convert .txt files to .html");
                Console.WriteLine("Usage: learn2blog [options] <input>");
                Console.WriteLine("Options:");
                Console.WriteLine("  -v, --version    Show version information");
                Console.WriteLine("  -h, --help       Show help information");
                return;
            }
        }

        static string GetAppVersion()
        {
            return typeof(Program).Assembly.GetName().Version.ToString();
        }
    }

    class Options
    {
        [Option('v', "version", HelpText = "Show version information")]
        public bool ShowVersion { get; set; }

        [Option('h', "help", HelpText = "Show help information")]
        public bool ShowHelp { get; set; }
    }
}