// <copyright file="FileProcessorTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace Learn2BlogTest
{
    using System;
    using Learn2Blog;
    using Xunit;
    using Xunit.Abstractions;

    public class FileProcessorTests : IClassFixture<FileProcessorFixture>
    {
        private readonly FileProcessorFixture fixture;
        private readonly ITestOutputHelper output;

        public FileProcessorTests(FileProcessorFixture fixture, ITestOutputHelper output)
        {
            this.fixture = fixture;
            this.output = output;
        }

        [Fact]
        public void TestProcessTextFiles()
        {
            this.output.WriteLine("Tests that the ProcessFiles method processes text files correctly.");

            var inputPath = Path.Combine(this.fixture.InputDirectory, "TestFile.txt");
            var outputPath = Path.Combine(this.fixture.OutputDirectory, "TestFile.html");

            File.WriteAllText(inputPath, "Test file contents");

            var options = new CommandLineOptions
            {
                InputPath = inputPath,
                OutputPath = this.fixture.OutputDirectory,
            };

            FileProcessor.ProcessFiles(options);

            Assert.True(File.Exists(outputPath));
        }

        [Fact]
        public void TestProcessMarkdownFiles()
        {
            this.output.WriteLine("Tests that the ProcessFiles method processes markdown files correctly.");

            var inputPath = Path.Combine(this.fixture.InputDirectory, "TestFile.md");
            var outputPath = Path.Combine(this.fixture.OutputDirectory, "TestFile.html");

            File.WriteAllText(inputPath, "Test file contents");

            var options = new CommandLineOptions
            {
                InputPath = inputPath,
                OutputPath = this.fixture.OutputDirectory,
            };

            FileProcessor.ProcessFiles(options);

            Assert.True(File.Exists(outputPath));
        }

        [Fact]
        public void TestProcessFilesInDirectory()
        {
            this.output.WriteLine("Tests that the ProcessFilesInDirectory method processes files in a directory correctly.");

            var inputPath = Path.Combine(this.fixture.InputDirectory, "TestFile.txt");
            var outputPath = Path.Combine(this.fixture.OutputDirectory, "TestFile.html");

            File.WriteAllText(inputPath, "Test file contents");

            var options = new CommandLineOptions
            {
                InputPath = this.fixture.InputDirectory,
                OutputPath = this.fixture.OutputDirectory,
            };

            FileProcessor.ProcessFiles(options);

            Assert.True(File.Exists(outputPath));
        }

        [Fact]
        public void TestGetUniqueOutputFileName()
        {
            this.output.WriteLine("Tests that the GetUniqueOutputFileName method generates unique file names.");

            var inputPath = Path.Combine(this.fixture.InputDirectory, "TestFile.txt");
            var inputPath2 = Path.Combine(this.fixture.InputDirectory, "TestFile.md");

            File.WriteAllText(inputPath, "Test file contents");
            File.WriteAllText(inputPath2, "Test file contents 2");

            var options = new CommandLineOptions
            {
                InputPath = this.fixture.InputDirectory,
                OutputPath = this.fixture.OutputDirectory,
            };

            FileProcessor.ProcessFiles(options);

            Assert.True(File.Exists(Path.Combine(this.fixture.OutputDirectory, "TestFile_1.html")));
        }
    }

    public class FileProcessorFixture : IDisposable
    {
        public FileProcessorFixture()
        {
            this.InputDirectory = Path.Combine(Path.GetTempPath(), "TestInputDirectory");
            this.OutputDirectory = Path.Combine(Path.GetTempPath(), "TestOutputDirectory");

            Directory.CreateDirectory(this.InputDirectory);
            Directory.CreateDirectory(this.OutputDirectory);
        }

        public string InputDirectory { get; }

        public string OutputDirectory { get; }

        public void Dispose()
        {
            Directory.Delete(this.InputDirectory, true);
            Directory.Delete(this.OutputDirectory, true);
        }
    }
}
