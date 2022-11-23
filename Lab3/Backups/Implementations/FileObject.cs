using System.IO.Compression;
using Backups.Interfaces;
using Backups.Models;
using Zio;
using Zio.FileSystems;

namespace Backups.Implementations;

public class FileObject : IBackupObject
{
    public UPath Path { get; }
    private Repository _repository;

    public FileObject(UPath path, Repository repository)
    {
        if (repository.FileSystem.GetAttributes(path) == FileAttributes.Directory)
            throw new NotImplementedException();
        _repository = repository;
        Path = path;
    }

    public Stream Archive(Stream source)
    {
        using (var archive = new ZipArchiveFileSystem(source, ZipArchiveMode.Update, true))
        {
            _repository.FileSystem.CopyFileCross(Path, archive, UPath.Root / Path.GetName(), true);
        }

        return source;
    }
}