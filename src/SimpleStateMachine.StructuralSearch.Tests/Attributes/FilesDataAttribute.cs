using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit.Sdk;

namespace SimpleStateMachine.StructuralSearch.Tests.Attributes;

public class FilesDataAttribute : DataAttribute
{
    private readonly string _folderPath;

    public FilesDataAttribute(string folderPath)
    {
        _folderPath = folderPath;
    }

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        ArgumentNullException.ThrowIfNull(testMethod);

        if (!Directory.Exists(_folderPath))
        {
            throw new DirectoryNotFoundException($"Could not find folder: {_folderPath}");
        }

        foreach (var file in Directory.GetFiles(_folderPath))
        {
            yield return [file];
        }
    }
}