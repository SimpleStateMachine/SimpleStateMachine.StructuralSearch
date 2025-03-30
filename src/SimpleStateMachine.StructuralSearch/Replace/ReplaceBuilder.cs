using SimpleStateMachine.StructuralSearch.Context;
using SimpleStateMachine.StructuralSearch.Parameters;

namespace SimpleStateMachine.StructuralSearch.Replace;

internal class ReplaceBuilder : IReplaceBuilder
{
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