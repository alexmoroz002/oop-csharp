using System.IO.Compression;
using Backups.Interfaces;
using Zio;

namespace Backups.Entities;

public class Storage : IStorage
{
    private UPath _path;
    private List<ZipArchive> _archives;
}