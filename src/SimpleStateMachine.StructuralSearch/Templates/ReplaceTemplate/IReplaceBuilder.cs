using System;
using System.Collections.Generic;
using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Rules.Parameters;

namespace SimpleStateMachine.StructuralSearch.Templates.ReplaceTemplate;

internal interface IReplaceBuilder
{
    string Build(ref IParsingContext context);
}
    
internal class EmptyReplaceBuilder: IReplaceBuilder
{
    public string Build(ref IParsingContext context) 
        => string.Empty;
}