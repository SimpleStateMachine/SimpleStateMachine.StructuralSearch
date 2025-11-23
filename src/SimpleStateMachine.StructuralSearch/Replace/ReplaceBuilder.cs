using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Parameters;

namespace SimpleStateMachine.StructuralSearch.Replace;

internal class ReplaceBuilder(IParameter parameter) : IReplaceBuilder
{
    public string Build(ref IParsingContext context) 
        => parameter.GetValue(ref context);

    public override string? ToString() 
        => parameter.ToString();
}