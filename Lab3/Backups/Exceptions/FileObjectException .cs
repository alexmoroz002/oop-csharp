using Zio;

namespace Backups.Exceptions;

public class FileObjectException : Exception
{
    private FileObjectException(string e)
        : base(e) { }

    public static FileObjectException InvalidObjectType(UPath path)
    {
        return new FileObjectException($"Object in {path} is a directory, expected file");
    }
}