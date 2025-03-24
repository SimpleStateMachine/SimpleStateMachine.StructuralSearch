using SimpleStateMachine.StructuralSearch.Context;

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