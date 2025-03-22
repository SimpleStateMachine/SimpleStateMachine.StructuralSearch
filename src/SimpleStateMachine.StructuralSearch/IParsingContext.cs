using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Input;

namespace SimpleStateMachine.StructuralSearch;

internal interface IParsingContext : IDictionary<string, IPlaceholder>
{
    IInput Input { get; }
}