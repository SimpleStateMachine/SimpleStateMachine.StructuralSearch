using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Input;
using SimpleStateMachine.StructuralSearch.Placeholder;

namespace SimpleStateMachine.StructuralSearch.Context;

internal class ParsingContext : Dictionary<string, IPlaceholder>, IParsingContext
{
    public ParsingContext(IInput input)
    {
        Input = input;
    }

    public IInput Input { get; }
}