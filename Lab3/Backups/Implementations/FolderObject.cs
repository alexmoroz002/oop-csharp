using System.IO.Compression;
using Backups.Exceptions;
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
        if (repository.GetObjectAttributes(path) != FileAttributes.Directory)
            throw FolderObjectException.InvalidObjectType(path);
        _repository = repository;
        Path = path;
    }

    public UPath Path { get; }

    public string Name => Path.GetName();

    public Stream Archive(Stream source)
    {
        using (var archive = new ZipArchiveFileSystem(source, ZipArchiveMode.Update, true))
        {
            _repository.CopyDirectory(Path, archive, UPath.Root / Path.GetName(), true);
        }

        return source;
    }

    public override bool Equals(object obj)
    {
        return obj is FolderObject folderObject &&
               Name == folderObject.Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}