using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Input;

namespace SimpleStateMachine.StructuralSearch.Output;

internal interface IOutput
{
    void Replace(IInput input, IEnumerable<ReplaceMatch> replaceMatches);
}