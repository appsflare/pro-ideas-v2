using ProIdeas.Files.Contracts;
using System;
using System.Linq;
using System.IO;
using RethinkDb.Driver.ReGrid;
using RethinkDb.Driver.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProIdeas.Domain.Repositories.RethinkDb
{
    public class ReGridStorageProvider : IFileStorage
    {
        private readonly IConnection _connection;
        private readonly ConnectionOptions _connectionOptions;
        private readonly Bucket _bucket;

        public ReGridStorageProvider(IRethinkDbConnectionProvider connectionProvider, ConnectionOptions connectionOptions)
        {
            _connectionOptions = connectionOptions;
            _connection = connectionProvider.GetConnection(_connectionOptions);
            _bucket = new Bucket(_connection, _connectionOptions.DBName, "fs");
            _bucket.Mount();

        }

        public Task<Guid> AddFileAsync(string fileName, Stream fileStream, object metadata)
        {
            var uploadOptions = new UploadOptions();
            uploadOptions.SetMetadata(metadata);
            return _bucket.UploadAsync(fileName, fileStream, uploadOptions);
        }



        public async Task<bool> CheckIfExistsAsync(Guid fileId)
        {

            try
            {
                var file = await _bucket.GetFileInfoAsync(fileId);

                return file != null;
            }
            catch
            {

                return false;
            }

        }

        public async Task<bool> CheckIfExistsAsync(string fileName)
        {
            try
            {
                var file = await _bucket.GetFileInfoByNameAsync(fileName);

                return file != null;

            }
            catch
            {

                return false;
            }
        }

        public async Task<IEnumerable<IFileInfo>> GetAllRevisionsAsync(string fileName)
        {
            var revisions = await _bucket.GetAllRevisionsAsync(fileName);
            return revisions.ToList().Select(CreateInternalFile).ToList();
        }

        public async Task<IFileInfo> GetFileInfoAsync(Guid fileId)
        {
            var file = await _bucket.GetFileInfoAsync(fileId);

            return CreateInternalFile(file);
        }

        private static InternalFileInfo CreateInternalFile(global::RethinkDb.Driver.ReGrid.FileInfo file)
        {
            return new InternalFileInfo(file.Id, file.FileName, file.Metadata);
        }


        public async Task<IFileInfo> GetFileInfoAsync(string fileName)
        {
            var file = await _bucket.GetFileInfoByNameAsync(fileName);

            return CreateInternalFile(file);
        }

        public async Task<bool> DeleteRevisionAsync(Guid fileId)
        {
            try
            {
                await _bucket.DeleteRevisionAsync(fileId);
            }
            catch
            {

                return false;
            }
            return true;
        }

        public async Task<bool> DeleteAllRevisionsAsync(string fileName)
        {
            try
            {
                await _bucket.DeleteAllRevisionsAsync(fileName);
            }
            catch
            {

                return false;
            }
            return true;
        }

        public async Task<Stream> GetFileStreamAsync(string fileName)
        {
            if (!await CheckIfExistsAsync(fileName))
            {
                return Stream.Null;
            }


            var stream = new MemoryStream();
            await _bucket.DownloadToStreamByNameAsync(fileName, stream);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public Task<byte[]> GetFileBytesAsync(string fileName)
        {
            return _bucket.DownloadAsBytesByNameAsync(fileName);
        }

        private class InternalFileInfo : IFileInfo
        {
            public InternalFileInfo(Guid id, string fileName, object metaData)
            {
                Id = id;
                FileName = fileName;
                MetaData = metaData;
            }
            public Guid Id { get; private set; }

            public string FileName { get; private set; }

            public object MetaData { get; private set; }
        }


    }
}
