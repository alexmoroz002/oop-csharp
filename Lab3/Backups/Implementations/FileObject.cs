﻿using System.IO.Compression;
using Backups.Exceptions;
using Backups.Interfaces;
using Backups.Models;
using Zio;
using Zio.FileSystems;

namespace Backups.Implementations;

public class FileObject : IBackupObject
{
    public FileObject(Repository repository, UPath path)
    {
        if (repository.GetObjectAttributes(path) == FileAttributes.Directory)
            throw FileObjectException.InvalidObjectType(path);
        Repository = repository;
        Path = path;
    }

    public UPath Path { get; }
    public Repository Repository { get; }

    public string Name => Path.GetName();

    public Stream Archive(Stream source)
    {
        using (var archive = new ZipArchiveFileSystem(source, ZipArchiveMode.Update, true))
        {
            Repository.CopyFile(Path, archive, UPath.Root / Path.GetName(), true);
        }

        return source;
    }

    public override bool Equals(object obj)
    {
        return obj is FileObject fileObject &&
               Name == fileObject.Name;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}