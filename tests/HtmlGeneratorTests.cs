// <copyright file="HtmlGeneratorTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Learn2BlogTest
{
    using Learn2Blog;
    using Xunit;
    using Xunit.Abstractions;

    public class HtmlGeneratorTests : IClassFixture<FileProcessorFixture>
    {
        private readonly ITestOutputHelper output;

        public HtmlGeneratorTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestHtmlGeneratorContent()
        {
            this.output.WriteLine("Tests that the HtmlGeneratorContent's GenerateHtmlFromText() returns correct string.");

            Assert.Equal(
                $@"<!DOCTYPE html>
                 <html lang=""en"">
                 <head>
                     <meta charset=""utf-8"">
                     <title>Test Title</title>
                     <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
                 </head>
                 <body>
                     Test Body
                 </body>
                 </html>", HtmlGenerator.GenerateHtmlFromText("Test Title", "Test Body"));
        }
    }
}
