using System.IO;

namespace SimpleStateMachine.StructuralSearch;

internal static class Output
{
    public static IOutput File(FileInfo fileInfo) => new FileOutput(fileInfo);
}