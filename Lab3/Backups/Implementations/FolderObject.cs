using System.IO.Compression;
using Backups.Interfaces;
using Backups.Models;
using Zio;
using Zio.FileSystems;

namespace Backups.Implementations;

public class FolderObject : IBackupObject
{
    private Repository _repository;

    public FolderObject(Repository repository, UPath path)
    {
        if (repository.FileSystem.GetAttributes(path) != FileAttributes.Directory)
            throw new NotImplementedException();
        _repository = repository;
        Path = path;
    }

    public UPath Path { get; }

    public Stream Archive(Stream source)
    {
        using (var archive = new ZipArchiveFileSystem(source, ZipArchiveMode.Update, true))
        {
            _repository.FileSystem.CopyDirectory(Path, archive, UPath.Root, true);
        }

        return source;
    }
}