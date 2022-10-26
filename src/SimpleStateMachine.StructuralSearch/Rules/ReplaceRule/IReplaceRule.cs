using System;
using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch;

public interface IReplaceRule
{
    IEnumerable<ReplaceSubRule> Rules { get; }
    bool IsMatch(ref IParsingContext context);
}

public class EmptyReplaceRule : IReplaceRule
{
    public IEnumerable<ReplaceSubRule> Rules { get; } = Array.Empty<ReplaceSubRule>();
    
    public bool IsMatch(ref IParsingContext context)
    {
        return Rule.Empty.Execute(ref context);
    }
}