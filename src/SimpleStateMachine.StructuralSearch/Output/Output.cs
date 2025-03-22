using System.IO;

namespace SimpleStateMachine.StructuralSearch.Output;

internal static class Output
{
    public static IOutput File(FileInfo fileInfo) => new FileOutput(fileInfo);
}