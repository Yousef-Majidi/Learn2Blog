// <copyright file="CommandLineParserTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Learn2BlogTest
{
    using System.IO;
    using Learn2Blog;
    using Xunit;
    using Xunit.Abstractions;

    public class CommandLineParserTests
    {
        private readonly ITestOutputHelper output;

        public CommandLineParserTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestNoArgumentReturnsNull()
        {
            this.output.WriteLine("Should return null when user passes no arguments");

            var args = Array.Empty<string>();
            CommandLineOptions? options = CommandLineParser.ParseCommandLineArgs(args);

            Assert.Null(options);
        }

        [Fact]
        public void TestNoArgumentShowsHelp()
        {
            this.output.WriteLine("Should display the help menu when user passes no arguments");

            var args = Array.Empty<string>();

            using StringWriter sw = new();
            Console.SetOut(sw);

            CommandLineOptions? options = CommandLineParser.ParseCommandLineArgs(args);
            string consoleOutput = sw.ToString();

            Assert.Contains("Learn2Blog Help", consoleOutput);
            Assert.Contains("Options:", consoleOutput);
        }

        [Theory]
        [InlineData("-v")]
        [InlineData("--version")]
        public void TestVersionArgument(string arg)
        {
            this.output.WriteLine("Should return option with ShowVersion == true");

            var args = new string[] { arg };
            CommandLineOptions? options = CommandLineParser.ParseCommandLineArgs(args);

            Assert.NotNull(options);
            Assert.False(options?.ShowHelp);
            Assert.True(options?.ShowVersion);
        }

        [Theory]
        [InlineData("-h")]
        [InlineData("--help")]
        public void TestHelpArgument(string arg)
        {
            this.output.WriteLine("Should return option with ShowHelp == true");

            var args = new string[] { arg };
            CommandLineOptions? options = CommandLineParser.ParseCommandLineArgs(args);

            Assert.NotNull(options);
            Assert.False(options?.ShowVersion);
            Assert.True(options?.ShowHelp);
        }

        [Theory]
        [InlineData("-o")]
        [InlineData("--output")]
        public void TestOutputArgument(string arg)
        {
            this.output.WriteLine("Should return option with OutputPath == 'testOutput'");

            string outputPath = "testOutput";
            var args = new string[] { arg, outputPath, "input" };
            CommandLineOptions? options = CommandLineParser.ParseCommandLineArgs(args);

            Assert.NotNull(options);
            Assert.False(options?.ShowHelp);
            Assert.False(options?.ShowVersion);
            Assert.Equal(outputPath, options?.OutputPath);
        }

        // TODO: test not implemented
        //    [Theory]
        //    [InlineData("-c")]
        //    [InlineData("--config")]
        //    public void TestConfigArgument()
        //    {
        //    }
    }
}
