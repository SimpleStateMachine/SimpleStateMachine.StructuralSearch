using System;
using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Rules.FindRules;

namespace SimpleStateMachine.StructuralSearch.Rules.ReplaceRules;

internal interface IReplaceRule
{
    IEnumerable<ReplaceSubRule> Rules { get; }
    bool IsMatch(ref IParsingContext context);
}

internal class EmptyReplaceRule : IReplaceRule
{
    public IEnumerable<ReplaceSubRule> Rules { get; } = Array.Empty<ReplaceSubRule>();
    
    public bool IsMatch(ref IParsingContext context) 
        => Rule.Empty.Execute(ref context);
}