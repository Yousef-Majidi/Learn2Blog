﻿// <copyright file="HtmlGenerator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Learn2Blog
{
    public class HtmlGenerator
    {
        public static string GenerateHtmlFromText(string title, string body)
        {
            return $@"<!DOCTYPE html>
                 <html lang=""en"">
                 <head>
                     <meta charset=""utf-8"">
                     <title>{title}</title>
                     <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
                 </head>
                 <body>
                     {body}
                 </body>
                 </html>";
        }
    }
}
