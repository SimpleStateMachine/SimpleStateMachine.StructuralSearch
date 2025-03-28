using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Input;
using SimpleStateMachine.StructuralSearch.Operator.Logical;
using SimpleStateMachine.StructuralSearch.Placeholder;
using SimpleStateMachine.StructuralSearch.Rules.FindRules;

namespace SimpleStateMachine.StructuralSearch.Context;

public interface IParsingContext : IDictionary<string, IPlaceholder>
{
    IInput Input { get; }
    IReadOnlyCollection<ILogicalOperation> FindRules { get; }
}