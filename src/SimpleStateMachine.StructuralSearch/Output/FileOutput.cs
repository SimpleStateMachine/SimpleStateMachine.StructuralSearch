using System;
using System.Collections.Generic;
using System.IO;
using SimpleStateMachine.StructuralSearch.Input;

namespace SimpleStateMachine.StructuralSearch.Output;

internal class FileOutput : IOutput
{
    private readonly FileInfo _fileInfo;
    
    public FileOutput(FileInfo fileInfo)
    {
        _fileInfo = fileInfo;
    }
    
    public void Replace(IInput input, IEnumerable<ReplaceMatch> replaceMatches)
    {
        throw new NotImplementedException();
        // var text = replaceMatches
        //     .Aggregate(input.Data, (current, replaceMatch) => 
        //         current.Replace(replaceMatch.Match.Value, replaceMatch.Value));
        //
        // File.WriteAllText(Path, text);
    }
    
    public string Extension => _fileInfo.Extension;
    public string Path => System.IO.Path.GetFullPath(_fileInfo.FullName);
    public string Name => System.IO.Path.GetFileNameWithoutExtension(_fileInfo.Name);
}