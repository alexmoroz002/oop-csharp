using System.IO.Compression;
using Zio;

namespace Backups.Entities;

public class Storage
{
    private UPath _path;
    private List<ZipArchive> _archives;
}