using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ProIdeas.Files.Contracts
{
    public interface IFileStorage
    {
        Task<bool> CheckIfExistsAsync(Guid fileId);

        Task<bool> CheckIfExistsAsync(string fileName);

        Task<Guid> AddFileAsync(string fileName, Stream fileStream, object metadata);

        Task<IFileInfo> GetFileInfoAsync(Guid fileId);

        Task<IFileInfo> GetFileInfoAsync(string fileName);

        Task<Stream> GetFileStreamAsync(string fileName);

        Task<byte[]> GetFileBytesAsync(string fileName);

        Task<IEnumerable<IFileInfo>> GetAllRevisionsAsync(string fileName);        

        Task<bool> DeleteRevisionAsync(Guid fileId);

        Task<bool> DeleteAllRevisionsAsync(string fileName);


    }
}
