using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SimpleStateMachine.StructuralSearch;

public class FileOutput : IOutput
{
    public readonly FileInfo FileInfo;
    
    public FileOutput(FileInfo fileInfo)
    {
        FileInfo = fileInfo;
    }
    
    public void Replace(IInput input, IEnumerable<ReplaceMatch> replaceMatches)
    {
        var text = replaceMatches
            .Aggregate(input.Data, (current, replaceMatch) => 
                current.Replace(replaceMatch.Match.Value, replaceMatch.Value));
        
        File.WriteAllText(Path, text);
    }
    
    public string Extension => FileInfo.Extension;
    public string Path => System.IO.Path.GetFullPath(FileInfo.FullName);
    public string Name => System.IO.Path.GetFileNameWithoutExtension(FileInfo.Name);
}