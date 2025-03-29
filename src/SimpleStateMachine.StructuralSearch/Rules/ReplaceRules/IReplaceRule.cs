using System;
using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Rules.FindRules;

namespace SimpleStateMachine.StructuralSearch.Rules.ReplaceRules;

internal interface IReplaceRule
{
    IEnumerable<Assignment> Assignments { get; }
    bool IsMatch(ref IParsingContext context);
}

internal class EmptyReplaceRule : IReplaceRule
{
    public IEnumerable<Assignment> Assignments { get; } = Array.Empty<Assignment>();

    public bool IsMatch(ref IParsingContext context)
        => true;
}