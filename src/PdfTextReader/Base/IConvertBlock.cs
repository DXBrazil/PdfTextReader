﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PdfTextReader.Base
{
    interface IConvertBlock
    {
        IEnumerable<TextLine> ProcessPage(int pageNumber, BlockPage page);
    }
}
