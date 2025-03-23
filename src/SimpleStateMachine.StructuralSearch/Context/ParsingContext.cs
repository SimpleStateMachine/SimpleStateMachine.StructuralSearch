using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Input;
using SimpleStateMachine.StructuralSearch.Placeholder;
using SimpleStateMachine.StructuralSearch.Rules.FindRules;

namespace SimpleStateMachine.StructuralSearch.Context;

internal class ParsingContext : Dictionary<string, IPlaceholder>, IParsingContext
{
    public ParsingContext(IInput input, IReadOnlyCollection<IFindRule> findRules)
    {
        Input = input;
        FindRules = findRules;
    }

    public IInput Input { get; }
    public IReadOnlyCollection<IFindRule> FindRules { get; }
}