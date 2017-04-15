using System.IO;

namespace ProIdeas.Files.Contracts
{
    public interface IStorageProvider
    {

        bool CheckIfExist(string fileName);

        Stream GetFileStream(string fileName);

        void SaveFileStream(string fileName, Stream stream);

    }
}
