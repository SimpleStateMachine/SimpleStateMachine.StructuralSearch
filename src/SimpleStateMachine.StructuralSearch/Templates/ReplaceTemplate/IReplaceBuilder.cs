using System;
using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Rules;

namespace SimpleStateMachine.StructuralSearch.ReplaceTemplate;

internal interface IReplaceBuilder
{
    string Build(ref IParsingContext context);
}
    
internal class EmptyReplaceBuilder: IReplaceBuilder
{
    public IEnumerable<IRuleParameter> Steps { get; } = Array.Empty<IRuleParameter>();
        
    public string Build(ref IParsingContext context) 
        => string.Empty;
}