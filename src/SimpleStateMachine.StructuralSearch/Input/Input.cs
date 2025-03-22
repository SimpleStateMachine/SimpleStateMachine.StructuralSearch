using System.IO;

namespace SimpleStateMachine.StructuralSearch.Input;

public static class Input
{
    public static readonly EmptyInput Empty = new ();
    public static StringInput String(string input) => new (input);
    public static FileInput File(FileInfo fileInfo) => new (fileInfo);
}