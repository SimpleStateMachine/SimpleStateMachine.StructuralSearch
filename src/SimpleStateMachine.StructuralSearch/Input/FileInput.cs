using System;
using System.IO;

namespace SimpleStateMachine.StructuralSearch.Input;

public class FileInput(FileInfo fileInfo) : IInput
{
    public TextReader ReadData()
        => fileInfo.OpenText();

    public string GetProperty(string propertyName)
    {
        var lower = propertyName.ToLower();

        return lower switch
        {
            "path" => Path.GetFullPath(fileInfo.FullName),
            "extension" => fileInfo.Extension,
            "name" => fileInfo.Name,
            "length" => fileInfo.Length.ToString(),
            _ => throw new ArgumentOutOfRangeException(propertyName)
        };
    }
}