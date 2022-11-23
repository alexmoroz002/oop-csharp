using Backups.Implementations;
using Backups.Interfaces;

namespace Backups.Visitors;

public interface IBackupObjectTypeVisitor
{
    void VisitFolder(FolderObject folder);
    void VisitFile(FileObject file);
}