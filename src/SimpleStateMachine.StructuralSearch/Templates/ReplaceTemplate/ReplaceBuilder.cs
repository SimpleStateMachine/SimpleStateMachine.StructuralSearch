using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Parameters;

namespace SimpleStateMachine.StructuralSearch.Templates.ReplaceTemplate;

internal class ReplaceBuilder : IReplaceBuilder
{
    public static readonly EmptyReplaceBuilder Empty = new ();
    
    private readonly IParameter _parameter;

    public ReplaceBuilder(IParameter parameter)
    {
        _parameter = parameter;
    }

    public string Build(ref IParsingContext context) 
        => _parameter.GetValue(ref context);

    public override string? ToString() 
        => _parameter.ToString();
}