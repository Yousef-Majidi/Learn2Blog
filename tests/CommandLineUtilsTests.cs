// <copyright file="CommandLineUtilsTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Learn2BlogTest
{
    using System.Text.RegularExpressions;
    using Learn2Blog;
    using Xunit;
    using Xunit.Abstractions;

    public partial class CommandLineUtilsTests
    {
        private readonly ITestOutputHelper output;

        public CommandLineUtilsTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestCreateOutputDirectory()
        {
            this.output.WriteLine("Tests that the output directory is created if it does not exist.");

            var testDirectory = Path.Combine(Path.GetTempPath(), "TestDirectory");
            CommandLineUtils.CreateOutputDirectory(testDirectory);
            Assert.True(Directory.Exists(testDirectory));
        }

        [Fact]
        public void TestShowHelp()
        {
            this.output.WriteLine("Tests that the help menu is displayed.");

            var currentOut = Console.Out;
            using StringWriter sw = new();
            Console.SetOut(sw);

            CommandLineUtils.ShowHelp();
            var consoleOutput = sw.ToString();

            Assert.Contains("Learn2Blog Help", consoleOutput);
            Assert.Contains("Options:", consoleOutput);

            Console.SetOut(currentOut);
        }

        [Fact]
        public void TestShowVersion()
        {
            this.output.WriteLine("Tests that the version is displayed in the correct format.");

            var currentOut = Console.Out;
            using StringWriter sw = new();
            Console.SetOut(sw);

            CommandLineUtils.ShowVersion();
            var consoleOutput = sw.ToString();

            Assert.Matches(VersionPattern(), consoleOutput);

            Console.SetOut(currentOut);
        }

        [Fact]
        public void TestLogger()
        {
            this.output.WriteLine("Tests that the logger writes the correct messages to the console.");

            var currentOut = Console.Out;
            using StringWriter sw = new();
            Console.SetOut(sw);
            string[] messages = { "Test Message 1", "Test Message 2", "Test Message 3" };

            CommandLineUtils.Logger(messages);
            string consoleOutput = sw.ToString();

            foreach (string message in messages)
            {
                Assert.Contains(message, consoleOutput);
            }

            Console.SetOut(currentOut);
        }

        [GeneratedRegex("Learn2Blog v\\d+\\.\\d+\\.\\d+")]
        private static partial Regex VersionPattern();
    }
}
