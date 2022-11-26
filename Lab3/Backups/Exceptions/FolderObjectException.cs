using Zio;

namespace Backups.Exceptions;

public class FolderObjectException : Exception
{
    private FolderObjectException(string e)
        : base(e) { }

    public static FolderObjectException InvalidObjectType(UPath path)
    {
        return new FolderObjectException($"Object in {path} is a file, expected directory");
    }
}