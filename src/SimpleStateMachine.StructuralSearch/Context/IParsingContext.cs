using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Input;
using SimpleStateMachine.StructuralSearch.Placeholder;

namespace SimpleStateMachine.StructuralSearch.Context;

internal interface IParsingContext : IDictionary<string, IPlaceholder>
{
    IInput Input { get; }
}