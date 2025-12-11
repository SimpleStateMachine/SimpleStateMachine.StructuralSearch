using System.IO;
using System.Runtime.CompilerServices;

namespace SimpleStateMachine.StructuralSearch.Tests.Helper;

public static class DataHelper
{
    public static FileInfo GetDataFileInfo(string fileName, [CallerFilePath] string? testFilePath = null)
    {
        var directoryName = Path.GetDirectoryName(testFilePath!)!;
        var path = Path.Combine(directoryName, "Data", fileName);
        return new FileInfo(path);
    }

    public static string ReadDataFileText(string fileName, [CallerFilePath] string? testFilePath = null)
    {
        var fileInfo = GetDataFileInfo(fileName, testFilePath);
        return File.ReadAllText(fileInfo.FullName);
    }
}