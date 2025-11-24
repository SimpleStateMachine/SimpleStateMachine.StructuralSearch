using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Input;
using SimpleStateMachine.StructuralSearch.Replace;

namespace SimpleStateMachine.StructuralSearch.Output;

internal interface IOutput
{
    void Replace(IInput input, IEnumerable<ReplaceResult> replaceResults);
}