using System;

namespace ProIdeas.Files.Contracts
{
   public interface IFileInfo
    {
        Guid Id { get; }
        string FileName { get; }
        object MetaData { get; }

    }
}
