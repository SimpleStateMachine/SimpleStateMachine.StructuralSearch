using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Input;

namespace SimpleStateMachine.StructuralSearch;

public class ParsingContext : Dictionary<string, IPlaceholder>, IParsingContext
{
    public ParsingContext(IInput input)
    {
        Input = input;
    }
    
    public ParsingContext(IInput input, IReadOnlyDictionary<string, IPlaceholder> placeholders) : base(placeholders)
    {
        Input = input;
    }

    // public IInput Input { get; }
    public IInput Input { get; }
}