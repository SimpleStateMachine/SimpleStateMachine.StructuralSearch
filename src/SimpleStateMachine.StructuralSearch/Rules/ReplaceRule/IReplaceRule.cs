using System;
using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch;

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