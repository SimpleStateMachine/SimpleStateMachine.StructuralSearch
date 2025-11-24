using System.Collections.Generic;
using System.IO;
using System.Linq;
using SimpleStateMachine.StructuralSearch.Input;
using SimpleStateMachine.StructuralSearch.Replace;

namespace SimpleStateMachine.StructuralSearch.Output;

internal class FileOutput(FileInfo fileInfo) : IOutput
{
    public void Replace(IInput input, IEnumerable<ReplaceResult> replaceResults)
    {
        var inputStr = input.ReadData().ReadToEnd();
        var text = replaceResults.Aggregate(inputStr, (current, replaceMatch) =>
            current.Replace(replaceMatch.FindResult.Match.Value, replaceMatch.Value));

        File.WriteAllText(fileInfo.FullName, text);
    }
}