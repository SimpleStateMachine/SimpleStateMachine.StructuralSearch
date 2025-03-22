using System;
using System.IO;

namespace SimpleStateMachine.StructuralSearch.Input;

public class FileInput : IInput
{
    private readonly FileInfo _fileInfo;

    public FileInput(FileInfo fileInfo)
    {
        _fileInfo = fileInfo;
    }

    public TextReader ReadData()
        => _fileInfo.OpenText();

    public string GetProperty(string propertyName)
    {
        var lower = propertyName.ToLower();

        return lower switch
        {
            "path" => System.IO.Path.GetFullPath(_fileInfo.FullName),
            "extension" => _fileInfo.Extension,
            "name" => _fileInfo.Name,
            "length" => _fileInfo.Length.ToString(),
            _ => throw new ArgumentOutOfRangeException(propertyName)
        };
    }
}