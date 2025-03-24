using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Rules.Parameters;

namespace SimpleStateMachine.StructuralSearch.Templates.ReplaceTemplate;

internal class ReplaceBuilder : IReplaceBuilder
{
    public static readonly EmptyReplaceBuilder Empty = new ();
    
    private readonly IRuleParameter _parameter;

    public ReplaceBuilder(IRuleParameter parameter)
    {
        _parameter = parameter;
    }

    public string Build(ref IParsingContext context) 
        => _parameter.GetValue(ref context);

    public override string? ToString() 
        => _parameter.ToString();
}