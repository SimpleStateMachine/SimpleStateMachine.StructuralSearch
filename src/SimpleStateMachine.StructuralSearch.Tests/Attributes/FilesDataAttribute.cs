using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Xunit.Sdk;

namespace SimpleStateMachine.StructuralSearch.Tests.Attributes;

public class FilesDataAttribute(string folderPath) : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        ArgumentNullException.ThrowIfNull(testMethod);

        if (!Directory.Exists(folderPath))
            throw new DirectoryNotFoundException($"Could not find folder: {folderPath}");

        foreach (var file in Directory.GetFiles(folderPath))
            yield return [file];
    }
}