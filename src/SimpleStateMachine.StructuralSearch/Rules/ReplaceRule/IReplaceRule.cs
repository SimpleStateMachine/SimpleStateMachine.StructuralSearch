using System;
using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch;

public interface IReplaceRule : IContextDependent
{
    IEnumerable<ReplaceSubRule> Rules { get; }
    IRule ConditionRule { get; }
}

public class EmptyReplaceRule : IReplaceRule
{
    public IEnumerable<ReplaceSubRule> Rules { get; } = Array.Empty<ReplaceSubRule>();
    public IRule ConditionRule => Rule.Empty;

    public void SetContext(IParsingContext context)
    {
    }
}