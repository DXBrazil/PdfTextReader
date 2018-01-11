﻿using System;
using System.Collections.Generic;
using System.Text;
using PdfTextReader.Base;
using PdfTextReader.Parser;
using PdfTextReader.ExecutionStats;
using PdfTextReader.TextStructures;

namespace PdfTextReader
{
    class Program3
    {
        public static void ProcessTextLines(string page)
        {
            var artigos = Examples.GetTextLines(page)
                            .ConvertText<CreateParagraphs, TextStructure>()
                            .ConvertText<CreateTextSegment, TextSegment>()
                                .DebugPrint()
                            .ToList();            
        }
    }
}