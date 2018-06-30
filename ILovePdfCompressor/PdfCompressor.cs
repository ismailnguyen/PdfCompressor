using LovePdf.Core;
using LovePdf.Model.Task;
using System;

namespace ILovePdfCompressor
{
    public class PdfCompressor : ICompressor
    {
        private readonly LovePdfApi api;

        public PdfCompressor(string publicKey, string privateKey)
        {
            api = new LovePdfApi(publicKey, privateKey);
        }

        public void Compress(string filePath, string destinationDirectoryPath)
        {
            if (api == null || string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(destinationDirectoryPath))
            {
                return;
            }

            try
            {
                // create compress task
                var task = api.CreateTask<CompressTask>();

                if (task == null)
                {
                    return;
                }

                // file variable contains server file name
                task.AddFile(filePath);

                // proces added files
                task.Process();

                // download files to specific folder
                task.DownloadFile(destinationDirectoryPath);
            }
            catch (Exception exception)
            {
                throw new Exception($@"/!\ Error while compressing : {filePath} ({exception.Message})");
            }
        }
    }
}
