using ProIdeas.Files.Contracts;
using System.IO;

namespace ProIdeas.Files
{
    public class LocalStorageProvider : IStorageProvider
    {
        public bool CheckIfExist(string fileName)
        {
            return File.Exists(fileName);
        }

        public Stream GetFileStream(string fileName)
        {
            return File.OpenRead(fileName);
        }

        public void SaveFileStream(string fileName, Stream stream)
        {
            using (var fileStream = File.OpenWrite(fileName))
            {
                stream.CopyTo(fileStream);
                fileStream.Flush();
            }
        }
    }
}
