using System.IO;

namespace SimpleStateMachine.StructuralSearch;

public static class Output
{
    public static IOutput File(FileInfo fileInfo) => new FileOutput(fileInfo);
}