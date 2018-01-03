using iText.Kernel.Pdf;
using System;
using System.IO;

namespace PdfTextReader
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1
            //Console.WriteLine("Testing in batch!");
            //ProcessFiles();

            // 2
            Console.WriteLine("Processando a tabela!");            
            ProcessFileWithTable("p40");

            //ProcessPage();
            //ProcessFilesWithTable();
        }

        static void TabifyFile(string basename)
        {
            var user = new UserWriter();

            user.ProcessBlockExtra($"bin/{basename}.pdf", $"bin/{basename}-table-output.pdf");
        }

        static void ProcessPage()
        {
            var user = new UserWriter();
            string subfolder = "PerYear";
            var dir = new DirectoryInfo($"bin/{subfolder}");
            string basename = "p44";
            //user.ProcessBlock($"bin/{basename}.pdf", $"bin/{basename}-output.pdf");
            //user.ProcessText($"bin/{subfolder}/{basename}.pdf", $"bin/{subfolder}/{basename}-output.pdf");
            user.ProcessBlock($"bin/{subfolder}/{basename}.pdf", $"bin/{subfolder}/{basename}-output.pdf");
        }

        static void ProcessFileWithTable(string basename)
        {
            var user = new UserWriter();
        
            user.ProcessBlockExtra($"bin/{basename}.pdf", $"bin/{basename}-table-output.pdf");

            var tablesFound = user.ActiveTables;

            user.ProcessBlock($"bin/{basename}.pdf", $"bin/{basename}-output.pdf");            
        }

        static void ProcessFile(string basename)
        {
            var user = new UserWriter();
            
            user.ProcessBlock($"bin/{basename}.pdf", $"bin/{basename}-output.pdf");
        }

        static void ProcessFiles()
        {
            string subfolder = "dz";
            var dir = new DirectoryInfo($"bin/{subfolder}");
            var user = new UserWriter();

            foreach (var f in dir.EnumerateFiles("*.pdf"))
            {
                string filename = f.Name;
                string basename = Path.GetFileNameWithoutExtension(filename);

                if (basename.EndsWith("-output"))
                    continue;

                //user.ProcessText($"bin/{subfolder}/{basename}.pdf", $"bin/{subfolder}/{basename}-output.pdf");
                user.ProcessBlock($"bin/{subfolder}/{basename}.pdf", $"bin/{subfolder}/{basename}-output.pdf");
            }

        }
        static void ProcessFilesWithTable()
        {
            string subfolder = "dz";
            var dir = new DirectoryInfo($"bin/{subfolder}");
            var user = new UserWriter();

            foreach (var f in dir.EnumerateFiles("*.pdf"))
            {
                string filename = f.Name;
                string basename = Path.GetFileNameWithoutExtension(filename);

                if (basename.EndsWith("-output"))
                    continue;

                user.ProcessBlockExtra($"bin/{subfolder}/{basename}.pdf", $"bin/{subfolder}/{basename}-table-output.pdf");

                var tablesFound = user.ActiveTables;

                //user.ProcessText($"bin/{subfolder}/{basename}.pdf", $"bin/{subfolder}/{basename}-output.pdf");
                user.ProcessBlock($"bin/{subfolder}/{basename}.pdf", $"bin/{subfolder}/{basename}-output.pdf");
            }

        }
    }
}
