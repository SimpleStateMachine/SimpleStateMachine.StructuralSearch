using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Context;

namespace SimpleStateMachine.StructuralSearch.Replace;

internal interface IReplaceRule
{
    List<Assignment> Assignments { get; }
    bool IsMatch(ref IParsingContext context);
}

internal class EmptyReplaceRule : IReplaceRule
{
    public List<Assignment> Assignments { get; } = [];

    public bool IsMatch(ref IParsingContext context)
        => true;
}