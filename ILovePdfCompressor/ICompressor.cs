namespace ILovePdfCompressor
{
    public interface ICompressor
    {
        void Compress(string filePath, string destinationDirectoryPath);
    }
}