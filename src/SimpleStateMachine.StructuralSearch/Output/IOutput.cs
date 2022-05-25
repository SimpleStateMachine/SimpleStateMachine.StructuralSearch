using System.Collections.Generic;

namespace SimpleStateMachine.StructuralSearch;

public interface IOutput
{
    void Replace(IInput input, IEnumerable<ReplaceMatch> replaceMatches);
}