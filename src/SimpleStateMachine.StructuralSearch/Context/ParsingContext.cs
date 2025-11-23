using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Input;
using SimpleStateMachine.StructuralSearch.Operator.Logical;

namespace SimpleStateMachine.StructuralSearch.Context;

internal class ParsingContext(IInput input, IReadOnlyCollection<ILogicalOperation> findRules)
    : Dictionary<string, IPlaceholder>, IParsingContext
{
    public IInput Input { get; } = input;
    public IReadOnlyCollection<ILogicalOperation> FindRules { get; } = findRules;
}