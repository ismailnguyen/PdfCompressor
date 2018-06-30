using System;
using System.IO;
using System.Configuration;
using ILovePdfCompressor;

namespace PdfCompressorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = GetFilesToCompress();

            CompressFiles(files);

            Console.WriteLine();
            Console.WriteLine("Compression finished !");

            Console.WriteLine();
            Console.WriteLine("Press any key to exit ...");
            Console.Read();
        }

        private static FileInfo[] GetFilesToCompress()
        {
            var folderToScan = ConfigurationSettings.AppSettings["FolderToScan"];

            if (string.IsNullOrEmpty(folderToScan))
            {
                folderToScan = "./";
            }

            var directory = new DirectoryInfo(folderToScan);

            return directory.GetFiles("*.pdf", SearchOption.AllDirectories);
        }

        private static void CompressFiles(FileInfo[] files)
        {
            var apiPublicKey = ConfigurationSettings.AppSettings["API_Public_Key"];
            var apiPrivateKey = ConfigurationSettings.AppSettings["API_Private_Key"];

            ICompressor compressor = new PdfCompressor(apiPublicKey, apiPrivateKey);

            int i = 1;
            foreach (var file in files)
            {
                Console.WriteLine($"Compressing #{i++} ({file.FullName})");

                try
                {
                    compressor.Compress(file.FullName, file.DirectoryName);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }
    }
}
