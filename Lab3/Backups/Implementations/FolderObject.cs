using System.IO.Compression;
using Backups.Exceptions;
using Backups.Interfaces;
using Backups.Models;
using Newtonsoft.Json;
using Zio;
using Zio.FileSystems;

namespace Backups.Implementations;

public class FolderObject : IBackupObject
{
    public FolderObject(Repository repository, UPath path)
    {
        if (repository == null)
            throw new ArgumentNullException(nameof(repository));
        if (repository.GetObjectAttributes(path) != FileAttributes.Directory)
            throw FolderObjectException.InvalidObjectType(path);
        if (path == null)
            throw new ArgumentNullException(nameof(path));
        Repository = repository;
        Path = path;
    }

    public Repository Repository { get; }

    [JsonConverter(typeof(UPathConverter))]
    public UPath Path { get; }

    [JsonIgnore]
    public string Name => Path.GetName();

    public Stream Archive(Stream source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));
        using (var archive = new ZipArchiveFileSystem(source, ZipArchiveMode.Update, true))
        {
            Repository.CopyDirectory(Path, archive, UPath.Root / Path.GetName(), true);
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