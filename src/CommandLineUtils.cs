namespace Learn2Blog
{
    public class CommandLineUtils
    {
        public static void CreateOutputDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            Directory.CreateDirectory(path);
        }

        public static void ShowHelp()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("                        Learn2Blog Help                     ");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Learn2Blog - Convert .txt files to .html");
            Console.WriteLine("Usage 1: learn2blog [option]");
            Console.WriteLine("Usage 2: learn2blog <input>");
            Console.WriteLine("NOTE:<input> can be a file or a directory");
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Options:");
            Console.WriteLine("  -h, --ShowHelp     Show this help message");
            Console.WriteLine("  -v, --version      Show version information");
            Console.WriteLine("------------------------------------------------------------");
        }

        public static void ShowVersion()
        {
            var version = typeof(Program).Assembly.GetName().Version;
            Console.WriteLine($"Learn2Blog v{version}");
        }

        public static void Logger(params string[] messages)
        {
            foreach (string message in messages)
            {
                Console.WriteLine(message);
            }
        }
    }
}